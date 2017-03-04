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
using PiMediaCenter.Network;

namespace PiMediaCenter.userInterface
{
    /// <summary>
    /// Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : UserControl
    {
        KeyStrokes Navigator;
        
        public NavigationControl()
        {
            InitializeComponent();
            Navigator = new KeyStrokes();
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            Navigator.KeyUp();
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            Navigator.KeyLeft();
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            Navigator.KeyRight();
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            Navigator.KeyDown();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            Navigator.KeyEnter();
        }
        private void btnSubEnter_Click(object sender, RoutedEventArgs e)
        {
            Navigator.MouseRight();
        }
        private void btnInfoSent_Click(object sender, RoutedEventArgs e)
        {
            
        }

        
    }
}
