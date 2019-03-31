using Microsoft.Win32;
using PiMediaCenter.Casting;
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
    /// Interaction logic for Casting.xaml
    /// </summary>
    public partial class Casting : Window
    {
        public Casting()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video|*.mkv;*.vob;*.avi;*.mov;*mp4|Audio|*.mp3;*.ogg;*.aac| Other|*.*";
            /*
                 case ".mkv":break;
				case ".flv":break;
				case ".vob":break;
				case ".avi":break;
				case ".mov":break;
				case ".qt":break;
				case ".wmv":break;
				case ".mpg":break;
				case ".mpeg":break;
				case ".mp4":break;
             */
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = openFileDialog.FileName;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(textBox.Text);
            
            
            //VLC vlc = new VLC();
            //VLC.Play(textBox.Text);
        }
    }
}
