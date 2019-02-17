using Cranium.Data.Models;
using Cranium.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cranium.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : AController<Category>
    {
        public CategoriesController(IDataService dataService)
            : base(dataService, x => x.Categories)
        {
        }
    }
}
