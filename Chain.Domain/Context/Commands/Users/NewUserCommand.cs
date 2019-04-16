using System;
using System.Collections.Generic;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Users
{
    public class NewUserCommand : ICommand
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Guid> Groups { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}