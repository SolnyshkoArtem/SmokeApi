using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmokeApi.Model;

namespace SmokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakController : ControllerBase
    {
        private Context db;
        public BreakController(Context db)
        {

            this.db = db;

        }

        [HttpGet]
        public IActionResult GetUserBreaks(int Userid)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == Userid);
            var a = db.SmokeTimes.Where(x => x.User == user).ToList();
            var friends = db.Friends.Where(x=>x.User == user).Select(x=>x.FriendUser).ToList();
            List<SmokeTime> b = new ();
            foreach (var item in friends)
            {
                var k = db.SmokeTimes.Where(x => x.User == item);
                b.AddRange(k);
            }
            a.AddRange(b);
            return new JsonResult(a);
        }

        [HttpPost]
        public IActionResult AddBreak(int id)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == id);

            SmokeTime time = new SmokeTime()
            {
                User = user,
                IsDone = false,
                When = DateTime.Now
            };
            try
            {
                db.SmokeTimes.Add(time);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult PutBreak(int id)
        {
            var a = db.SmokeTimes.FirstOrDefault(x=>x.Id== id);
            try
            {
                a.IsDone = true;
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
           
        }
    }
}
