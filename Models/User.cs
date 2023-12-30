namespace Vaccinatedclient.Models
{
    public class User
    {
        public int? ID { get; set; }
        public string user_name { get; set; } = string.Empty;
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public int type { get; set; }
        public string? token { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}

