using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIHomeWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inn : ControllerBase
    {

        // GET: api/<Inn>
        [HttpGet]
        public async Task<IEnumerable<Inn>> Get()
        {
            string url = "https://openapi.kcg.gov.tw/Api/Service/Get/2911f851-59dd-4d7b-b6c7-b750ebb88728"; //這是資料來源

            HttpClient client = new HttpClient();

            var resp = await client.GetStringAsync(url);  //取得API的回應的Json字串

            // 先定義一個包裝 data 的類型
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(resp);
            var dataJson = jsonObj.data.ToString();

            // 將 data 欄位轉換為 IEnumerable<Inn>
            var collection = JsonConvert.DeserializeObject<IEnumerable<Inn>>(dataJson);

            return collection;
        }

        // POST api/<Inn>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Inn>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Inn>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
