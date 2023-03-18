using AGDATAAssessment.Data;
using AGDATAAssessment.Data.Models;
using AGDATAAssessment.Framework;
using AGDATAAssessment.Interfaces;
using AGDATAAssessment.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace AGDATATest.Services
{
    public class PersonServiceTest
    {
        private IPersonService _personService;
        private IApplicationCache _cache;

        public PersonServiceTest()
        {
            var options = new DbContextOptionsBuilder<AgDataDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var mockPersonService = new Mock<PersonService>(new AgDataDbContext(options));
            mockPersonService.Setup(x => x.GetPeople()).Returns(new List<Person>());

            _personService = mockPersonService.Object;
            _cache = new ApplicationCache(new MemoryCache(new MemoryCacheOptions()), _personService);
        }

        /*
        [Fact]
        public void GetPeople_ReturnsListOfPeople()
        {
            var people = new List<Person>
            {
                new Person { PersonId = 1, Name = "Ben Cristinziani", AddressLine1 = "360 Old Rail Rd", City = "Mount Airy", State = "NC", Zip = "27030" },
                new Person { PersonId = 2, Name = "John Doe", AddressLine1 = "123 Main St", City = "Huntersville", State = "NC", Zip = "28708" },
                new Person { PersonId = 2, Name = "Mary Maple", AddressLine1 = "555 Maple Ln", City = "Cana", State = "VA", Zip = "24315" }
            };

            _mockDbContext.Setup(x => x.People).Returns((Microsoft.EntityFrameworkCore.DbSet<Person>)people.AsQueryable());

            var result = _personService.GetPeople();

            Assert.Equal(3, result.Count);

            Assert.Equal("Ben Cristinziani", result[0].Name);
            Assert.Equal("360 Old Rail Rd", result[0].AddressLine1);
            Assert.Equal("Mount Airy", result[0].City);
            Assert.Equal("NC", result[0].State);
            Assert.Equal("27030", result[0].Zip);

            Assert.Equal("John Doe", result[0].Name);
            Assert.Equal("123 Main St", result[0].AddressLine1);
            Assert.Equal("Huntersville", result[0].City);
            Assert.Equal("NC", result[0].State);
            Assert.Equal("28708", result[0].Zip);

            Assert.Equal("JMary Maple", result[0].Name);
            Assert.Equal("555 Maple Ln", result[0].AddressLine1);
            Assert.Equal("Cana", result[0].City);
            Assert.Equal("VA", result[0].State);
            Assert.Equal("24315", result[0].Zip);
        }
        */
    }
}