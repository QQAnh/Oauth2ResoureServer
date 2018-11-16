using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Oauth2ResourceServer.Models;

namespace Oauth2ResourceServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly string OAUTH2_SERVER = "https://oauth2serverapt.azurewebsites.net";
        private readonly string OAUTH2_AUTHORIZATION_URI = "/Oauth2/CheckToken";
        private readonly string SCOPES = "http://basicscope.com,http://songresourcescope.com";
        // GET api/values
        [EnableCors("MyPolicy")]
        [HttpGet]
        public ActionResult<IEnumerable<Song>> Get([FromHeader(Name = "Authorization")] string authorization)
        {
            return null;
            authorization = authorization.Replace("Basic ", "");
            var client = new HttpClient();
            var result = client.GetAsync(OAUTH2_SERVER + OAUTH2_AUTHORIZATION_URI + "?tokenKey=" + authorization + "&scopes=" + SCOPES)
                .Result;            
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return new List<Song>()
                {
                    new Song()
                    {
                        Name = "Trái Tim Em Cũng Biết Đau",
                        Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui922/TraiTimEmCungBietDau-BaoAnh-4472281.mp3",
                        Thumbnail = "https://avatar-nct.nixcdn.com/song/2017/08/24/3/f/b/f/1503547119985.jpg"
                    },
                    new Song()
                    {
                        Name = "Phai Dấu Cuộc Tình",
                        Link = "https://c1-ex-swe.nixcdn.com/NhacCuaTui953/PhaiDauCuocTinh-BichPhuong-3666934.mp3",
                        Thumbnail = "https://avatar-nct.nixcdn.com/singer/avatar/2017/12/05/0/c/8/a/1512407047114.jpg"
                    },
                };
            }
            return StatusCode((int)result.StatusCode);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
