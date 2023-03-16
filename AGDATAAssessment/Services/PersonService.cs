using AGDATAAssessment.Data;
using AGDATAAssessment.Data.Models;
using AGDATAAssessment.Interfaces;

namespace AGDATAAssessment.Services
{
    public class PersonService : IPersonService
    {
        private readonly AgDataDbContext _dbcontext;

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
