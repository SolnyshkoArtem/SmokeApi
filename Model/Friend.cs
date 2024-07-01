using System.ComponentModel.DataAnnotations.Schema;

namespace SmokeApi.Model
{
    public class Friend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int FriendId { get; set; }
        [ForeignKey("FriendId")]
        public User FriendUser { get; set; }
        public bool Accepted { get; set; }
    }
}
