namespace ExileRota.Infrastructure.Settings
{
    public static class JwtSettings
    {
        public static readonly string Key = "secret_key_123_xyz_123_!?";
        public static readonly string Issuer = "https://localhost:5000";
        public static readonly int ExpiryMinutes = 5;
    }
}