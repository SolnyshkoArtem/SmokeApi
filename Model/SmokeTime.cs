namespace SmokeApi.Model
{
    public class SmokeTime
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime When { get; set; }
        public bool IsDone { get; set; }
        public string Commentary {  get; set; }
    }
}
