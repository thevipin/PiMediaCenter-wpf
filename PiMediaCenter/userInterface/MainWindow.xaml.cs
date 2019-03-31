﻿using System;
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

namespace PiMediaCenter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            gridContiner.Children.Add(new PiMediaCenter.userInterface.RemoterPlayerPage());
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            PiMediaCenter.Network.MainClient.Kill();
        }

        private void btnMenuRemote_Click(object sender, RoutedEventArgs e)
        {
            gridContiner.Children.Clear();
            gridContiner.Children.Add(new PiMediaCenter.userInterface.RemoterPlayerPage());            
        }
    }
}
