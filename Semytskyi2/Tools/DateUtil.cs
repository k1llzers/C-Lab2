namespace Semytskyi2.Tools
{
    internal class DateUtil
    {
        public static bool CheckAdult(DateTime dateOfBirth)
        {
            int age = CalculateAge(dateOfBirth);
            return age >= 18;
        }

        public static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime currentDate = DateTime.Today;
            int age = currentDate.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > currentDate.AddYears(-age))
            {
                age--;
            }
            return age;
        }
        
        public static bool CheckBirthday(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            if (dateOfBirth.Month == today.Month && dateOfBirth.Day == today.Day)
                return true;
            return false;
        }

        public static ZodiacEnum GetZodiac(DateTime dateOfBirth)
        {
            int day = dateOfBirth.DayOfYear;
            if (day >= 21 && day <= 51)
                return ZodiacEnum.Aquarius;
            else if (day >= 52 && day <= 80)
                return ZodiacEnum.Pisces;
            else if (day >= 81 && day <= 110)
                return ZodiacEnum.Aries;
            else if (day >= 111 && day <= 141)
                return ZodiacEnum.Taurus;
            else if (day >= 142 && day <= 172)
                return ZodiacEnum.Gemini;
            else if (day >= 173 && day <= 204)
                return ZodiacEnum.Cancer;
            else if (day >= 205 && day <= 236)
                return ZodiacEnum.Leo;
            else if (day >= 237 && day <= 266)
                return ZodiacEnum.Virgo;
            else if (day >= 267 && day <= 296)
                return ZodiacEnum.Libra;
            else if (day >= 297 && day <= 326)
                return ZodiacEnum.Scorpio;
            else if (day >= 327 && day <= 355)
                return ZodiacEnum.Sagittarius;
            else
                return ZodiacEnum.Capricorn;
        }
        
        public static ChineseZodiacEnum GetChineseZodiac(DateTime dateOfBirth)
        {
            return (ChineseZodiacEnum) (dateOfBirth.Year % 12);
        }
    }

    public enum ZodiacEnum
    {
        Aries,
        Taurus,
        Gemini,
        Cancer,
        Leo,
        Virgo,
        Libra,
        Scorpio,
        Sagittarius,
        Capricorn,
        Aquarius,
        Pisces,
        All
    }

    public enum ChineseZodiacEnum
    {
        Monkey = 0,
        Rooster = 1,
        Dog = 2,
        Pig = 3,
        Rat = 4,
        Ox = 5,
        Tiger = 6,
        Rabbit = 7,
        Dragon = 8,
        Snake = 9,
        Horse = 10,
        Goat = 11,
        All = 12,
    }
}