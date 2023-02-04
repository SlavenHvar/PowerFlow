using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Numerics;
namespace PowerFlow
{
    public static class Data
    {
        //Element dictioanries
        public static Dictionary<int, Connection> ConnectionDict { get; set; }
        public static Dictionary<int, PowerLine> PowerLineDict { get; set; }
        public static Dictionary<int, NodeBus> NodeBusDict { get; set; }
        public static Dictionary<int, Load> LoadDict { get; set; }
        public static Dictionary<int, Generator> GeneratorDict { get; set; }
        public static Dictionary<int, Transformer> TransformerDict { get; set; }
        //Connection
        public static int ConnectionCounter = 0;// maybe add node bus list that holdes the keys
        public static bool ConnectionStart = true;
        public static bool ConnectionEnd = false;
        public static bool ConnectionIsEnabled = false;//
        public static int MaxKey_Connection = 0;// max key in connection dictionary//to acces last element
        //PowerLine
        public static int PLineCounter = 0;// maybe add node bus list that holdes the keys
        public static bool PLineStart = true;
        public static bool PLineEnd = false;
        public static bool PLineIsEnabled = false;//
        public static int MaxKey_PLine = 0;// max key in connection dictionary//to acces last element
        //PLine and Line var
        public static int LineCounter = 0;//counts nuber of lines in a connection

        //Node Bus
        public static int NodeBusCounter = 0;
        public static int MaxKey_NodeBus = 0;
        public static bool NodeConnected = false;//checks if node is already connected
        public static bool ConnectorConnected = false;//checks if Conecctor is already connected
        //Load
        public static int LoadCounter = 0;
        public static int MaxKey_Load = 0;
        //Generator
        public static int GeneratorCounter = 0;
        public static int MaxKey_Generator = 0;
        //Transformer
        public static int TransformerCounter = 0;
        public static int MaxKey_transformer = 0;
        //Move
        //public static bool MoveElements = false;
        //
        public static List<Button> Buttons;
        public static Tuple<int, string> ElementKeyType;
        //Hold selected items for// fast deselection without foreach//only one of them is selected ata the time no one of them//cant be one variable because they hold different user controls
        public static NodeBus NodeBusSelected = null;
        public static Generator GeneratorSelected = null;
        public static Load LoadSelected = null;
        public static PowerLine PowerLineSelected = null;
        public static Transformer TransformerSelected = null;
        //
        public static List<string> GeneratorTypes = new List<string>() { "RE", "OE" };
        //settings
        public static double? EpsRe {get;set;}
        public static double? EpsIm {get;set;}
        public static double? UB {get;set;}
        public static double? SB {get;set;}

        public static void CalcPowerFlow(double SB, double UB, double EpsRe, double EpsIm, Dictionary<int, PowerLine> PowerLineDictionary, 
            Dictionary<int,Load> LoadDict, Dictionary<int, Generator> GeneratorDict, Dictionary<int, NodeBus> NodeBusDict, Dictionary<int, Transformer> TransformerDictp)
        {
            //Calculate BaseVoltage for all nodes
            int currentNodeKey = 0;
            double? BaseVoltageInit = NodeBusDict[0].VoltageAmplitude;
            //List<int> NodeKeyWaitingList = new List<int>();
            Queue<int> NodeKeyWaitingQueue = new Queue<int>();
            NodeBusDict[0].BaseVoltage = BaseVoltageInit;// not in p.u.
            NodeKeyWaitingQueue.Enqueue(currentNodeKey);
            while (NodeBusDict.Any(x=>x.Value.BaseVoltage==null))
            {
                currentNodeKey = NodeKeyWaitingQueue.Peek();//first element in queue
                foreach (var key in NodeBusDict[currentNodeKey].ConnectedPowerLineKeys)
                {
                    PowerLineDict[key].BaseVoltage = BaseVoltageInit;
                    if(NodeBusDict[PowerLineDict[key].NodeStartKey].BaseVoltage==null)
                    {
                        NodeBusDict[PowerLineDict[key].NodeStartKey].BaseVoltage = BaseVoltageInit;
                        NodeKeyWaitingQueue.Enqueue(PowerLineDict[key].NodeStartKey);
                    }
                    else if(NodeBusDict[PowerLineDict[key].NodeEndKey].BaseVoltage == null)
                    {
                        NodeBusDict[PowerLineDict[key].NodeEndKey].BaseVoltage = BaseVoltageInit;
                        NodeKeyWaitingQueue.Enqueue(PowerLineDict[key].NodeEndKey);
                    }
                }
                foreach (var item in NodeBusDict[currentNodeKey].ConnectedTransformerKeysDict)
                {
                    TransformerDict[item.Key].BaseVoltage = BaseVoltageInit;
                    if (NodeBusDict[TransformerDict[item.Key].NodeKeyNN].BaseVoltage == null)
                    {
                        NodeBusDict[TransformerDict[item.Key].NodeKeyNN].BaseVoltage = BaseVoltageInit* TransformerDict[item.Key].VoltageVN/ TransformerDict[item.Key].VoltageNN;
                        NodeKeyWaitingQueue.Enqueue(TransformerDict[item.Key].NodeKeyNN);
                    }
                    else if(NodeBusDict[TransformerDict[item.Key].NodeKeyVN].BaseVoltage == null)
                    {
                        NodeBusDict[TransformerDict[item.Key].NodeKeyVN].BaseVoltage = BaseVoltageInit * TransformerDict[item.Key].VoltageNN / TransformerDict[item.Key].VoltageVN;
                        NodeKeyWaitingQueue.Enqueue(TransformerDict[item.Key].NodeKeyVN);
                    }
                }
                NodeKeyWaitingQueue.Dequeue();//remove first element added to the queue
            }
            //

            double ZB = Math.Pow(UB, 2) / SB;//bazna impedancija//ohm
            double YB = 1 / ZB;
            double VB = UB/Math.Sqrt(3);

            //Dictionary<int, PowerLine> PowerLineSumDict = new Dictionary<int, PowerLine>();//PowerLine in this dict represents all powerlines between two nodes

            Dictionary<int, Complex> NodeVoltageDict = new Dictionary<int, Complex>();//holds old dictionary node voltage values for calculating error//
            
            //powerLine data
            foreach (var powerLine in PowerLineDict)
            {
                powerLine.Value.Zu =new Complex((double)powerLine.Value.Rd, (double)powerLine.Value.Xd) * new Complex((double)powerLine.Value.lv,0)/ new Complex(ZB, 0);//ako je null  
                powerLine.Value.Yu = 1 / powerLine.Value.Zu;
                //krivo Yp preveliko
                powerLine.Value.Yp = new Complex((double)powerLine.Value.Gd, (double)powerLine.Value.Bd*1E-6) * new Complex((double)powerLine.Value.lv, 0) / new Complex(YB, 0);//ako je null
                

            }
            //Generator Data
            foreach (var generator in GeneratorDict)//možda se može prebacit kad se pridjeljuje vrijednost varijabli preko prozora
            {
                if (generator.Value.GeneratorType == "OE")
                {
                    generator.Value.PgB = (double)generator.Value.Pg / SB;
                    generator.Value.QgminB = (double)generator.Value.Qgmin / SB;
                    generator.Value.QgmaxB = (double)generator.Value.Qgmax / SB;
                }
                else if (generator.Value.GeneratorType == "RE")
                {
                    generator.Value.PgminB = (double)generator.Value.Pgmin / SB;
                    generator.Value.PgmaxB = (double)generator.Value.Pgmax / SB;

                    generator.Value.QgminB = (double)generator.Value.Qgmin / SB;
                    generator.Value.QgmaxB = (double)generator.Value.Qgmax / SB;
                }

            }
            //Load Data
            foreach (var load in LoadDict)
            {
                load.Value.PpB = (double)load.Value.P / SB;
                load.Value.QpB = (double)load.Value.Q / SB;
                load.Value.SpB = load.Value.Sp / SB;
            }

            //NodeBus Data
            foreach (var nodeBus in NodeBusDict)
            {
                nodeBus.Value.Voltage = Complex.FromPolarCoordinates((double)nodeBus.Value.VoltageAmplitude, (double)nodeBus.Value.VoltageAngle*Math.PI/180)/Math.Sqrt(3);

                nodeBus.Value.VoltageB = nodeBus.Value.Voltage / VB;

                NodeVoltageDict.Add(nodeBus.Key, nodeBus.Value.VoltageB);
            }

            //impedance between nodes//
            




            //YBUS
            int nodes = NodeBusDict.Count;




            Complex[,] YBUS = new Complex[nodes,nodes];

            for (int i = 0; i < nodes; i++)
            {
                //YBUS
                for (int j = 0; j < nodes; j++)
                {
                    if (i == j)
                    {
                        foreach (var item in NodeBusDict[i].ConnectedPowerLineKeys)//ako postoje paralelni vodovi koji su spojeni u oba čvora admitancije se samo zbrajaju// nije potrebno rasčlanjivati posebno paralelne vodove
                        {
                            YBUS[i, j] += PowerLineDict[item].Yu + PowerLineDict[item].Yp/2;
                        }
                    }
                    else
                    {
                       // var powerline = PowerLineDict.Where(x => x.Value.NodeStartKey == i && x.Value.NodeEndKey == j || x.Value.NodeStartKey == j && x.Value.NodeEndKey == i).FirstOrDefault().Value;
                        var powerline = PowerLineDict.Where(x => x.Value.NodeStartKey == i && x.Value.NodeEndKey == j || x.Value.NodeStartKey == j && x.Value.NodeEndKey == i);
                        if (powerline != null)
                        {
                            foreach (var item in powerline)//foreach jer mogu biti paralelne admitancije između dva čvora
                            {
                                YBUS[i, j] += item.Value.Yu;//samo uzdužne admitancije ???
                            }
                            YBUS[i, j] *= -1;
                        }
                        else
                        {
                            YBUS[i, j] = 0;//možda treba stavit new complex(0,0)
                        }
                    }
                }
            }

            foreach (var item in NodeBusDict)//nać bolje reješenje
            {
                item.Value.SpB = 0;
            }

            for (int i = 0; i < nodes; i++)
            {
                foreach (var item in NodeBusDict[i].NodeLoadKeyList)//load power calc
                {
                    NodeBusDict[i].SpB += LoadDict[item].SpB;//krivo računa//ukupni potrošači u čvoru
                }
            }
          

            bool run = true;
            Complex recAdm = new Complex();
            Complex sumV = new Complex();
            Complex sumG = 0;
            List<bool> ErrorList = new List<bool>();
            while (run) //napone uzimat iz NodeVoltageDict
            {
                ErrorList.Clear();
                //treba i nodevoltage dict updatat//sum += YBUS[gen.Value.NodeKey, x] * NodeVoltageDict[x];
                List<int> genList = null;
                
                for (int i = 0; i < nodes; i++) 
                {
                    genList = NodeBusDict[i].NodeGenKeyList;//lista generatora u ovom čvoru

                    if (genList != null)
                    {
                        foreach (var key in genList)
                        {
                            
                            sumG = 0;
                            //treba računat u while-u
                            if (GeneratorDict[key].GeneratorType == "OE")
                            {
                                for (int x = 0; x < NodeBusDict.Count; x++)
                                {
                                    //sum += YBUS[gen.Value.NodeKey, x] * NodeVoltageDict[x];
                                    sumG += YBUS[GeneratorDict[key].NodeKey, x] * NodeBusDict[x].VoltageB;
                                }
                                //gen.Value.QgB = new Complex(0,(NodeVoltageDict[gen.Value.NodeKey] * Complex.Conjugate(sum) + NodeBusDict[gen.Value.NodeKey].SpB).Imaginary);//nije ukupna snaga nego samo potrošač
                                GeneratorDict[key].QgB = new Complex(0, (NodeBusDict[GeneratorDict[key].NodeKey].VoltageB * Complex.Conjugate(sumG) + NodeBusDict[GeneratorDict[key].NodeKey].SpB).Imaginary);//nije ukupna snaga nego samo potrošač
                                if (GeneratorDict[key].QgB.Imaginary < GeneratorDict[key].Qgmin)
                                {
                                //dont set voltage for PV node to 1, calculate it
                                GeneratorDict[key].QgB = new Complex(0, (double)GeneratorDict[key].Qgmin);
                                }
                                else if (GeneratorDict[key].QgB.Imaginary > GeneratorDict[key].Qgmax)
                                {
                                //dont set voltage for PV node to 1, calculate it
                                GeneratorDict[key].QgB = new Complex(0, (double)GeneratorDict[key].Qgmax);
                                }
                                NodeBusDict[i].SgB += GeneratorDict[key].PgB + GeneratorDict[key].QgB;
                            }
                         }
                     }

                   

                    recAdm = 1 / YBUS[i, i];
                    //NodeBusDict[i].VoltageB = 1 / YBUS[i, i] *( new Complex(NodeBusDict[i].SgB.Real - NodeBusDict[i].SpB.Real, -1 * (NodeBusDict[i].SgB.Imaginary - NodeBusDict[i].SpB.Imaginary)) )
                    sumV = recAdm * (new Complex(NodeBusDict[i].SgB.Real - NodeBusDict[i].SpB.Real, -1 * (NodeBusDict[i].SgB.Imaginary - NodeBusDict[i].SpB.Imaginary)))/Complex.Conjugate(NodeVoltageDict[i]);
                    for (int j = 0; j < nodes; j++)
                    {
                        if(j!=i)
                        {
                            sumV -= recAdm * YBUS[i, j]* NodeVoltageDict[j];//mora biti nova vrijendost//uvdje ne treba ići još korekcija
                        }
                    }
                    var gen = GeneratorDict.FirstOrDefault(x => x.Value.NodeKey == i).Value;//ako samo postoji jedan gen u čvoru
                    NodeBusDict[i].SgB = 0;//možda krivo na ovom mjestu radit//setirat na nulu da se ne zbraja sa vrijednostima iz prošle iteracije
                    if (gen?.GeneratorType!="RE")//null ckeck???
                    {
                        NodeBusDict[i].VoltageB = sumV;

                        double reDiff = Math.Abs(NodeBusDict[i].VoltageB.Real - NodeVoltageDict[i].Real);
                        double imDiff = Math.Abs(NodeBusDict[i].VoltageB.Imaginary - NodeVoltageDict[i].Imaginary);//ne konvergira imaginarni dio

                        

                        if (reDiff<=EpsRe && imDiff<=EpsIm)
                        {
                            ErrorList.Add(true);
                        }

                        if(gen!=null)//ima generator u cvoru
                        {
                            
                            if (gen.QgB.Imaginary < gen.Qgmin)
                            {
                                //dont set voltage for PV node to 1, calculate it
                                gen.QgB = new Complex(0, (double)gen.Qgmin);
                            }
                            else if (gen.QgB.Imaginary > gen.Qgmax)
                            {
                                //dont set voltage for PV node to 1, calculate it
                                gen.QgB = new Complex(0, (double)gen.Qgmax);
                            }
                            else//if the reactive boundries arent passed set voltage to initial voltage
                            {
                                NodeBusDict[i].VoltageB = Complex.FromPolarCoordinates(NodeVoltageDict[i].Magnitude, NodeBusDict[i].VoltageB.Phase); //add initial magnitude and new phase because it's pv
                            }
                        }
                        NodeVoltageDict[i] = sumV;//sprema izračunatu vrijednost radi gornje jednadžbe jer se VoltageB mijenja za PV čvor u željenu vrijendost
                    }
                    
                }

                foreach (var item in NodeBusDict)
                {
                    NodeVoltageDict[item.Key] = item.Value.VoltageB;
                }

                if (ErrorList.Count!=0 && ErrorList.All(x=>x==true))
                {
                    run = false;
                }
            }




            ////foreach (var item in regGen)
            ////{
            ////    for (int j = 0; j < NodeBusDict.Count; j++)
            ////    {
            ////        NodeBusDict[item.Value.NodeKey].CurrentB += YBUS[item.Value.NodeKey, j] * NodeBusDict[j].VoltageB;
            ////    }
            ////}
            foreach (var item in NodeBusDict)//nać bolje reješenje
            {
                item.Value.CurrentB = 0;
            }

            //bazne struje čvorova
            for (int i = 0; i < NodeBusDict.Count; i++)
            {
                for (int j = 0; j < NodeBusDict.Count; j++)
                {
                    NodeBusDict[i].CurrentB += YBUS[i, j] * NodeBusDict[j].VoltageB;//!!!!!!!!!!!!!sve varijable koje imaju zbroj setirat na nulu prije novog pokretanja proračuna
                }
                NodeBusDict[i].SnodeB = NodeBusDict[i].VoltageB * Complex.Conjugate(NodeBusDict[i].CurrentB);
            }

            //provjera snage reg el
            var regGen = GeneratorDict.Where(x => x.Value.GeneratorType == "RE");

            foreach (var item in regGen)
            {
                item.Value.SgOutputB = NodeBusDict[item.Value.NodeKey].SnodeB;
                bool ReBoundry = item.Value.SgOutputB.Real <= item.Value.PgmaxB && item.Value.SgOutputB.Real >= item.Value.PgminB;
                bool ImBoundry = item.Value.SgOutputB.Imaginary <= item.Value.QgmaxB && item.Value.SgOutputB.Imaginary >= item.Value.QgminB;

                item.Value.BoundriesOK = ReBoundry && ImBoundry ? true : false;
            }
            
            //Snage vodova
            foreach (var item in PowerLineDict)
            {
                item.Value.SB = NodeBusDict[item.Value.NodeStartKey].VoltageB * 
                    Complex.Conjugate((NodeBusDict[item.Value.NodeStartKey].VoltageB * item.Value.Yp / 2 + 
                    (NodeBusDict[item.Value.NodeStartKey].VoltageB - NodeBusDict[item.Value.NodeEndKey].VoltageB) * item.Value.Yu));

                item.Value.SrevB = NodeBusDict[item.Value.NodeEndKey].VoltageB *
                   Complex.Conjugate((NodeBusDict[item.Value.NodeEndKey].VoltageB * item.Value.Yp / 2 +
                   (NodeBusDict[item.Value.NodeEndKey].VoltageB - NodeBusDict[item.Value.NodeStartKey].VoltageB) * item.Value.Yu));

                item.Value.SlossB = item.Value.SB + item.Value.SrevB;
            }



        }

    }
}
