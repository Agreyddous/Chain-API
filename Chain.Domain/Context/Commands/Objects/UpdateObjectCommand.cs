using System;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;

namespace Chain.Domain.Context.Commands.Object
{
    public class UpdateObjectCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid Creator { get; set; }
        public Guid Father { get; set; }
        public string Path { get; set; }
        public Guid Type { get; set; }
        public Guid Status { get; set; }
        public Guid Id { get; private set; }
        public string RequestHost { get; private set; }

        public void setObjectId(Guid id) => Id = id;
        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}