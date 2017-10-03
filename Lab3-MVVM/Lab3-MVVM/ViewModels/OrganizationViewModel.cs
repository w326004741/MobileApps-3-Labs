using Lab3_MVVM.Data;
using Lab3_MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab3_MVVM.ViewModels
{
    class OrganizationViewModel : NotificationBase
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
        int _SelectedIndex;
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
