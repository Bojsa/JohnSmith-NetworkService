﻿using PZ3_NetworkService.ViewModel;
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

namespace PZ3_NetworkService.View
{
    /// <summary>
    /// Interaction logic for Tab2.xaml
    /// </summary>
    public partial class Tab2 : UserControl
    {
        public Tab2()
        {
            InitializeComponent();
            DataContext = new Tab2ViewModel(this);

        }
    }
}
