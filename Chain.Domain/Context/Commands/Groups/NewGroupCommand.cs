using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Groups
{
    public class NewGroupCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}