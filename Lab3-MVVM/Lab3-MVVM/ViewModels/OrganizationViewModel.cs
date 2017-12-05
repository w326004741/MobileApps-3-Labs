using Lab3_MVVM.Data;
using Lab3_MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

//这实际上是应用程序的核心。我们来仔细看看。我们为组织类创建ViewModel并将其称为OrganizationViewModel。
//像PersonViewModel一样，它也从NotificationBase继承，提供INotifyPropertyChanged的默认实现。
//接下来，我们添加一个名为People的ObservableCollection <PersonViewModel>属性，以提供List <Person>的XAML可绑定实现。
//这个ObservableCollection真的没有什么比通过底层模型加载更多的东西了。
//我们在构造函数中初始化集合，当我们更改ObservableCollection时，我们更新模型。
//接下来，我们添加两个互相连接的字段 - 一个SelectedIndex，我们可以从XAML和SelectedIndex获取和设置SelectedPerson。
//当用户点击列表时，我们的用户界面视图将移动SelectedIndex。
//一旦用户选择了一个人，该视图使用户能够更新SelectedPerson的属性。
//注意，当我们更新SelectedIndex时，我们还在SelectedPerson上触发一个属性改变的通知。
namespace Lab3_MVVM.ViewModels
{
    //继承NotificationBase去提供INotifyPropertyChanged的默认接口，间接实现INotifyPropertyChanged接口.
    public class OrganizationViewModel : NotificationBase
    {
        Organization organization;
        public OrganizationViewModel(String name)
        {
            organization = new Organization(name);
            _SelectedIndex = -1;
            //load the database
            foreach (var person in organization.People)
            {
                var np = new PersonViewModel(person);
                np.PropertyChanged += Person_OnNotifyPropertyChanged;
                _People.Add(np);
            }
        }
        //ObservableCollection: 代表一个动态数据集合，当项目被添加，删除或整个列表刷新时提供通知.
        // 添加一个名为People的ObservableCollection<PersonViewModel>属性，以提供List<Persion>的XAML可以绑定接口
        ObservableCollection<PersonViewModel> _People
            = new ObservableCollection<PersonViewModel>();
        public ObservableCollection<PersonViewModel> People
        {
            get { return _People; }
            set { SetProperty(ref _People, value); }
        }

        String _Name;
        public String Name
        {
            get { return organization.Name; }
        }
        int _SelectedIndex; // MainPgae.xaml内的SelectedIndex
        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set { if (SetProperty(ref _SelectedIndex, value))
                { RaisePropertyChanged(nameof(SelectedIndex)); } }
        }
        public PersonViewModel SelectedPerson
        {
            get { return (_SelectedIndex >= 0) ? _People[_SelectedIndex] : null; }
        }

        //我们为Add和Delete添加操作，这些操作只是OrganizationViewModel上的普通方法 - 没有花哨的命令或其他所需的东西。 
        //视图将能够响应用户输入绑定到这些操作。 正如你所看到的，他们将这些变化发送到集合和底层模型。 
        //该模型的最后一个重要部分要注意：当一个属性在PersonViewModel对象之一上被改变时，它们触发一个事件。 
        //我们在OrganizationViewModel中捕获这个事件，并用来发送更新到底层模型，最终发送到数据服务。
        //要启用这个功能，我们在Add操作中添加person.PropertyChanged + = Person_OnNotifyPropertyChanged。
        public void Add()
        {
            var person = new PersonViewModel();
            person.PropertyChanged += Person_OnNotifyPropertyChanged;
            People.Add(person);
            organization.Add(person);
            SelectedIndex = People.IndexOf(person);
        }
        public void Delete()
        {
            if (SelectedIndex != -1)
            {
                var person = People[SelectedIndex];
                People.RemoveAt(SelectedIndex);
                organization.Delete(person);
            }
        }

        private void Person_OnNotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            organization.Update((PersonViewModel)sender);
        }
    }
}
