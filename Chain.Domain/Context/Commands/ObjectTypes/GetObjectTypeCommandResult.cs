using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectTypes
{
    public class GetObjectTypeCommandResult : CommandResult
    {
        public GetObjectTypeCommandResult() : base() { }

        public GetObjectTypeCommandResult(int code) : base(code) { }

        public GetObjectTypeCommandResult(int code, Notification notification) : base(code, notification) { }

        public GetObjectTypeCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public GetObjectTypeCommandResult(int code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Name { get; private set; }
    }
}