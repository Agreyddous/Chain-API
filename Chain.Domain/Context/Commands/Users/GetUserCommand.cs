using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Users
{
    public class GetUserCommand : ICommand
    {
        public Guid User { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}