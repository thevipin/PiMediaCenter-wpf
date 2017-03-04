using PiMediaCenter.Network.Comm;
using System;
using System.Windows;


namespace PiMediaCenter.test
{
    /// <summary>
    /// Interaction logic for network.xaml
    /// </summary>
    public partial class network : Window
    {
        Client client;
        void onConnected()
        {
           label.Content = "Connected";
        }
        void onConnFailed(Exception e)
        {
            label.Content = "Failed";
            MessageBox.Show(e.StackTrace);
        }
        void onRecieve(DataCodex code)
        {
            string[] keys = code.getKeys().ToArray();
            textBox4.Text = code.getIdentifier();
            textBox5.Text += "\n**** New Incoming ***\n";
            for (int i = 0; i < keys.Length; i++)
            {
                textBox5.Text += " " + keys[i] + "=" + code.get(keys[i]) + "\n";
            }
            textBox5.Text += "**** End ***\n";
        }
        public network()
        {
            InitializeComponent();
            client = new Client(onConnected, onConnFailed, onRecieve);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string[] keys = textBox2.Text.Split('\n');
            string[] value = textBox3.Text.Split('\n');
            DataCodex code = new DataCodex(comboBox.Text);
            code.addKeys(keys);
            code.addValues(value);
            client.sent(code);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            comboBox.Items.Add("PlayerRemoteController");
            comboBox.Items.Add("test_NetworkServer");
            comboBox.Items.Add("KeyStrokes");
        }
    }
}
