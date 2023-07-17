namespace IDO_API.Entities
{
    public class TokenData
    {
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public override string ToString()
        {
            return $"UserId: {UserId}, IsAdmin: {IsAdmin}";
        }
    }
}
