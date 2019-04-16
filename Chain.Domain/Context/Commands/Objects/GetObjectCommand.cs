using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.Object
{
    public class GetObjectCommand : ICommand
    {
        public Guid Object { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}