namespace IDO_API.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LoginId { get; set; }
        public string ProfileUrl { get; set; }
        public Login Login { get; set; }    
    }
}
