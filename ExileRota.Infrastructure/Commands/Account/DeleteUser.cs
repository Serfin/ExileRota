using System;

namespace ExileRota.Infrastructure.Commands.Account
{
    public class DeleteUser : ICommand
    {
        public Guid UserId { get; set; }
    }
}