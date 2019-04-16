using System;
using Chain.Shared.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectTypes
{
    public class NewObjectTypeCommand : ICommand
    {
        public string Name { get; set; }
        public string RequestHost { get; private set; }

        public void setRequestHost(string requestHost) => RequestHost = requestHost;
    }
}