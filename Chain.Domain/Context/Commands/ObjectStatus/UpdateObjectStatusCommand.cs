using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectStatus
{
    public class UpdateObjectStatusCommand : ICommand
    {
        public string Name { get; set; }
        public Guid Id { get; private set; }
        public string RequestHost { get; private set; }

        public void setObjectStatusId(Guid id) => Id = id;
        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}