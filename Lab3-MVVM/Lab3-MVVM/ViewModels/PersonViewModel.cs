using Lab3_MVVM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 您可以看到我们为PersonViewModel创建了一个PersonModel。
// 该ViewModel维护对底层Person的引用，我们为依赖存储的底层Person的每个属性创建getter和setter，
// 并在设置属性(property is set)时发送INotifyPropertyChanged事件。
// 注意我们如何使用NotificationBase <Person>类来执行INotificationPropertyChanged的默认实现。
// 我们还使用SetProperty帮助函数来实现我们的getter和setter。
// NotificationBase在编译时使用了一个名为CallerMemberName的System.Runtime.CompilerServices的一个不错的新功能，因此我们不需要管理String类型的属性名称。     
// 使用NotificationBase <T>是一种方便的方式来封装不支持INPC的现有业务对象。
// 这是很好的方法，特别是当业务对象很大时，因为通过能力不需要重复的字段。
// 现在为OrganizationViewModel.cs添加一个新的类，并添加以下替代生成的命名空间和类的代码。
namespace Lab3_MVVM.ViewModels
{
    public class PersonViewModel : NotificationBase<Person> //NotificationBase<Person> = Notification<T>
    {
        public PersonViewModel(Person person = null) : base(person) { }//如果person 没有change,则implement 原本的person类，继承所有属性
                                                                       //如果变了，则使用下面的方法去get, set 
        public String Name
        {
            get { return This.Name; }
            set { SetProperty(This.Name, value, () => This.Name = value); }//这个value是在property changed后改变的值
                                                                           //This.Name = value is method to 前面那个()
                                                                           //This.Name = T field , value = T value
        }
        public int Age
        {
            get { return This.Age; }
            set { SetProperty(This.Age, value, () => This.Age = value); }
        }
    }
}
