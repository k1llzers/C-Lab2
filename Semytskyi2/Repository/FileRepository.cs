using System.IO;
using System.Text.Json;
using Semytskyi2.Exceptions;
using Semytskyi2.Models;
using Semytskyi2.Tools;

namespace Semytskyi2.Repository
{
    public class FileRepository
    {
        private static readonly string BaseFolder =
            Path.Combine("D:\\Users\\Sasha\\RiderProjects\\Semytskyi2\\Storage");
        
        public FileRepository()
        {
            if (!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);
                DataInitializer.getDataForInit().ForEach(AddOrUpdate);   
            }
        }
        
        public async Task AddOrUpdateAsync(DBPerson obj)
        {
            var stringObj = JsonSerializer.Serialize(obj);
            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.Email), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }
        
        public void AddOrUpdate(DBPerson obj)
        {
            var stringObj = JsonSerializer.Serialize(obj);
            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.Email), false))
            {
                sw.Write(stringObj);
            }
        }
        
        public void DeleteByEmail(string email)
        {
            if (File.Exists(Path.Combine(BaseFolder, email)))
                File.Delete(Path.Combine(BaseFolder, email));
            else
                throw new NoSuchPersonException("Can't find person by email: " + email);
        }
        //
        // public async Task<DBPerson> GetAsync(string email)
        // {
        //     string stringObj = null;
        //     string filePath = Path.Combine(BaseFolder, email);
        //
        //     if (!File.Exists(filePath))
        //         return null;
        //
        //     using (StreamReader sw = new StreamReader(filePath))
        //     {
        //         stringObj = await sw.ReadToEndAsync();
        //     }
        //
        //     return JsonSerializer.Deserialize<DBPerson>(stringObj);
        // }
        
        public async Task<List<DBPerson>> GetAllAsync()
        {
            var res = new List<DBPerson>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;
                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = await sw.ReadToEndAsync();
                }
                res.Add(JsonSerializer.Deserialize<DBPerson>(stringObj));
            }
            return res;
        }
        
        public List<DBPerson> GetAll()
        {
            var res = new List<DBPerson>();
            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;
                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = sw.ReadToEnd();
                }
                res.Add(JsonSerializer.Deserialize<DBPerson>(stringObj));
            }
            return res;
        }
    }   
}