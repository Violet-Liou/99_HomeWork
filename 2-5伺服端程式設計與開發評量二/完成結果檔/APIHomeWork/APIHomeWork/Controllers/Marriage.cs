using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIHomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Marriage : ControllerBase
    {
        // GET: api/<Marriage>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string url = "https://openapi.kcg.gov.tw/Api/Service/Get/a182559b-5f14-439d-ad4e-512c0a7291bb"; //這是資料來源

            return new string[] { "value1", "value2" };
        }

        // GET api/<Marriage>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Marriage>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Marriage>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Marriage>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
