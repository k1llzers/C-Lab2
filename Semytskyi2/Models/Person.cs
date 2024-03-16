using Semytskyi2.Tools;

namespace Semytskyi2.Models
{
    public class Person
    {
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;
        private readonly bool _isAdult;
        private readonly bool _isBirthday;
        private readonly ZodiacEnum _zodiacSign;
        private readonly ChineseZodiacEnum _chineseZodiacSign;
        
        public Person(string name, string surname, string email, DateTime dateOfBirth)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _dateOfBirth = dateOfBirth;
            int age = DateUtil.CalculateAge(_dateOfBirth);
            if (age < 0 || age >= 130)
            {
                throw new ArgumentException("Incorrect age");
            }
            _isAdult = DateUtil.CheckAdult(dateOfBirth);
            _isBirthday = DateUtil.CheckBirthday(dateOfBirth);
            _zodiacSign = DateUtil.GetZodiac(dateOfBirth);
            _chineseZodiacSign = DateUtil.GetChineseZodiac(dateOfBirth);
            Thread.Sleep(3000);
        }

        public Person(string name, string surname, DateTime dateOfBirth): this(name, surname, string.Empty, dateOfBirth)
        {
        }
        
        public Person(string name, string surname, string email): this(name, surname, email, DateTime.MinValue)
        {
        }

        public string Name
        {
            get
            { 
                return _name;   
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
                return _surname;   
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
                return _email;   
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
                return _dateOfBirth;   
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
                return _isAdult;   
            }
        }
        
        public bool IsBirthday
        {
            get
            { 
                return _isBirthday;   
            }
        }
        
        public ZodiacEnum Zodiac
        {
            get
            { 
                return _zodiacSign;   
            }
        }
        
        public ChineseZodiacEnum ChineseZodiac
        {
            get
            { 
                return _chineseZodiacSign;   
            }
        }
    }   
}