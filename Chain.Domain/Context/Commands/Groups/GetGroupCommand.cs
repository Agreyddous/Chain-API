using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Groups
{
    public class GetGroupCommand : ICommand
    {
        public Guid Group { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}