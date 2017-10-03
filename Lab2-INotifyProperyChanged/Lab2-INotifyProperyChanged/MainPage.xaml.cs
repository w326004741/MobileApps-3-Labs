using System;
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

namespace Lab2INotifyProperyChanged
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //create Data Model
        public Adder calc = new Adder();
        public MainPage()
        {
            //give the page a data context
            this.InitializeComponent();
            DataContext = calc; // give the page a data context
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            // add code to change Arg1 and Arg2 in the background
            // then update the screen automatically. Use a button
            calc.Arg1 = 110;
            calc.Arg2 = 200;
        }

        private void tblAnswer_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

    public class Adder : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private int arg1;
            public int Arg1
            {
                get { return arg1; }
                set
                {
                    arg1 = value;
                    if (PropertyChanged != null)
                    {
                        //raise the property changed event handler
                        PropertyChanged(this, new PropertyChangedEventArgs("AnswerValue"));
                    }
                }
            }
            //need arg2   copy arg1 change the value to arg2
            private int arg2;
            public int Arg2
            {
                get { return arg2; }
                set
                {
                    arg2 = value;
                    if (PropertyChanged != null)
                    {
                        //raise the property changed event handler
                        PropertyChanged(this, new PropertyChangedEventArgs("AnswerValue"));
                    }
                }
            }
            // need AnswerValue
            public int AnswerValue
            {
                get { return arg1 + arg2; }
            }
        }
    
}
