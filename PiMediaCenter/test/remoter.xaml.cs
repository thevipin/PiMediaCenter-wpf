using PiMediaCenter.Network.Comm;
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
using System.Windows.Shapes;

namespace PiMediaCenter.test
{
    /// <summary>
    /// Interaction logic for remoter.xaml
    /// </summary>
    public partial class remoter : Window
    {
        PiMediaCenter.Network.PlayerRemoteController Remoter;
        public remoter()
        {
            InitializeComponent();
            Remoter = new Network.PlayerRemoteController();
        }

        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            DataCodex code = new DataCodex("!");
            code.put("src", txt_src.Text);
            code.put("name", "testFile");
            Remoter.Play(code);
        }

        private void btn_pause_Click(object sender, RoutedEventArgs e)
        {
            Remoter.Pause();
        }

        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            Remoter.Stop();
        }

        private void btn_resume_Click(object sender, RoutedEventArgs e)
        {
            Remoter.Resume();
        }

        private void btn_volumeup_Click(object sender, RoutedEventArgs e)
        {
            //Remoter.v
        }
    }
}
