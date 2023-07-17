namespace IDO_API.Entities
{
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int loginId { get; set; }
        public string profileUrl { get; set; }
        public Login login { get; set; }    
    }
}
