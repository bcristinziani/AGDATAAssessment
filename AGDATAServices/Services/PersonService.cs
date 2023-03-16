using AGDATAServices.Data;
using AGDATAServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGDATAServices.Services
{
    public class PersonService
    {
        private readonly AgDataDbContext _dbcontext;

        public PersonService() { 
            _dbcontext = new AgDataDbContext();
        }

        public PersonService(AgDataDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IList<Person> GetPeople()
        {
            return _dbcontext.People.ToList();
        }

        public IList<Person> Add(Person person)
        {
            _dbcontext.People.Add(person);
            _dbcontext.SaveChanges();

            return _dbcontext.People.ToList();
        }

        public IList<Person> Update(Person person)
        {
            _dbcontext.People.Update(person);
            _dbcontext.SaveChanges();

            return _dbcontext.People.ToList();
        }

        public IList<Person> Delete(int personId)
        {
            var person = _dbcontext.People.Where(p => p.PersonId == personId).FirstOrDefault();
            _dbcontext.People.Remove(person);
            _dbcontext.SaveChanges();

            return _dbcontext.People.ToList();
        }
    }
}
