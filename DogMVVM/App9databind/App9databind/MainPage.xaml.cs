using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App9databind
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            // when the page is loaded, load the data
            //this.Loaded += MainPage_Loaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // if the data is already loaded, then exit the method
            // 如果数据已经加载，则退出改方法
            if (_myList != null)
                return;

            // else, load the data from a local source - Data/myDogs.txt 否则，从本地数据资源加载数据 - Data/myDogs.txt
            // could load remote from server 能够从服务器远程加载
            // instantiate the list - _myList   实例化列表 - _myList
            _myList = new List<clsDogs>();
            loadLocalData();
            // list view shows a list of items  列表视图显示项目对象
            // the source is the list of objects for dogs 资源来自狗的对象列表
            lvDogs.ItemsSource = _myList;

            // get the string from App.xaml.cs  从App.xaml.cs获取字符串 : myapp.strHello 获取到 tblTitle
            // get a reference/pointer to the instance  获取一个引用/指向当前运行的应用程序的实例
            // of the current app that is running
            App myapp = App.Current as App;
            tblTitle.Text = myapp.strHello;

        }

        #region Copy to App.xaml.cs file for global list.
        private async void loadLocalData()
        {
            /* &
             *  1.  get the file with the data  使用数据获取文件
             *  2.  read the text to a JSON Array  将文本读到JSON数组
             *  3.  parse the JSON Array and create the list of object 解析JSON数组并创建对象列表
             */
            //1. (like: FILE *fptr;  fptr = fopen("myDogs.txt", "r");
            var dogsFile = await
                Package.Current.InstalledLocation.GetFileAsync("Data\\myDogs.txt");
            var fileText = await FileIO.ReadTextAsync(dogsFile);

            // now have a block of text in fileText 现在在fileText中有一个文本块
            // send that to a json array to start making sense  发送到一个json数组开始有意义

            try
            {
                var dogsJArray = JsonArray.Parse(fileText);
                createListOfDogs(dogsJArray);
                tblTitle.Text = _myList.Count().ToString() + " Dog Breeds";
                //Count(): Return序列中的元素个数 e.g. 在Title上显示：13 Dog Breeds
            }
            catch (Exception exJA)
            {
                MessageDialog dialog = new MessageDialog(exJA.Message);
                await dialog.ShowAsync();
            }

        }

        private void createListOfDogs(JsonArray jsonData)
        {
            // foreach循环用于列举出集合中所有的元素，foreach语句中的表达式由关键字in隔开的两个项组成。
            // in右边的项是集合名(jsonData)，in左边的项是变量名(item)，用来存放该集合中的每个元素
            foreach (var item in jsonData)
            {
                // get the object
                var obj = item.GetObject();

                clsDogs dog = new clsDogs();

                // get each key value pair and sort it to the appropriate elements
                // of the class  获取每个键值对并将其排序到类的相应元素
                foreach (var key in obj.Keys)
                {
                    IJsonValue value;
                    //if(! a) = 如果a = 0,则为假，只要a != 0，其余都为真，则程序可以进行下去
                    if (!obj.TryGetValue(key, out value)) //TryGetValue:获取与指定键相关联的值
                        continue;

                    switch (key)
                    {
                        case "breed": // based on generic object key 基于通用对象键
                            dog.myBreedName = value.GetString();
                            break;
                        case "origin":
                            dog.origin = value.GetString();
                            break;
                        case "category":
                            dog.category = value.GetString();
                            break;
                        case "activity":
                            dog.activity = value.GetString();
                            break;
                        case "grooming":
                            dog.grooming = value.GetString();
                            break;
                        case "image":
                            dog.imgBreed = value.GetString();
                            break;
                    }
                } // end foreach (var key in obj.Keys)

                _myList.Add(dog); //Add到列表中

            } // end foreach (var item in array)
        }

        #endregion
        List<clsDogs> _myList;
        //数据绑定TAPPED事件代码
        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel curr = (StackPanel)sender;
            // fill in the values on the picture etc
            tblOneBreed.Text = _myList[lvDogs.SelectedIndex].myBreedName;
            tblOneCategory.Text = _myList[lvDogs.SelectedIndex].category;
            tblOneOrigin.Text = _myList[lvDogs.SelectedIndex].origin;

            // get the picture
            // check for the file existing.
            // if( fileexists(_myList[lvdogs.SelectedIndex].imgSource )
            string fileString = "ms-appx:///" +  _myList[lvDogs.SelectedIndex].imgBreed; 
            if ( !File.Exists(_myList[lvDogs.SelectedIndex].imgBreed) )
            {
                fileString = "ms-appx:///Images/images.jpe";
            }

            // create a uri from the string in the class   从类中的字符串创建一个uri
            // uri : 统一资源标识符(uniform resource identifier)，用来唯一的标识一个资源
            Uri myUri = new Uri(fileString,
                                UriKind.Absolute);
            // create a bitmap fromt he uri 从他的uri创建一个位图, Bitmap 是用于处理由像素数据定义的图像的对象。
            BitmapImage myBitmap = new BitmapImage(myUri);
            // use the bitmap as the source for the image. 使用位图作为图像的源。
            imgOneDog.Source = myBitmap;




        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Page2));//创建Page2,按下NextPage就会导航到Page2页面，显示关于狗种类的详细信息
        }
    }
}
