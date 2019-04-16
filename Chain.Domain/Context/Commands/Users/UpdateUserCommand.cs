using System;
using System.Collections.Generic;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Users
{
    public class UpdateUserCommand : ICommand
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Guid> Group { get; set; }
        public Guid Id { get; private set; }
        public string RequestHost { get; private set; }

        public void setUserId(Guid id) => Id = id;
        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}