namespace Semytskyi2.Models
{
    public class DBPerson
    {
        public DBPerson(string name, string surname, string email, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }
    }
}