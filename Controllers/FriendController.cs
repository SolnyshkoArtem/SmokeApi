using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmokeApi.Model;

namespace SmokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private Context db;
        public FriendController(Context db)
        {

            this.db = db;

        }

        [HttpPost]
        public IActionResult SendFriend(int userId, int friend)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            var fri = db.Users.FirstOrDefault(x => x.Id == friend);

            Friend b = new Friend()
            {
                FriendUser = fri,
                User = user,
                Accepted = false
            };
            try
            {
                db.Friends.Add(b);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult AcceptFriend(int recId, int friendId)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == recId);
            User fri = db.Users.FirstOrDefault(x => x.Id == friendId);
            Friend b = new Friend()
            {
                FriendUser = user,
                User = fri,
                Accepted = true
            };

            try
            {
                db.Friends.FirstOrDefault(x => x.User == user && x.FriendUser == fri).Accepted = true;

                db.Friends.Add(b);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet(Name = "~/GetFriends")]
        public IActionResult GetFriends(int Reqid)
        {
            var a = db.Friends.Include(x=>x.FriendUser).FirstOrDefault(x => x.UserId == Reqid);
            return new JsonResult(a);
        }
    }
}
