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
using System.Windows.Shapes;

namespace HomeIntegrationPlatform.Engine.RemoteControl
{
    /// <summary>
    /// Interaction logic for PickActionWindow.xaml
    /// </summary>
    public partial class PickDeviceWindow : Window
    {
        private PickDeviceWindow()
        {
            InitializeComponent();
        }

        private void OKClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal static int Display()
        {
            PickDeviceWindow window = new PickDeviceWindow();
            window.ShowDialog();
            return int.Parse(window.DeviceId.Text);
        }
    }
}
