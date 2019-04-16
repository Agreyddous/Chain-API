using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Flows
{
    public class DeleteFlowCommand : ICommand
    {
        public Guid Flow { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}