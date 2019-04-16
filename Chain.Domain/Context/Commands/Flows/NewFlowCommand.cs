using System;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;

namespace Chain.Domain.Context.Commands.Flows
{
    public class NewFlowCommand : ICommand
    {
        public Guid Owner { get; set; }
        public EOwnerType OwnerType { get; set; }
        public Guid Object { get; set; }
        public string Path { get; set; }
        public string RequestHost { get; set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}