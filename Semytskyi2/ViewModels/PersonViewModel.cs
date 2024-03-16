using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Semytskyi2.Models;
using Semytskyi2.Tools;

namespace Semytskyi2.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private Person? _person;
        private RelayCommand<object> _proceedCommand;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;
        private bool _isEnable = true;

        public string Name
        {
            get
            {
                return _person.Name;
            }
            set
            {
                _name = value;
            }
        }
        
        public string Surname
        {
            get
            {
                return _person.Surname;
            }
            set
            {
                _surname = value;
            }
        }
        
        public string Email
        {
            get
            {
                return _person.Email;
            }
            set
            {
                _email = value;
            }
        }
        
        public DateTime DateOfBirth
        {
            get
            {
                return _person.DateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
            }
        }
        
        public bool IsAdult
        {
            get
            {
                return _person.IsAdult;
            }
        }
        
        public bool IsBirthday
        {
            get
            { 
                return _person.IsBirthday;   
            }
        }
        
        public ZodiacEnum Zodiac
        {
            get
            { 
                return _person.Zodiac;   
            }
        }
        
        public ChineseZodiacEnum ChineseZodiac
        {
            get
            { 
                return _person.ChineseZodiac;   
            }
        }

        public bool IsEnable
        {
            get
            {
                return _isEnable;
            }
            set
            {
                _isEnable = value;
                OnPropertyChanged(nameof(IsEnable));
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _proceedCommand ??= new RelayCommand<object>(_ => Proceed(), CanProceed);
            }
        }

        private bool CanProceed(object parameter)
        {
            return !(String.IsNullOrWhiteSpace(_email) || String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_surname) || _dateOfBirth.Equals(DateTime.MinValue));
        }

        private async void Proceed()
        {
            try
            {
                IsEnable = false;
                _person = await Task.Run(() => new Person(_name, _surname, _email, _dateOfBirth));
            }
            catch (ArgumentException ex)
            {
                _person = null;
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                OnPropertyChangedAllProperty();
                IsEnable = true;
            }
            if (IsBirthday)
            {
                MessageBox.Show("Congratulations! I wish you to get the max mark for this lab! Happy Birthday!");
            }
        }

        private void OnPropertyChangedAllProperty()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(DateOfBirth));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(IsAdult));
            OnPropertyChanged(nameof(IsBirthday));
            OnPropertyChanged(nameof(Zodiac));
            OnPropertyChanged(nameof(ChineseZodiac));
        }
    }   
}