namespace IDO_API.Entities
{
    public class Login
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
    }
}
