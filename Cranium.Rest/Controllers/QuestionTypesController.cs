using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cranium.Data.Models;
using Cranium.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cranium.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypesController : AController<QuestionType>
    {
        public QuestionTypesController(IDataService dataService)
            : base(dataService, x => x.QuestionTypes)
        {
        }

        // GET api/values
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<QuestionType>>> Get([FromQuery] int skip = 0, [FromQuery] int take = -1)
            => await base.Get(skip, take);

        // GET api/values/5
        [HttpGet("{id}")]
        public override async Task<ActionResult<QuestionType>> Get(Guid id)
            => await base.Get(id);

        // POST api/values
        [HttpPost]
        public override async Task<IActionResult> Post([FromBody] QuestionType value)
            => await base.Post(value);

        // PUT api/values/5
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(Guid id, [FromBody] QuestionType value)
            => await base.Put(id,value);

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(Guid id)
            => await base.Delete(id);
    }
}
