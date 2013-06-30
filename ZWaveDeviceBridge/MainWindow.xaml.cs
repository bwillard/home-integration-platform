using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZWaveWrappers;
using ZWaveWrappers.Interfaces;
using System.ServiceModel;
using ZWaveDeviceBridge.DataService;
using System.ServiceModel.Description;
using ZWaveDeviceBridge.RemoteControl;

namespace ZWaveDeviceBridge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public static Wrapper Wrapper {get;set;}
        //private ServiceHost m_serviceHost;
        private ButtonHandler m_buttonHandler;
        private Settings settings = Settings.LoadSettings();

        public MainWindow()
        {
            Console.WriteLine("in main window");
            InitializeComponent();

            try
            {
                Wrapper = new ZWaveWrappers.Wrapper(Mode.Online);
                m_buttonHandler = new ButtonHandler(Wrapper.Controller.Devices);

                Devices.ItemsSource = Wrapper.Controller.Devices;
                Mappings.ItemsSource = m_buttonHandler.Mappings;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }



            /*m_serviceHost = new ServiceHost(typeof(HomeStatusService));
            try
            {
                m_serviceHost.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't start HomeStatusService: " + e);
            }*/
        }

        private void SwitchToggleClicked(object sender, RoutedEventArgs e)
        {
            IBinarySceneSwitch s = ((FrameworkElement)sender).DataContext as IBinarySceneSwitch;
            if (s.Level == 0)
            {
                s.PowerOn();
            }
            else
            {
                s.PowerOff();
            }
        }

        private void AddNewMapping(object sender, RoutedEventArgs e)
        {
            m_buttonHandler.Train();
        }

        private void AddDevice(object sender, RoutedEventArgs e)
        {
            String result = Wrapper.Controller.AddDevice();
            MessageBox.Show(result);
        }
    }
}
