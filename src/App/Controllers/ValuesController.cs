using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace SampleMvcApp.Controllers
{
    [Route("api/values")]
    public class ValuesController : Controller
    {
        [Authorize]
        [HttpGet]
        public string Get()
        {
            return JsonConvert.SerializeObject(new {
                claims = User.Claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value)).ToList()
            });
        }
        
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
