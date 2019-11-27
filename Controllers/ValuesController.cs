using Microsoft.AspNetCore.Mvc;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController
    {
        public string[] Get()
        {
            return new[] { "Hello", "From", "CodeGym" };
        }
    }
}