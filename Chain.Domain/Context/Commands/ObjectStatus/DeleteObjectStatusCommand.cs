using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectStatus
{
    public class DeleteObjectStatusCommand : ICommand
    {
        public Guid ObjectStatus { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}