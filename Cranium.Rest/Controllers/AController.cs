using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cranium.Data.Models.Bases;
using Cranium.Data.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cranium.Rest.Controllers
{
    [ApiController]
    public abstract class AController<T> : ControllerBase where T : class, IWithId
    {
        private readonly IDataService _dataService;


        protected AController(IDataService dataService, Func<DataContext, DbSet<T>> setSelector)
        {
            SetSelector = setSelector;
            _dataService = dataService;
        }


        protected Func<DataContext, DbSet<T>> SetSelector { get; }


        // GET api/values
        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<T>>> Get([FromQuery] int skip = 0, [FromQuery] int take = -1)
        {
            var questions = await _dataService.GetAsync(SetSelector, skip, take);
            return Ok(questions);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> Get(Guid id)
        {
            var question = await _dataService.GetAsync(SetSelector, id);
            return Ok(question);
        }

        // POST api/values
        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] T value)
        {
            await _dataService.CreateAsync(SetSelector, value);
            return Created($"{HttpContext.Request.GetDisplayUrl()}/{value.Id}", value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(Guid id, [FromBody] T value)
        {
            value.Id = id;
            await _dataService.UpdateAsync(SetSelector, value);
            return Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            var item = await _dataService.GetAsync(SetSelector, id);
            await _dataService.DeleteAsync(SetSelector, item);
            return Ok(id);
        }
    }
}