using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectStatus
{
    public class NewObjectStatusCommand : ICommand
    {
        public string Name { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}