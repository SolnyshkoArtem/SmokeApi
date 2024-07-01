using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmokeApi.Model;
using System.Web.Helpers;
using System.Xml.Linq;

namespace SmokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private Context db;
        public LoginController(Context db)
        {

            this.db = db;

        }

        [HttpGet(Name = "~/authorize")]

        public IActionResult Authorize(string name, string password)
        {
            var user = db.Users.FirstOrDefault(x => x.Nickname == name);

            if (user == null)
            {
                return NotFound();
            }
            if (user.Password != password)
            {
                return BadRequest();
            }
            return new JsonResult(user);
        }

        [HttpPost(Name = "~/createUser")]


        public IActionResult CreateUser(string nick, string pass)
        {
            User user = new()
            {
                Nickname = nick,
                Password = pass
            };

            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
