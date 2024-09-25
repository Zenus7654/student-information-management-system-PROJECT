namespace SIMS.Models
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // You should hash and salt passwords in a real application
    }
}
