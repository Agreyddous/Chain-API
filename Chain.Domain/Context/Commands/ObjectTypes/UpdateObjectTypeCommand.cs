using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectTypes
{
    public class UpdateObjectTypeCommand : ICommand
    {
        public string Name { get; set; }
        public Guid Id { get; private set; }
        public string RequestHost { get; private set; }

        public void setObjectTypeId(Guid id) => Id = id;
        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}