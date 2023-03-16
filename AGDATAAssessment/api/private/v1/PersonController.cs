using AGDATAAssessment.Framework;
using AGDATAAssessment.Services;
using AGDATAAssessment.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AGDATAAssessment.Interfaces;

namespace AGDATAAssessment.api
{
    [Route("api/private/person")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IApplicationCache _cache;

        /// <summary>
        /// API methods for managing Person records
        /// </summary>
        /// <param name="personService"></param>
        public PersonController(IPersonService personService, IApplicationCache cache)
        {
            _personService = personService;
            _cache = cache;
        }

        /// <summary>
        /// Gets all people
        /// </summary>
        /// <response code="200">Retrieved list of people</response>
        /// <response code="500">Unable to retrieve list</response>
        /// <returns>Returns list of people</returns>
        
        // GET: api/private/person
        [HttpGet]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        [ProducesResponseType(500)]
        public async Task<IList<Person>> GetPeople()
        {
            var task = await _cache.GetList<Person>(CacheKeys.People);
            return task;
            
        }

        /// <summary>
        /// Adds a new person
        /// </summary>
        /// <param name="person">JSON object representing a Person</param>
        /// <response code="200">Person added</response>
        /// <response code="400">Person has missing/invalid values</response>
        /// <response code="500">Unable to add person</response>
        /// <returns>Returns updated list of people</returns>

        // PUT: api/private/person
        [HttpPut]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IList<Person>> Add(Person person)
        {
            ValidationService.Validate(ModelState);

            _personService.Add(person);

            _cache.Refresh(CacheKeys.People);

            var task = await _cache.GetList<Person>(CacheKeys.People);
            return task;
        }

        /// <summary>
        /// Updates an existing Person record
        /// </summary>
        /// <param name="person">JSON object representing a Person</param>
        /// <response code="200">Person updated</response>
        /// <response code="400">Person has missing/invalid values</response>
        /// <response code="500">Unable to update person</response>
        /// <returns>Returns updated list of people</returns>
        
        // POST: api/private/person/update
        [HttpPost("update")]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IList<Person>> Update(Person person)
        {
            ValidationService.Validate(ModelState);

            _personService.Update(person);

            _cache.Refresh(CacheKeys.People);

            var task = await _cache.GetList<Person>(CacheKeys.People);
            return task;
        }

        /// <summary>
        /// Removes a person
        /// </summary>
        /// <param name="personId">Id of the person to remove</param>
        /// <response code="200">Person deleted</response>
        /// <response code="400">Invalid or missing Person Id</response>
        /// <response code="500">Unable to delete person</response>
        /// <returns>Returns an updated list of people</returns>

        // DELETE: api/private/person/{personId}
        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(IList<Person>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IList<Person>> Delete(int personId)
        {
            ValidationService.Validate(ModelState);

            _personService.Delete(personId);

            _cache.Refresh(CacheKeys.People);

            var task = await _cache.GetList<Person>(CacheKeys.People);
            return task;
        }
    }
}
