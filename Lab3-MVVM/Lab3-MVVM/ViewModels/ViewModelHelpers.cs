using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Lab3_MVVM.ViewModels
{
    // 定义一个NotificationBase类，这个类的作用是实现了INotifyPropertyChanged接口，
    // 目的是绑定数据属性。实现了它这个接口，数据属性发生变化后才会去通知UI。
    // 如果想在数据属性发生变化前知道，需要实现INotifyPropertyChanging接口。
    public class NotificationBase : INotifyPropertyChanged
    {
        // 第一点
        public event PropertyChangedEventHandler PropertyChanged;

        // SetField(设置字段) (Name, value); // where there is a data member
        // 定义ViewModel时定义一个SetProperty通用方法，第一个参数标记为ref，表示Property对应的字段，第二个参数为set提供的value。
        // 对于在属性中调用CallerMemberName所标记的函数即可获取属性名称，通过这种方式可以简化 INotifyPropertyChanged 接口的实现。
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] String property = null)
                                      //ref T This.Name, T value               String Name / Age = null(value)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(property);
            return true;
        }
        // SetField(()=> somewhere.Name = value; somewhere.Name, value) 
        // Advanced case where you rely on another property
        // Action : 封装一个没有参数且不返回值的方法
        // 定义ViewModel时定义一个SetProperty通用方法，第一个参数标记为ref，表示Property对应的字段，第二个参数为set提供的value。
        protected bool SetProperty<T>(T currentValue, T newValue, Action DoSet, [CallerMemberName] String property = null)
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
            DoSet.Invoke();//Sider
            RaisePropertyChanged(property);
            return true;
        }

        protected void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }

    public class NotificationBase<T> : NotificationBase where T : class, new()// : is implements , NotificationBase<Person>
    {
        protected T This;
        public static implicit operator T(NotificationBase<T> thing) // 隐式声明NotificationBase<T>类的转换类型的方法
        { return thing.This; }              

        public NotificationBase(T thing = null)
        {
            This = (thing == null) ? new T() : thing;
            //if thing == null --> new T()
            //else return thing.This   一个IF判断语句
        }

    }
    
}
