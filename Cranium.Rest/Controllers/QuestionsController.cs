using Cranium.Data.Models;
using Cranium.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cranium.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : AController<Question>
    {
        public QuestionsController(IDataService dataService)
            : base(dataService, x => x.Questions)
        {
        }
    }
}