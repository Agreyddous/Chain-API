using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectTypes
{
    public class DeleteObjectTypeCommand : ICommand
    {
        public Guid ObjectType { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}