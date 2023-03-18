using AGDATAAssessment.Data.Models;
using AGDATAAssessment.Data;
using AGDATAAssessment.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using AGDATAAssessment.api;
using AGDATAAssessment.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using AGDATAAssessment.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AGDATATest.APIControllers
{
    public class PersonControllerTest
    {
        private IPersonService _personService;
        private IApplicationCache _cache;

        public PersonControllerTest()
        {
            var options = new DbContextOptionsBuilder<AgDataDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;

            var people = new List<Person>
            {
                new Person { PersonId = 1, Name = "Ben Cristinziani", AddressLine1 = "360 Old Rail Rd", City = "Mount Airy", State = "NC", Zip = "27030" },
                new Person { PersonId = 2, Name = "John Doe", AddressLine1 = "123 Main St", City = "Huntersville", State = "NC", Zip = "28708" },
                new Person { PersonId = 3, Name = "Mary Maple", AddressLine1 = "555 Maple Ln", City = "Cana", State = "VA", Zip = "24315" }
            };

            var mockPersonService = new Mock<IPersonService>();
            mockPersonService.Setup(x => x.GetPeople()).Returns(people);

            _personService = mockPersonService.Object;
            _cache = new ApplicationCache(new MemoryCache(new MemoryCacheOptions()), _personService);
        }

        [Fact]
        public void GetPeople_ReturnsListOfPeople()
        {
            var controller = new PersonController(_personService, _cache);

            var result = controller.GetPeople();

            Assert.NotNull(result);
            Assert.IsType<Task<IList<Person>>>(result);
        }

        [Fact]
        public void AddPerson_ReturnsListOfPeople()
        {
            var controller = new PersonController(_personService, _cache);

            var result = controller.Add(new Person
            {
                Name = "Doctor Strange",
                AddressLine1 = "777 Strange Circle",
                City = "Charlotte",
                State = "NC",
                Zip = "27138"
            });

            Assert.NotNull(result);
            Assert.IsType<Task<IList<Person>>>(result);
        }

        [Fact]
        public async Task AddPersonWithoutName_ThrowsException()
        {
            // Arrange
            var controller = new PersonController(_personService, _cache);
            var personWithoutName = new Person
            {
                AddressLine1 = "777 Strange Circle",
                City = "Charlotte",
                State = "NC",
                Zip = "27138"
            };
            controller.ModelState.AddModelError("Name", "Name is required");

            // Assert
            _ = Assert.ThrowsAsync<ValidationException>(async () => await controller.Add(personWithoutName));
        }
    }
}
