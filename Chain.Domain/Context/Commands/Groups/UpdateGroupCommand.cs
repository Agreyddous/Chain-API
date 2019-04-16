using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Groups
{
    public class UpdateGroupCommand : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Id { get; private set; }
        public string RequestHost { get; private set; }

        public void setGroupId(Guid id) => Id = id;
        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}