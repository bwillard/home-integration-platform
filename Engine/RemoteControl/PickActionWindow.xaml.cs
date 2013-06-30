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
    public partial class PickActionWindow : Window
    {
        private PickActionWindow()
        {
            InitializeComponent();
        }

        private void OKClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal static ButtonAction Display()
        {
            PickActionWindow window = new PickActionWindow();
            window.ShowDialog();
            return (ButtonAction)Enum.Parse(typeof(ButtonAction), ((ComboBoxItem)window.Data.SelectedItem).Content.ToString());
        }
    }
}
