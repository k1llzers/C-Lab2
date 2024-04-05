using System.Text.RegularExpressions;
using Semytskyi2.Exceptions;
using Semytskyi2.Models;
using Semytskyi2.Repository;
using Semytskyi2.Tools;

namespace Semytskyi2.Service
{
    public class PersonService
    {
        private FileRepository Repository = new FileRepository();
        
        public List<Person> GetAllUsers()
        {
            var res = new List<Person>();
            foreach (var user in Repository.GetAll())
            {
                res.Add(new Person(user.Name, user.Surname, user.Email, user.DateOfBirth));                    
            }
            return res;
        }

        public async Task<bool> AddOrUpdatePerson(Person person)
        {
            int age = DateUtil.CalculateAge(person.DateOfBirth);
            if (age >= 130)
            {
                throw new SoPastDateOfBirthException("Age can`t be more then 130");
            } 
            if (age < 0)
            {
                throw new SoPastDateOfBirthException("You can`t create not birth user");
            } 
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            if(!validateEmailRegex.IsMatch(person.Email))
            {
                throw new EmailValidationException("Incorrect email");
            }
            var dbPerson = new DBPerson(person.Name, person.Surname, person.Email, person.DateOfBirth);
            await Repository.AddOrUpdateAsync(dbPerson);
            return true;
        }
        
        public bool DeleteByEmail(string email)
        {
            Repository.DeleteByEmail(email);
            return true;
        }
    }   
}