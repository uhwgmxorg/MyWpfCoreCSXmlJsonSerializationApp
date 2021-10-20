using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace MyWpfCoreCSXmlJsonSerializationApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region INotify Changed Properties  
        private string message;
        public string Message
        {
            get { return message; }
            set { SetField(ref message, value, nameof(Message)); }
        }

        private Data selectedData;
        public Data SelectedData
        {
            get { return selectedData; }
            set { SetField(ref selectedData, value, nameof(SelectedData)); }
        }
        private ObservableCollection<Data> datas;
        public ObservableCollection<Data> Datas
        {
            get { return datas; }
            set { SetField(ref datas, value, nameof(Datas)); }
        }

        // Template for a new INotify Changed Property
        // for using CTRL-R-R
        private bool xxx;
        public bool Xxx
        {
            get { return xxx; }
            set { SetField(ref xxx, value, nameof(Xxx)); }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Datas = LST.PopulateData();

            // Set Button ToolTips
            Button_1.ToolTip = "Button #1: reload the default list";
            Button_2.ToolTip = "Button #2: clear the list";
            Button_3.ToolTip = "Button #3: save the list/classes to Xml";
            Button_4.ToolTip = "Button #4: save the list/classes to Json";
            Button_5.ToolTip = "Button #5: load the list from Xml";
            Button_6.ToolTip = "Button #6: load the list from Json";
            Button_7.ToolTip = "Button #7: save just one single class to CDatas.json/CDatas.xml";
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// Button_1_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_1_Click");

            // Reload the default list
            ReFillList(LST.PopulateData());
        }

        /// <summary>
        /// Button_2_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_2_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_2_Click");

            // Clear the list
            Datas.Clear();
        }

        /// <summary>
        /// Button_3_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_3_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_3_Click");

            // Save list to Xml
            LST.SaveListToXml<Data>(LST.OToL<Data>(Datas), "Datas.xml");
        }

        /// <summary>
        /// Button_4_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_4_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_4_Click");

            // Save list to Json
            LST.SaveListToJson<Data>(LST.OToL<Data>(Datas), "Datas.json");
        }

        /// <summary>
        /// Button_5_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_5_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_5_Click");

            // Load list from Xml
            ReFillList(LST.LToO<Data>(LST.LoadListFromXml<Data>("Datas.xml")));
        }

        /// <summary>
        /// Button_6_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_6_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_6_Click");

            // Load list from Json
            ReFillList(LST.LToO<Data>(LST.LoadListFromJson<Data>("Datas.json")));
        }

        /// <summary>
        /// Button_7_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_7_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_7_Click");

            Data dataX = new Data() { Id = 888, Name = "Xxxx" };

            LST.SaveClassToXml<Data>(dataX, "CDatas.xml");
            Debug.WriteLine("Save data to CDatas.xml");
            LST.LoadClassFromXml<Data>(ref dataX, "CDatas.xml");
            Debug.WriteLine("Load data from CDatas.xml Id={0} Name={1}", dataX.Id, dataX.Name);

            Data dataJ = new Data() { Id = 999, Name = "Jjjj" };

            LST.SaveClassToJson<Data>(dataJ, "CDatas.json");
            Debug.WriteLine("Save data to CDatas.json");
            LST.LoadClassFromJson<Data>(ref dataJ, "CDatas.json");
            Debug.WriteLine("Load data from CDatas.json Id={0} Name={1}", dataJ.Id, dataJ.Name);
        }

        /// <summary>
        /// Button_Close_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Button_Close_Click");
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// ReFillList
        /// </summary>
        /// <param name="ls"></param>
        private void ReFillList(ObservableCollection<Data> ls)
        {
            Datas.Clear();
            foreach (var i in ls) Datas.Add(i);
        }

        /// <summary>
        /// PopulateData
        /// </summary>
        /// <param name="newData"></param>
        /// <returns></returns>
        private ObservableCollection<Data> PopulateData(ObservableCollection<Data> newData)
        {
            int count = 1;

            #region Data
            var dataArray = new[] {
                new { Name = "Peter" },
                new { Name = "Allen" },
                new { Name = "Burton" },
                new { Name = "Elliott" },
                new { Name = "Graham" },
                new { Name = "Harvey" },
                new { Name = "Reynolds" },
                new { Name = "Robert" },
                new { Name = "Thomas" },
                new { Name = "Wilson" },
                new { Name = "Young" },
            };

            datas.Clear();
            foreach (var n in dataArray)
                newData.Add(new Data { Id = count++, Name = n.Name });
            #endregion

            return newData;
        }

        /// <summary>
        /// SetField
        /// for INotify Changed Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }

    public class Data
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
