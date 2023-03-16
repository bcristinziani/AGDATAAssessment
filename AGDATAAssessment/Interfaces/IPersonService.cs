using AGDATAAssessment.Data.Models;

namespace AGDATAAssessment.Interfaces
{
    public interface IPersonService
    {
        public IList<Person> GetPeople();
        public void Add(Person person);
        public void Update(Person person);
        public void Delete(int personId);
    }
}
