using AGDATAAssessment.Data.Models;

namespace AGDATAAssessment.Interfaces
{
    public interface IPersonService
    {
        public IList<Person> GetPeople();
        public IList<Person> Add(Person person);
        public IList<Person> Update(Person person);
        public IList<Person> Delete(int personId);
    }
}
