namespace SmokeApi.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string Password { get; set; }

        public List<Friend> Friends { get; set; }
        public List<SmokeTime> Breaks { get; set; }
    }
}
