﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab1_DataBinding
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Person person = new Person { Name = "Salman", Age = 26 };
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = person;
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string message = person.Name + "   is   " + person.Age + "  years old";
            txtblock.Text = message;
        }
    }
    public class Person
    {
        private string nameValue;
        public string Name { get { return nameValue; }set { nameValue = value; } }
        private double ageValue;
        public double Age { get { return ageValue; }set { ageValue = value; } }
    }
}
