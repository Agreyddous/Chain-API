using System;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;

namespace Chain.Domain.Context.Commands.Flows
{
    public class UpdateFlowCommand : ICommand
    {
        public Guid Owner { get; set; }
        public EOwnerType OwnerType { get; set; }
        public Guid Object { get; set; }
        public string Path { get; set; }
        public Guid Id { get; private set; }
        public string RequestHost { get; private set; }

        public void setFlowId(Guid id) => Id = id;
        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}