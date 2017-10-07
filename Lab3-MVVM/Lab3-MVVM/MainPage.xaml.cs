using Lab3_MVVM.Models;
using Lab3_MVVM.ViewModels;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lab3_MVVM
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage() 
        {
            this.InitializeComponent();
            Organization = new OrganizationViewModel("Office");
         
        }

     
        public OrganizationViewModel Organization { get; set; }
    }
    
}
