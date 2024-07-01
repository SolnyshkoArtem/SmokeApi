using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmokeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        Context context1;
        public AdminController(Context context)
        {
            context1 = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var use = context1.Users.Include(x=>x.Friends).Include(x=>x.Breaks).ToList();
            var bre = context1.SmokeTimes.Include(x=>x.User).ToList();
            var fr = context1.Friends.ToList();

            var all = new
            {
                use,
                bre,
                fr
            };

            return new JsonResult(all);
        }

        [HttpDelete]
        public void Delete(int table, int id)
        {
            if(table == 1)
            {
                var a = context1.Users.FirstOrDefault(x => x.Id == id);
                context1.Users.Remove(a);
            }
            if (table == 2)
            {
                var a = context1.Friends.FirstOrDefault(x => x.Id == id);
                context1.Friends.Remove(a);
            }
            if (table == 3)
            {
                var a = context1.SmokeTimes.FirstOrDefault(x => x.Id == id);
                context1.SmokeTimes.Remove(a);
            }
        }
    }
}
