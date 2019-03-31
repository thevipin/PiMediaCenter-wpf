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

namespace PiMediaCenter.userInterface
{
    /// <summary>
    /// Interaction logic for PlayListElementPage.xaml
    /// </summary>
    public partial class PlayListElementPage : UserControl
    {
        int Id;
        public delegate void onClick(int Id);
        public onClick onCLickPlay, onCLickRemove;
        public PlayListElementPage(int id,string FilePath)
        {
            Id = id;
            InitializeComponent();
            labeSrc.Content = FilePath;
            System.IO.FileInfo file = new System.IO.FileInfo(FilePath);
            labeName.Content = file.Name;
            //labeSrc.Content+=file.Attributes["Length"]
        }
        public PlayListElementPage()
        {
            InitializeComponent();
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                onCLickPlay(Id);
            }
            catch { }
        }        
        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                onCLickRemove(Id);                
            }
            catch { }
        }
    }
}
