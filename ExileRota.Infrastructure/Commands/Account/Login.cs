namespace ExileRota.Infrastructure.Commands.Account
{
    public class Login : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}