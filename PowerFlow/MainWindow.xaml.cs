using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerFlow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //move btn treba maknut - zaminit kod
        // ONLY ALLAW TO CONNECT NODES AND ELEMENTS
        public MainWindow()//write methods for repeating code
        {
            InitializeComponent();
            Application.Current.MainWindow.WindowState = WindowState.Maximized;

            //initialize dictionaries
            Data.ConnectionDict = new Dictionary<int, Connection>();
            Data.NodeBusDict = new Dictionary<int, NodeBus>();
            Data.LoadDict = new Dictionary<int, Load>();
            Data.GeneratorDict = new Dictionary<int, Generator>();
            Data.PowerLineDict = new Dictionary<int, PowerLine>();
            Data.TransformerDict = new Dictionary<int, Transformer>();
            //initialize buttons
            Data.Buttons = new List<Button>() {BtnConnection, BtnPLine};
            //mainGrid.MouseRightButtonDown += 
        }

        private void mainWindow_KeyDown(object sender, KeyEventArgs e)//rotate element
        {
            if(e.Key==Key.R)
            {
                if (Data.NodeBusSelected!=null && Data.NodeBusSelected.IsSelected)
                {
                    Data.NodeBusSelected.Angle += 90;
                    if(Data.NodeBusSelected.Angle==360)
                    {
                        Data.NodeBusSelected.Angle = 0;
                    }

                    Data.NodeBusSelected.rotateTransform.Angle = Data.NodeBusSelected.Angle;
                    Data.NodeBusSelected.rotateTransform.CenterX = Data.NodeBusSelected.ActualWidth / 2.0;
                    Data.NodeBusSelected.rotateTransform.CenterY = Data.NodeBusSelected.ActualHeight / 2.0;

                    Data.NodeBusSelected.RenderTransformOrigin= new Point(0.5, 0.5);

                    Data.NodeBusSelected.transformGroup.Children.Remove(Data.NodeBusSelected.rotateTransform);
                    Data.NodeBusSelected.transformGroup.Children.Remove(Data.NodeBusSelected.translateTransform);

                    Data.NodeBusSelected.transformGroup.Children.Add(Data.NodeBusSelected.rotateTransform);
                    Data.NodeBusSelected.transformGroup.Children.Add(Data.NodeBusSelected.translateTransform);

                    Data.NodeBusSelected.RenderTransform = Data.NodeBusSelected.transformGroup;

                    
                }
                else if(Data.GeneratorSelected!=null && Data.GeneratorSelected.IsSelected)
                {
                    Data.GeneratorSelected.Angle += 90;
                    if (Data.GeneratorSelected.Angle == 360)
                    {
                        Data.GeneratorSelected.Angle = 0;
                    }

                    Data.GeneratorSelected.rotateTransform.Angle = Data.GeneratorSelected.Angle;
                    Data.GeneratorSelected.rotateTransform.CenterX = Data.GeneratorSelected.ActualWidth / 2.0;
                    Data.GeneratorSelected.rotateTransform.CenterY = Data.GeneratorSelected.ActualHeight / 2.0;

                    Data.GeneratorSelected.RenderTransformOrigin = new Point(0.5, 0.5);

                    Data.GeneratorSelected.transformGroup.Children.Remove(Data.GeneratorSelected.rotateTransform);
                    Data.GeneratorSelected.transformGroup.Children.Remove(Data.GeneratorSelected.translateTransform);

                    Data.GeneratorSelected.transformGroup.Children.Add(Data.GeneratorSelected.rotateTransform);
                    Data.GeneratorSelected.transformGroup.Children.Add(Data.GeneratorSelected.translateTransform);

                    Data.GeneratorSelected.RenderTransform = Data.GeneratorSelected.transformGroup;
                }
                else if(Data.LoadSelected!=null && Data.LoadSelected.IsSelected)
                {
                    Data.LoadSelected.Angle += 90;
                    if (Data.LoadSelected.Angle == 360)
                    {
                        Data.LoadSelected.Angle = 0;
                    }

                    Data.LoadSelected.rotateTransform.Angle = Data.LoadSelected.Angle;
                    Data.LoadSelected.rotateTransform.CenterX = Data.LoadSelected.ActualWidth / 2.0;
                    Data.LoadSelected.rotateTransform.CenterY = Data.LoadSelected.ActualHeight / 2.0;

                    Data.LoadSelected.RenderTransformOrigin = new Point(0.5, 0.5);

                    Data.LoadSelected.transformGroup.Children.Remove(Data.LoadSelected.rotateTransform);
                    Data.LoadSelected.transformGroup.Children.Remove(Data.LoadSelected.translateTransform);

                    Data.LoadSelected.transformGroup.Children.Add(Data.LoadSelected.rotateTransform);
                    Data.LoadSelected.transformGroup.Children.Add(Data.LoadSelected.translateTransform);

                    Data.LoadSelected.RenderTransform = Data.LoadSelected.transformGroup;
                }
                else if (Data.TransformerSelected != null && Data.TransformerSelected.IsSelected)
                {
                    Data.TransformerSelected.Angle += 90;
                    if (Data.TransformerSelected.Angle == 360)
                    {
                        Data.TransformerSelected.Angle = 0;
                    }

                    Data.TransformerSelected.rotateTransform.Angle = Data.TransformerSelected.Angle;
                    Data.TransformerSelected.rotateTransform.CenterX = Data.TransformerSelected.ActualWidth / 2.0;
                    Data.TransformerSelected.rotateTransform.CenterY = Data.TransformerSelected.ActualHeight / 2.0;

                    Data.TransformerSelected.RenderTransformOrigin = new Point(0.5, 0.5);

                    Data.TransformerSelected.transformGroup.Children.Remove(Data.TransformerSelected.rotateTransform);
                    Data.TransformerSelected.transformGroup.Children.Remove(Data.TransformerSelected.translateTransform);

                    Data.TransformerSelected.transformGroup.Children.Add(Data.TransformerSelected.rotateTransform);
                    Data.TransformerSelected.transformGroup.Children.Add(Data.TransformerSelected.translateTransform);

                    Data.TransformerSelected.RenderTransform = Data.TransformerSelected.transformGroup;
                }
            }
        }

        private void UnselectElement()//unselect selected elements
        {
            if (Data.NodeBusSelected!=null && Data.NodeBusSelected.IsSelected)
            {
                Data.NodeBusSelected.IsSelected = false;
                Data.NodeBusSelected.Opacity = 1;
                Data.NodeBusSelected = null;
            }
            else if (Data.GeneratorSelected != null && Data.GeneratorSelected.IsSelected)
            {
                Data.GeneratorSelected.IsSelected = false;
                Data.GeneratorSelected.Opacity = 1;
                Data.GeneratorSelected = null;
            }
            else if (Data.LoadSelected!=null && Data.LoadSelected.IsSelected)
            {
                Data.LoadSelected.IsSelected = false;
                Data.LoadSelected.Opacity = 1;
                Data.LoadSelected = null;
            }
            else if (Data.PowerLineSelected != null && Data.PowerLineSelected.IsSelected)
            {
                Data.PowerLineSelected.IsSelected = false;
                foreach (var item in Data.PowerLineSelected.PowerLineList)
                {
                    item.Opacity = 1;
                }
                Data.PowerLineSelected = null;
            }
            else if (Data.TransformerSelected != null && Data.TransformerSelected.IsSelected)
            {
                Data.TransformerSelected.IsSelected = false;
                Data.TransformerSelected.Opacity = 1;
                Data.TransformerSelected = null;
            }
        }

        private void UnselectElement_Grid(object sender, MouseButtonEventArgs e)//unselect elements when gird clicked
        {
            UnselectElement();
        }

        private void SelectElement(object sender, MouseButtonEventArgs e)//select elements//if double click open input win
        {
            if (sender is NodeBus)
            {
                var element = sender as NodeBus;

                if (Data.NodeBusSelected != element)
                {
                    UnselectElement();
                    element.IsSelected = true;
                    element.Opacity = 0.5;
                    Data.NodeBusSelected = element;
                }
                else if(e.ClickCount==2)//double click
                {
                    NodeBusWin nbWin = new NodeBusWin();
                    nbWin.Show();
                }
            }
            else if(sender is Generator)
            {
                var element = sender as Generator;

                if (Data.GeneratorSelected != element)
                {
                    UnselectElement();
                    element.IsSelected = true;
                    element.Opacity = 0.5;
                    Data.GeneratorSelected = element;//pamti element
                }
                else if (e.ClickCount == 2)
                {
                    GeneratorWin genWin = new GeneratorWin();
                    genWin.Show();
                }
            }
            else if(sender is Load)
            {
                var element = sender as Load;

                if (Data.LoadSelected != element)
                {
                    UnselectElement();
                    element.IsSelected = true;
                    element.Opacity = 0.5;
                    Data.LoadSelected = element;//pamti element
                }
                else if (e.ClickCount == 2)
                {
                    LoadWin loadWin = new LoadWin();
                    loadWin.Show();
                }
            }
            else if(sender is Line)//if powerLine
            {
                var element = sender as Line;

                int dictKey = 1000;//da ne bude nula i da da exception

                dictKey = Data.PowerLineDict.First(x => x.Value.PowerLineList.Contains(sender)).Key; //dogodi se exception ako se ne neđe linija

                var powerLine = Data.PowerLineDict[dictKey];

                if (Data.PowerLineSelected != powerLine )
                {
                    UnselectElement();
                    powerLine.IsSelected = true;
                    foreach (var item in powerLine.PowerLineList)
                    {
                        item.Opacity = 0.5;
                    }
                    Data.PowerLineSelected = powerLine;
                }
                else if (e.ClickCount == 2)
                {
                    PowerLineWin PLineWin = new PowerLineWin();
                    PLineWin.Show();
                }
                
            }
            else if (sender is Transformer)
            {
                var element = sender as Transformer;

                if (Data.TransformerSelected != element)
                {
                    UnselectElement();
                    element.IsSelected = true;
                    element.Opacity = 0.5;
                    Data.TransformerSelected = element;//pamti element
                }
                else if (e.ClickCount == 2)
                {
                    TransformerWin TrWin = new TransformerWin();
                    TrWin.Show();
                }
            }

        }

        #region Add Elements - NodeBus, Generator and Load
        private void nodeBusBtn_Click(object sender, RoutedEventArgs e)//add node bus to grid
        {
            NodeBus nodebus = new NodeBus();

            nodebus.HorizontalAlignment = HorizontalAlignment.Left;
            nodebus.VerticalAlignment = VerticalAlignment.Top;

            Data.NodeBusDict.Add(Data.NodeBusCounter++, nodebus);

            Data.MaxKey_NodeBus = Data.NodeBusDict.Max(x => x.Key);//find the max key value // acces the last elemnt with the max key// later for deleting elements

            Data.NodeBusDict[Data.MaxKey_NodeBus].MouseEnter += NodeBusEnter;//za pokretanje elmeneata
            Data.NodeBusDict[Data.MaxKey_NodeBus].MouseLeave += NodeBusLeave;//za pokretanje elmeneata

            Panel.SetZIndex(Data.NodeBusDict[Data.MaxKey_NodeBus], 3);//put node in front of the line so it can be clicked

            mainGrid.Children.Add(Data.NodeBusDict[Data.MaxKey_NodeBus]);//add element to grid
        }

        private void genBtn_Click(object sender, RoutedEventArgs e)// add generator
        {
            Generator generator = new Generator();
            generator.HorizontalAlignment = HorizontalAlignment.Left;
            generator.VerticalAlignment = VerticalAlignment.Top;

            Data.GeneratorDict.Add(Data.GeneratorCounter++, generator);

            Data.MaxKey_Generator = Data.GeneratorDict.Max(x => x.Key);//max key is the last key because it is set via the LoadCounter//new element has the highets key value

            Data.GeneratorDict[Data.MaxKey_Generator].MouseEnter += GeneratorEnter;//za move feature
            Data.GeneratorDict[Data.MaxKey_Generator].MouseLeave += GeneratorLeave;//za move feature

            Panel.SetZIndex(Data.GeneratorDict[Data.MaxKey_Generator], 3);//put node in front of the line so it can be clicked

            mainGrid.Children.Add(Data.GeneratorDict[Data.MaxKey_Generator]);//add element to grid

        }


        private void loadBtn_Click(object sender, RoutedEventArgs e)//add load
        {
            Load load = new Load();
            load.HorizontalAlignment = HorizontalAlignment.Left;
            load.VerticalAlignment = VerticalAlignment.Top;

            Data.LoadDict.Add(Data.LoadCounter++, load);

            Data.MaxKey_Load = Data.LoadDict.Max(x => x.Key);//max key is the last key because it is set via the LoadCounter

            Data.LoadDict[Data.MaxKey_Load].MouseEnter += LoadEnter;//za move feature
            Data.LoadDict[Data.MaxKey_Load].MouseLeave += LoadLeave;//za move feature

            Panel.SetZIndex(Data.LoadDict[Data.MaxKey_Load], 3);//put node in front of the line so it can be clicked

            mainGrid.Children.Add(Data.LoadDict[Data.MaxKey_Load]);//add element to grid
        }

        private void transformerBtn_Click(object sender, RoutedEventArgs e)
        {
            Transformer transformer = new Transformer();
            transformer.HorizontalAlignment = HorizontalAlignment.Left;
            transformer.VerticalAlignment = VerticalAlignment.Top;

            Data.TransformerDict.Add(Data.TransformerCounter++, transformer);

            Data.MaxKey_transformer = Data.TransformerDict.Max(x => x.Key);//max key is the last key because it is set via the LoadCounter//new element has the highets key value

            Data.TransformerDict[Data.MaxKey_transformer].MouseEnter += TransformerEnter;//za move feature
            Data.TransformerDict[Data.MaxKey_transformer].MouseLeave += TransformerLeave;//za move feature

            Panel.SetZIndex(Data.TransformerDict[Data.MaxKey_transformer], 3);//put node in front of the line so it can be clicked

            mainGrid.Children.Add(Data.TransformerDict[Data.MaxKey_transformer]);//add element to grid
        }

        #endregion

        #region Vertical Canvas Buttons - Connection and Move

        private void BtnConnection_Click(object sender, RoutedEventArgs e)// enable connection drawing
        {
            if(!Data.ConnectionIsEnabled)
            {
                Data.ConnectionIsEnabled = true;
                BtnConnection.Background = Brushes.GreenYellow;
                Data.Buttons.Where(x => x != BtnConnection).ToList().ForEach(x => x.IsEnabled = false);//disable all other buttons
            }
            else
            {
                Data.ConnectionIsEnabled = false;
                BtnConnection.Background = Brushes.LightGray;
                Data.Buttons.Where(x => x != BtnConnection).ToList().ForEach(x => x.IsEnabled = true);
            }
        }

        private void BtnPline_Click(object sender, RoutedEventArgs e)
        {
            if (!Data.PLineIsEnabled)
            {
                Data.PLineIsEnabled = true;
                BtnPLine.Background = Brushes.GreenYellow;
                Data.Buttons.Where(x => x != BtnPLine).ToList().ForEach(x => x.IsEnabled = false);
            }
            else
            {
                Data.PLineIsEnabled = false;
                BtnPLine.Background = Brushes.LightGray;
                Data.Buttons.Where(x => x != BtnPLine).ToList().ForEach(x => x.IsEnabled = true);
            }
        }

        //private void Move_Elements(object sender, RoutedEventArgs e)//enable moving of elelments
        //{
        //    //if (!Data.MoveElements)
        //    //{
        //    //    Data.MoveElements = true;
        //    //    BtnMove.Background = Brushes.GreenYellow;
        //    //    Data.Buttons.Where(x => x != BtnMove).ToList().ForEach(x => x.IsEnabled = false);
        //    //}
        //    //else
        //    //{
        //    //    Data.MoveElements = false;
        //    //    BtnMove.Background = Brushes.LightGray;
        //    //    Data.Buttons.Where(x => x != BtnMove).ToList().ForEach(x => x.IsEnabled = true);
        //    //}
        //}

        #endregion

        


        private Line InitaializeLine(MouseEventArgs e, int StrokeThickness, Brush color )//create new line
        {
            //create new lineElement
            Line lineElement = new Line();
            lineElement= new Line();
            lineElement.X1 = e.GetPosition(mainGrid).X;
            lineElement.Y1 = e.GetPosition(mainGrid).Y;

            //lineElement.startPoint = new Point(e.GetPosition(mainGrid).X, e.GetPosition(mainGrid).Y);// save the start point

            lineElement.X2 = e.GetPosition(mainGrid).X;//initaial points are the same point
            lineElement.Y2 = e.GetPosition(mainGrid).Y;

            //line properties
            lineElement.Visibility = System.Windows.Visibility.Visible;
            lineElement.StrokeThickness = StrokeThickness;
            lineElement.Stroke = color;//System.Windows.Media.Brushes.Black;

            return lineElement;
        }

        #region Creating a connection

        private void MouseDown_Connection(object sender, MouseEventArgs e)//drawing connections
        {
            //int elementKey = 0;

            if (sender is Ellipse)//start of connection only can be Ellipse -> load or generator//always ellipse//it configuer with the addin of events in leave nad enter events
            {
                var element = sender as Ellipse;

                if (element.Name == "LoadConnector")
                {
                    Data.ElementKeyType = new Tuple<int, string>(Data.LoadDict.Where(x => x.Value.LoadConnector == (Ellipse)sender).Select(x => x.Key).FirstOrDefault(), "Load");//sprema key i vrstu elementa
                    if (Data.LoadDict[Data.ElementKeyType.Item1].Connected)//ako je konektor već spojen ne crtaj novu liniju i ne spajaj konekotr//item1 je key
                    {
                        return;
                    }
                    else
                    {
                        Data.LoadDict[Data.ElementKeyType.Item1].Connected = true;
                    }
                }
                else if (element.Name == "GenConnector")
                {

                    Data.ElementKeyType = new Tuple<int, string>(Data.GeneratorDict.Where(x => x.Value.GenConnector == (Ellipse)sender).Select(x => x.Key).FirstOrDefault(), "Generator");
                    if (Data.GeneratorDict[Data.ElementKeyType.Item1].Connected)//ako je konektor već spojen ne crtaj novu liniju i ne spajaj konekotr
                    {
                        return;
                    }
                    else
                    {
                        Data.GeneratorDict[Data.ElementKeyType.Item1].Connected = true;
                    }
                }
                else if (element.Name == "TrConnectorVN")
                {

                    Data.ElementKeyType = new Tuple<int, string>(Data.TransformerDict.Where(x => x.Value.TrConnectorVN == (Ellipse)sender).Select(x => x.Key).FirstOrDefault(), "TransformerVN");
                    if (Data.TransformerDict[Data.ElementKeyType.Item1].ConnectedConVN)//ako je konektor već spojen ne crtaj novu liniju i ne spajaj konekotr
                    {
                        return;
                    }
                    else
                    {
                        Data.TransformerDict[Data.ElementKeyType.Item1].ConnectedConVN = true;
                    }
                }
                else if (element.Name == "TrConnectorNN")
                {

                    Data.ElementKeyType = new Tuple<int, string>(Data.TransformerDict.Where(x => x.Value.TrConnectorNN == (Ellipse)sender).Select(x => x.Key).FirstOrDefault(), "TransformerNN");
                    if (Data.TransformerDict[Data.ElementKeyType.Item1].ConnectedConNN)//ako je konektor već spojen ne crtaj novu liniju i ne spajaj konekotr
                    {
                        return;
                    }
                    else
                    {
                        Data.TransformerDict[Data.ElementKeyType.Item1].ConnectedConNN = true;
                    }
                }
                //
            }
            else if (sender is Rectangle)//end of connection//
            {
                var element = sender as Rectangle;
                Data.ConnectionEnd = true;//kada je node kliknut -> end of line
                element.MouseLeftButtonDown -= MouseDown_Connection;//maknuti mouse down kad je kraj connectiona nema taj elemnt što više klikat jer od njega ne počinje veza
                int NodeBusKey = Data.NodeBusDict.First(x => x.Value.NodeBusRect == element).Key;//get key in dict of clicked nodeBus
                if (Data.ElementKeyType.Item2 == "Load")
                {
                    Data.NodeBusDict[NodeBusKey].NodeLoadKeyList.Add(Data.ElementKeyType.Item1);//dodaje se key elementa iz load dicta koji je pokrenuo cranje linije
                    Data.LoadDict[Data.ElementKeyType.Item1].nodeKey = NodeBusKey;
                }
                else if (Data.ElementKeyType.Item2 == "Generator")
                {
                    Data.NodeBusDict[NodeBusKey].NodeGenKeyList.Add(Data.ElementKeyType.Item1);//add key data to node
                    Data.GeneratorDict[Data.ElementKeyType.Item1].NodeKey = NodeBusKey;//add key info to generator
                }
                else if (Data.ElementKeyType.Item2 == "TransformerVN")
                {
                    Data.NodeBusDict[NodeBusKey].ConnectedTransformerKeysDict.Add(Data.ElementKeyType.Item1, "TransformerVN");//add key data to node
                    Data.TransformerDict[Data.ElementKeyType.Item1].NodeKeyVN = NodeBusKey;
                }
                else if (Data.ElementKeyType.Item2 == "TransformerNN")
                {
                    Data.NodeBusDict[NodeBusKey].ConnectedTransformerKeysDict.Add(Data.ElementKeyType.Item1, "TransformerNN");//add key data to node
                    Data.TransformerDict[Data.ElementKeyType.Item1].NodeKeyNN = NodeBusKey;
                }

            }

            if (Data.ConnectionStart)// a new connection started
            {
                Data.ConnectionDict.Add(Data.ConnectionCounter++, new Connection());// add new connection to dictionary

                Line lineElement = InitaializeLine(e, 3, Brushes.Black);//initialize Line

                Data.MaxKey_Connection = Data.ConnectionDict.Max(x => x.Key);//find the max key value // acces the last elemnt with the max key// later for deleting elements

                //accesing the last elemnt in dict
                Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList.Add(lineElement);//accessing dictionary instance with max key number//add line elemnt to connection instance inside the dictionary
                Data.LineCounter++;//increment number of lines

                mainGrid.Children.Add(Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1]);// ne može preko cuntera ako se mogu brisat elementi//ili da se counter resetira//ili last ellemt of dictionary
                //unsubscription added to avoid multiple subscription
                mainGrid.MouseMove -= MouseMove_Connection;
                mainGrid.MouseMove += MouseMove_Connection;
                //
                Data.ConnectionStart = false;
                Data.ConnectionEnd = false;
                //to avoid multiple subscribing in other function//
                mainGrid.MouseLeftButtonDown -= Continue_Connection;
                mainGrid.MouseLeftButtonDown += Continue_Connection;//contione drawing on grid
            }
            else if (Data.ConnectionEnd == true)//connection end//
            {
                mainGrid.MouseMove -= MouseMove_Connection;//stop drawing line
                mainGrid.MouseLeftButtonDown -= Continue_Connection;
                Data.ConnectionStart = true;
                Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1].X2 = e.GetPosition(mainGrid).X;//save connection endpoint
                Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1].Y2 = e.GetPosition(mainGrid).Y;
                Data.LineCounter = 0;
                Data.ConnectionEnd = false;
            }
        }

       

        
        //Check if event is subscribed
       
        private void Continue_Connection(object sender, MouseEventArgs e)//continue connection//mainGrid event
        {
            Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1].X2 = e.GetPosition(mainGrid).X;

            Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1].Y2 = e.GetPosition(mainGrid).Y;

            Line lineElement = InitaializeLine(e, 3, Brushes.Black);//initialize Line

            Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList.Add(lineElement);
            Data.LineCounter++;

            mainGrid.Children.Add(Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1]);

            Data.ConnectionStart = false;
        }


        private void MouseMove_Connection(object sender, MouseEventArgs e)//drawing connections
        {
            Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1].X2 = e.GetPosition(mainGrid).X;
            Data.ConnectionDict[Data.MaxKey_Connection].ConnectionList[Data.LineCounter - 1].Y2 = e.GetPosition(mainGrid).Y;
        }

        #endregion

        #region Create PowerLine

        private void MouseDown_PLine(object sender, MouseEventArgs e)//drawing connections//event only enabled in nodeBus elements
        {
            if (Data.PLineStart)// a new power Line started
            {
                Data.PowerLineDict.Add(Data.PLineCounter++, new PowerLine());// add new connection to dictionary

                Line lineElement = InitaializeLine(e, 5, Brushes.Blue);//initialize Line

                Data.MaxKey_PLine = Data.PowerLineDict.Max(x => x.Key);//find the max key value // acces the last elemnt with the max key// later for deleting elements

                //accesing the last elemnt in dict
                Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList.Add(lineElement);//accessing dictionary instance with max key number//add line elemnt to connection instance inside the dictionary
                Data.LineCounter++;//increment number of lines

                Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].MouseEnter += PowerLineEnter;
                Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].MouseLeave += PowerLineLeave;

                mainGrid.Children.Add(Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1]);// ne može preko cuntera ako se mogu brisat elementi//ili da se counter resetira//ili last ellemt of dictionary
                
                //unsubscription added to avoid multiple subscription
                mainGrid.MouseMove -= MouseMove_PLine;
                mainGrid.MouseMove += MouseMove_PLine;
                //
                Data.PLineStart = false;
                Data.PLineEnd = false;
                //to avoid multiple subscribing in other function//
                mainGrid.MouseLeftButtonDown -= Continue_PLine;
                mainGrid.MouseLeftButtonDown += Continue_PLine;//contione drawing on grid
                //Start Node
                Data.PowerLineDict[Data.MaxKey_PLine].NodeStartKey = Data.NodeBusDict.First(x => x.Value.NodeBusRect == sender).Key;//pazit na exception//jer je First a ne First or default koji daje nula ako nema elemenata
            }
            else if (Data.PLineEnd == true)//connection end//
            {
                var element = sender as Rectangle;// mogo bi breakati kod//vjerojatno neće jer se event samo dodaje rectangle
                //kraj power lina - izbriši sve evente
                mainGrid.MouseMove -= MouseMove_PLine;//stop drawing line
                mainGrid.MouseLeftButtonDown -= Continue_PLine;
                element.MouseLeftButtonDown -= MouseDown_PLine;
                Data.PLineStart = true;
                Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].X2 = e.GetPosition(mainGrid).X ;//save connection endpoint
                Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].Y2 = e.GetPosition(mainGrid).Y;
                Data.LineCounter = 0;
                Data.PLineEnd = false;
                //add NodeEndKey to PowerLine Dict
                Data.PowerLineDict[Data.MaxKey_PLine].NodeEndKey = Data.NodeBusDict.First(x => x.Value.NodeBusRect == sender).Key;//pazit na exception//jer je First
                                                                                                                                  //add EndNodeKey to Start Node and StartNodeKey To EndNode// so that the start node knows to witch node it is connected

                //nepotrebo????
                Data.NodeBusDict[Data.PowerLineDict[Data.MaxKey_PLine].NodeStartKey].PLineNodeKeyList.Add(Data.PowerLineDict[Data.MaxKey_PLine].NodeEndKey);
                Data.NodeBusDict[Data.PowerLineDict[Data.MaxKey_PLine].NodeEndKey].PLineNodeKeyList.Add(Data.PowerLineDict[Data.MaxKey_PLine].NodeStartKey);

                //
                Data.NodeBusDict[Data.PowerLineDict[Data.MaxKey_PLine].NodeStartKey].ConnectedPowerLineKeys.Add(Data.MaxKey_PLine);
                Data.NodeBusDict[Data.PowerLineDict[Data.MaxKey_PLine].NodeEndKey].ConnectedPowerLineKeys.Add(Data.MaxKey_PLine);

            }
        }
        //Check if event is subscribed

        private void Continue_PLine(object sender, MouseEventArgs e)//continue connection//mainGrid event
        {
            Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].X2 = e.GetPosition(mainGrid).X;

            Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].Y2 = e.GetPosition(mainGrid).Y;

            Line lineElement = InitaializeLine(e, 5, Brushes.Blue);//initialize Line

            Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList.Add(lineElement);
            Data.LineCounter++;

            Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].MouseEnter += PowerLineEnter;
            Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].MouseLeave += PowerLineLeave;

            mainGrid.Children.Add(Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1]);

            Data.PLineStart = false;
        }


        private void MouseMove_PLine(object sender, MouseEventArgs e)//drawing connections
        {
            Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].X2 = e.GetPosition(mainGrid).X;
            Data.PowerLineDict[Data.MaxKey_PLine].PowerLineList[Data.LineCounter - 1].Y2 = e.GetPosition(mainGrid).Y;
        }
        #endregion

        #region Enter and Leave events for NodeBus, Generator, Load, Transformer

        //move events for node, generator and load
        private void NodeBusEnter(object sender, MouseEventArgs e)
        {
            var element = sender as NodeBus;

            if (Data.ConnectionIsEnabled)//ne maknu se eventi kada se šalta između power line i connection
            {
                if(!Data.ConnectionStart)//samo ako je elemnt već kliknut//jer se počinje linija s elementa a ne noda
                {
                    element.NodeBusRect.MouseLeftButtonDown += MouseDown_Connection;
                    mainGrid.MouseLeftButtonDown -= Continue_Connection;//disable continuing line on entering connector
                }
            }
            else if(Data.PLineIsEnabled)
            {
                if (!Data.PLineStart)//ako nije  početak powewrLine-a onda je kraj kada se ulazi u nodeBus kursoron
                {
                    Data.PLineEnd = true;
                }
                element.NodeBusRect.MouseLeftButtonDown += MouseDown_PLine;
                mainGrid.MouseLeftButtonDown -= Continue_PLine;//disable continuing line on entering connector
            }
            //else if(Data.MoveElements)//doadavanje evenata
            //{
            //    element.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
            //    element.MouseMove += UserControl_MouseMove_NodeBus;
            //    element.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
            //}
            else//kada nije clikana nijedna radnja
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                element.MouseLeftButtonDown += SelectElement;
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//micanje deselektiranje objekta
                //
                element.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
                element.MouseMove += UserControl_MouseMove_NodeBus;
                element.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
            }
        }
        private void NodeBusLeave(object sender, MouseEventArgs e)
        {
            var element = sender as NodeBus;
            if (Data.ConnectionIsEnabled)
            {
                element.NodeBusRect.MouseLeftButtonDown -= MouseDown_Connection;
                //the event is first unsubscribed to avoid mutliple subscriptions
                if (!Data.ConnectionStart)
                {
                    mainGrid.MouseLeftButtonDown -= Continue_Connection;
                    mainGrid.MouseLeftButtonDown += Continue_Connection;//enable drawing on grid when leave node//nastavi crtati liniju
                }
            }
            else if (Data.PLineIsEnabled)//
            {
                element.NodeBusRect.MouseLeftButtonDown -= MouseDown_PLine;
                if (!Data.PLineStart)//ako se već crta powerLine onda je nastavi
                {
                    Data.PLineEnd = false;//ako leave node nije end power line
                    mainGrid.MouseLeftButtonDown -= Continue_PLine;
                    mainGrid.MouseLeftButtonDown += Continue_PLine;//enable drawing on grid when leave node
                }
                
            }
            //else if (Data.MoveElements)//micanje evenata
            //{
            //    element.MouseLeftButtonDown -= UserControl_MouseLeftButtonDown;
            //    element.MouseMove -= UserControl_MouseMove_NodeBus;
            //    element.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            //}
            else
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                mainGrid.MouseLeftButtonDown += UnselectElement_Grid;//deselektiranje objekta kada se klikne grid//
                //
                element.MouseLeftButtonDown -= UserControl_MouseLeftButtonDown;
                element.MouseMove -= UserControl_MouseMove_NodeBus;
                element.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            }

        }

        private void GeneratorEnter(object sender, MouseEventArgs e)
        {
            var element = sender as Generator;
            if (Data.ConnectionIsEnabled)
            {
                if (Data.ConnectionStart)//samo ako je početna konekcija dodaj event//jer počinje od connectora
                {
                    element.GenConnector.MouseLeftButtonDown += MouseDown_Connection;
                }
                    
                mainGrid.MouseLeftButtonDown -= Continue_Connection;//end drawing on grid
            }
            else if (Data.PLineIsEnabled)
            {
                mainGrid.MouseLeftButtonDown -= Continue_PLine;//disable continuing line on entering connector
            }
            //else if (Data.MoveElements)
            //{
            //    element.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
            //    element.MouseMove += UserControl_MouseMove_Generator;
            //    element.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
            //}
            else
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                element.MouseLeftButtonDown += SelectElement;
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                //moving events
                element.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
                element.MouseMove += UserControl_MouseMove_Generator;
                element.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
            }
        }
        private void GeneratorLeave(object sender, MouseEventArgs e)
        {
            var element = sender as Generator;

            if (Data.ConnectionIsEnabled)
            {
                element.GenConnector.MouseLeftButtonDown -= MouseDown_Connection;
                if(!Data.ConnectionStart)//linija se već crta//sprječava da se dodaje handler ako linija nije startala kod već spojenog konekotra
                {
                    mainGrid.MouseLeftButtonDown -= Continue_Connection;
                    mainGrid.MouseLeftButtonDown += Continue_Connection;//enable drawing on grid when leave node
                }
            }
            else if (Data.PLineIsEnabled)//
            {
                if (!Data.PLineStart)//ako se već crta powerLine onda je nastavi
                {
                    mainGrid.MouseLeftButtonDown -= Continue_PLine;
                    mainGrid.MouseLeftButtonDown += Continue_PLine;//enable drawing on grid when leave node
                }
            }
            //else if (Data.MoveElements)
            //{
            //    element.MouseLeftButtonDown -= UserControl_MouseLeftButtonDown;
            //    element.MouseMove -= UserControl_MouseMove_Generator;
            //    element.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            //}
            else
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                mainGrid.MouseLeftButtonDown += UnselectElement_Grid;//deselektiranje objekta kada se klikne grid//
                //moving events
                element.MouseLeftButtonDown -= UserControl_MouseLeftButtonDown;
                element.MouseMove -= UserControl_MouseMove_Generator;
                element.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            }

        }

        private void LoadEnter(object sender, MouseEventArgs e)
        {
            var element = sender as Load;
          
            if (Data.ConnectionIsEnabled)
            {
                if (Data.ConnectionStart)//samo ako je početna konekcija dodaj event//jer počinje od connectora//da spriječim skidanje event handlera na klikanom elementu kojise skida u MouseDown_Connection eventu
                {
                    element.LoadConnector.MouseLeftButtonDown += MouseDown_Connection;
                }
                mainGrid.MouseLeftButtonDown -= Continue_Connection;//end drawing on grid
            }
            else if (Data.PLineIsEnabled)
            {
                mainGrid.MouseLeftButtonDown -= Continue_PLine;//disable continuing line on entering connector
            }
            //else if (Data.MoveElements)
            //{
            //    element.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
            //    element.MouseMove += UserControl_MouseMove_Load;
            //    element.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;

            //}
            else
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                element.MouseLeftButtonDown += SelectElement;
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                //
                element.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
                element.MouseMove += UserControl_MouseMove_Load;
                element.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;

            }
        }
        private void LoadLeave(object sender, MouseEventArgs e)
        {
            var element = sender as Load;
            if (Data.ConnectionIsEnabled)
            {
                element.LoadConnector.MouseLeftButtonDown -= MouseDown_Connection;
                if (!Data.ConnectionStart)//linija se već crta//sprječava da se dodaje handler ako linija nije startala kod već spojenog konekotra
                {
                    mainGrid.MouseLeftButtonDown -= Continue_Connection;
                    mainGrid.MouseLeftButtonDown += Continue_Connection;//enable drawing on grid when leave node
                }
            }
            else if (Data.PLineIsEnabled)//
            {
                if (!Data.PLineStart)//ako se već crta powerLine onda je nastavi
                {
                    mainGrid.MouseLeftButtonDown -= Continue_PLine;
                    mainGrid.MouseLeftButtonDown += Continue_PLine;//enable drawing on grid when leave node
                }
            }
            //else if (Data.MoveElements)
            //{
            //    element.MouseLeftButtonDown -= UserControl_MouseLeftButtonDown;
            //    element.MouseMove -= UserControl_MouseMove_Load;
            //    element.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            //}
            else
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                mainGrid.MouseLeftButtonDown += UnselectElement_Grid;//deselektiranje objekta kada se klikne grid//
                //
                element.MouseLeftButtonDown -= UserControl_MouseLeftButtonDown;
                element.MouseMove -= UserControl_MouseMove_Load;
                element.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            }
        }

        private void PowerLineEnter(object sender, MouseEventArgs e)
        {
            var element = sender as Line;

            //var Powline = Data.PowerLineDict.First(x => x.Value.PowerLineList.Contains(sender)).Value; 

            if(!Data.ConnectionIsEnabled && !Data.PLineIsEnabled)
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                element.MouseLeftButtonDown += SelectElement;
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
            }
        }

        private void PowerLineLeave(object sender, MouseEventArgs e)
        {
            var element = sender as Line;

            //var Powline = Data.PowerLineDict.First(x => x.Value.PowerLineList.Contains(sender)).Value;

            if (!Data.ConnectionIsEnabled && !Data.PLineIsEnabled)//(!Data.ConnectionIsEnabled && !Data.PLineIsEnabled && !Data.MoveElements)
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                mainGrid.MouseLeftButtonDown += UnselectElement_Grid;//deselektiranje objekta kada se klikne grid//
            }
        }

        private void TransformerEnter(object sender, MouseEventArgs e)
        {
            var element = sender as Transformer;
            if (Data.ConnectionIsEnabled)
            {
                if (Data.ConnectionStart)//samo ako je početna konekcija dodaj event//jer počinje od connectora
                {
                    element.TrConnectorVN.MouseLeftButtonDown += MouseDown_Connection;
                    element.TrConnectorNN.MouseLeftButtonDown += MouseDown_Connection;
                }

                mainGrid.MouseLeftButtonDown -= Continue_Connection;//end drawing on grid
            }
            else if (Data.PLineIsEnabled)
            {
                mainGrid.MouseLeftButtonDown -= Continue_PLine;//disable continuing line on entering connector
            }
            else
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                element.MouseLeftButtonDown += SelectElement;
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                                                                     //
                element.MouseLeftButtonDown += UserControl_MouseLeftButtonDown;
                element.MouseMove += UserControl_MouseMove_Transformer;
                element.MouseLeftButtonUp += UserControl_MouseLeftButtonUp;
            }
        }
        private void TransformerLeave(object sender, MouseEventArgs e)
        {
            var element = sender as Transformer;

            if (Data.ConnectionIsEnabled)
            {
                element.TrConnectorVN.MouseLeftButtonDown -= MouseDown_Connection;
                element.TrConnectorNN.MouseLeftButtonDown -= MouseDown_Connection;
                if (!Data.ConnectionStart)//linija se već crta//sprječava da se dodaje handler ako linija nije startala kod već spojenog konekotra
                {
                    mainGrid.MouseLeftButtonDown -= Continue_Connection;
                    mainGrid.MouseLeftButtonDown += Continue_Connection;//enable drawing on grid when leave node
                }
            }
            else if (Data.PLineIsEnabled)//
            {
                if (!Data.PLineStart)//ako se već crta powerLine onda je nastavi
                {
                    mainGrid.MouseLeftButtonDown -= Continue_PLine;
                    mainGrid.MouseLeftButtonDown += Continue_PLine;//enable drawing on grid when leave node
                }
            }
            else
            {
                element.MouseLeftButtonDown -= SelectElement;//remove if already subscribed//to avoid multiple subscriptions
                mainGrid.MouseLeftButtonDown -= UnselectElement_Grid;//
                mainGrid.MouseLeftButtonDown += UnselectElement_Grid;//deselektiranje objekta kada se klikne grid//
                //
                element.MouseLeftButtonDown -= UserControl_MouseLeftButtonDown;
                element.MouseMove -= UserControl_MouseMove_Transformer;
                element.MouseLeftButtonUp -= UserControl_MouseLeftButtonUp;
            }

        }

        #endregion

        #region Events for moving elements across the grid
        //coordination transfrom events (move elements)
        Point anchorPoint;
        Point currentPoint;
        bool isInDrag = false;

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            anchorPoint = e.GetPosition(null);
            element.CaptureMouse();
            isInDrag = true;
            e.Handled = true;
        }

        private void UserControl_MouseMove_NodeBus(object sender, MouseEventArgs e)//za nodebus
        {
            if (isInDrag)
            {
                var element = sender as NodeBus;
                currentPoint = e.GetPosition(null);

                element.translateTransform.X += currentPoint.X - anchorPoint.X;
                element.translateTransform.Y += (currentPoint.Y - anchorPoint.Y);
                anchorPoint = currentPoint;

                element.RenderTransformOrigin = new Point(0.5, 0.5);


                element.transformGroup.Children.Remove(element.rotateTransform);
                element.transformGroup.Children.Remove(element.translateTransform);

                element.transformGroup.Children.Add(element.rotateTransform);
                element.transformGroup.Children.Add(element.translateTransform);

                element.RenderTransform = element.transformGroup;


            }
        }

        private void UserControl_MouseMove_Generator(object sender, MouseEventArgs e)//za gen
        {
            if (isInDrag)
            {
                var element = sender as Generator;
                currentPoint = e.GetPosition(null);

                element.translateTransform.X += currentPoint.X - anchorPoint.X;
                element.translateTransform.Y += (currentPoint.Y - anchorPoint.Y);
                anchorPoint = currentPoint;

                element.RenderTransformOrigin = new Point(0.5, 0.5);


                element.transformGroup.Children.Remove(element.rotateTransform);
                element.transformGroup.Children.Remove(element.translateTransform);

                element.transformGroup.Children.Add(element.rotateTransform);
                element.transformGroup.Children.Add(element.translateTransform);

                element.RenderTransform = element.transformGroup;
            }
        }

        private void UserControl_MouseMove_Load(object sender, MouseEventArgs e)//za load
        {
            if (isInDrag)
            {
                var element = sender as Load;
                currentPoint = e.GetPosition(null);

                element.translateTransform.X += currentPoint.X - anchorPoint.X;
                element.translateTransform.Y += (currentPoint.Y - anchorPoint.Y);
                anchorPoint = currentPoint;

                element.RenderTransformOrigin = new Point(0.5, 0.5);


                element.transformGroup.Children.Remove(element.rotateTransform);
                element.transformGroup.Children.Remove(element.translateTransform);

                element.transformGroup.Children.Add(element.rotateTransform);
                element.transformGroup.Children.Add(element.translateTransform);

                element.RenderTransform = element.transformGroup;
            }
        }

        private void UserControl_MouseMove_Transformer(object sender, MouseEventArgs e)//za gen
        {
            if (isInDrag)
            {
                var element = sender as Transformer;
                currentPoint = e.GetPosition(null);

                element.translateTransform.X += currentPoint.X - anchorPoint.X;
                element.translateTransform.Y += (currentPoint.Y - anchorPoint.Y);
                anchorPoint = currentPoint;

                element.RenderTransformOrigin = new Point(0.5, 0.5);


                element.transformGroup.Children.Remove(element.rotateTransform);
                element.transformGroup.Children.Remove(element.translateTransform);

                element.transformGroup.Children.Add(element.rotateTransform);
                element.transformGroup.Children.Add(element.translateTransform);

                element.RenderTransform = element.transformGroup;
            }
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isInDrag)
            {
                var element = sender as FrameworkElement;
                element.ReleaseMouseCapture();
                isInDrag = false;
                e.Handled = true;
            }
        }




        #endregion

        private void Settings_Click(object sender, RoutedEventArgs e)//settingsWin
        {
            SettingsWin Win = new SettingsWin();
            Win.Show();
        }

        private void BtnCalc_Click(object sender, RoutedEventArgs e)
        {
            Data.CalcPowerFlow((double)Data.SB, (double)Data.UB, (double)Data.EpsRe, (double)Data.EpsIm, Data.PowerLineDict, Data.LoadDict, Data.GeneratorDict, Data.NodeBusDict,Data.TransformerDict );
            //Data.CalcPowerFlow(100, 110, 0.0001, 0.0001, Data.PowerLineDict, Data.LoadDict, Data.GeneratorDict, Data.NodeBusDict);
        }

        
    }
}
