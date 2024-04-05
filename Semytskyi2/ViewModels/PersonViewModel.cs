using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Semytskyi2.Exceptions;
using Semytskyi2.Models;
using Semytskyi2.Service;
using Semytskyi2.Tools;

namespace Semytskyi2.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _persons;
        private RelayCommand<object> _proceedCommand;
        private RelayCommand<Person> _deleteCommand;
        private RelayCommand<Person> _updateCommand;
        private RelayCommand<object> _filterCommand;
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;
        
        private string _filterName = "";
        private string _filterSurname = "";
        private string _filterEmail = "";
        private DateTime _filterToDateOfBirth = DateTime.Now;
        private DateTime _filterFromDateOfBirth = new(1950, 1, 1);
        private bool _filterIsAdult;
        private bool _filterIsNotAdult;
        private bool _filterIsAdultBoth = true;
        private bool _filterIsBirthday;
        private bool _filterIsNotBirthday;
        private bool _filterIsBirthdayBoth = true;
        private ZodiacEnum _filterZodiacSighn = ZodiacEnum.All;
        private ChineseZodiacEnum _filterChineseZodiacSighn = ChineseZodiacEnum.All;
        
        private bool _isEnable = true;
        private PersonService _personService;

        public PersonViewModel()
        {
            _personService = new PersonService();
            _persons = new ObservableCollection<Person>(_personService.GetAllUsers());
        }

        public string Name
        {
            set
            {
                _name = value;
            }
        }
        
        public string Surname
        {
            set
            {
                _surname = value;
            }
        }
        
        public string Email
        {
            set
            {
                _email = value;
            }
        }
        
        public DateTime DateOfBirth
        {
            set
            {
                _dateOfBirth = value;
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

        public ObservableCollection<Person> Persons
        {
            get
            {
                return _persons;
            }
            set
            {
                _persons = value;
            }
        }

        public string FilterName
        {
            get
            {
                return _filterName;
            }
            set
            {
                _filterName = value;
            }
        }

        public string FilterSurname
        {
            get
            {
                return _filterSurname;
            }
            set
            {
                _filterSurname = value;
            }
        }

        public string FilterEmail
        {
            get
            {
                return _filterEmail;
            }
            set
            {
                _filterEmail = value;
            }
        }

        public DateTime FilterToDateOfBirth
        {
            get
            {
                return _filterToDateOfBirth;
            }
            set
            {
                _filterToDateOfBirth = value;
            }
        }

        public DateTime FilterFromDateOfBirth
        {
            get
            {
                return _filterFromDateOfBirth;
            }
            set
            {
                _filterFromDateOfBirth = value;
            }
        }

        public bool FilterIsAdult
        {
            get
            {
                return _filterIsAdult;
            }
            set
            {
                _filterIsAdult = value;
            }
        }

        public bool FilterIsNotAdult
        {
            get
            {
                return _filterIsNotAdult;
            }
            set
            {
                _filterIsNotAdult = value;
            }
        }

        public bool FilterIsAdultBoth
        {
            get
            {
                return _filterIsAdultBoth;
            }
            set
            {
                _filterIsAdultBoth = value;
            }
        }

        public bool FilterIsBirthday
        {
            get
            {
                return _filterIsBirthday;
            }
            set
            {
                _filterIsBirthday = value;
            }
        }

        public bool FilterIsNotBirthday
        {
            get
            {
                return _filterIsNotBirthday;
            }
            set
            {
                _filterIsNotBirthday = value;
            }
        }

        public bool FilterIsBirthdayBoth
        {
            get
            {
                return _filterIsBirthdayBoth;
            }
            set
            {
                _filterIsBirthdayBoth = value;
            }
        }

        public ZodiacEnum FilterZodiacSighn
        {
            get
            {
                return _filterZodiacSighn;
            }
            set
            {
                _filterZodiacSighn = value;
            }
        }

        public ChineseZodiacEnum FilterChineseZodiacSighn
        {
            get
            {
                return _filterChineseZodiacSighn;
            }
            set
            {
                _filterChineseZodiacSighn = value;
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
        
        public RelayCommand<Person> DeleteCommand
        {
            get
            {
                return _deleteCommand ??= new RelayCommand<Person>(person => Delete(person));
            }
        }
        
        public RelayCommand<Person> UpdateCommand
        {
            get
            {
                return _updateCommand ??= new RelayCommand<Person>(person => Update(person));
            }
        }
        
        public RelayCommand<object> FilterCommand
        {
            get
            {
                return _filterCommand ??= new RelayCommand<object>(person => Filter());
            }
        }

        private bool CanProceed(object parameter)
        {
            return !(String.IsNullOrWhiteSpace(_email) || String.IsNullOrWhiteSpace(_name) || String.IsNullOrWhiteSpace(_surname) || _dateOfBirth.Equals(DateTime.MinValue));
        }

        private async void Proceed()
        {
            Person person;
            try
            {
                IsEnable = false;
                person = await Task.Run(() => new Person(_name, _surname, _email, _dateOfBirth));
                await _personService.AddOrUpdatePerson(person);
                _persons.Add(person);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Filter();
                IsEnable = true;
            }
        }
        
        private void Delete(Person person)
        {
            try
            {
                _personService.DeleteByEmail(person.Email);
                _persons.Remove(person);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            finally
            {
                Filter();
                IsEnable = true;
            }
        }

        private async void Update(Person person)
        {
            try
            {
                IsEnable = false;
                await _personService.AddOrUpdatePerson(person);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Filter();
                IsEnable = true;
            }
        }
        
        private void Filter()
        {
            var persons = _personService.GetAllUsers();
            var filtered = persons.Where(person => person.Email.ToLower().Contains(_filterEmail.ToLower()))
                .Where(person => person.Name.ToLower().Contains(_filterName.ToLower()))
                .Where(person => person.Surname.ToLower().Contains(_filterSurname.ToLower()))
                .Where(person => person.DateOfBirth >= _filterFromDateOfBirth)
                .Where(person => person.DateOfBirth <= _filterToDateOfBirth);
            if (!_filterIsAdultBoth)
            {
                if (_filterIsAdult)
                {
                    filtered = filtered.Where(person => person.IsAdult);
                }
                else
                {
                    filtered = filtered.Where(person => !person.IsAdult);
                }
            }
            if (!_filterIsBirthdayBoth)
            {
                if (_filterIsBirthday)
                {
                    filtered = filtered.Where(person => person.IsBirthday);
                }
                else
                {
                    filtered = filtered.Where(person => !person.IsBirthday);
                }
            }
            if (_filterZodiacSighn != ZodiacEnum.All)
            {
                filtered = filtered.Where(person => person.Zodiac == _filterZodiacSighn);
            }
            if (_filterChineseZodiacSighn != ChineseZodiacEnum.All)
            {
                filtered = filtered.Where(person => person.ChineseZodiac == _filterChineseZodiacSighn);
            }
            Persons = new ObservableCollection<Person>(filtered);
            OnPropertyChangedAllProperty();
        }

        private void OnPropertyChangedAllProperty()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Surname));
            OnPropertyChanged(nameof(DateOfBirth));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(Persons));
        }
    }   
}