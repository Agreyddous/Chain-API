using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectStatus
{
    public class GetObjectStatusCommandResult : CommandResult
    {
        public GetObjectStatusCommandResult() : base() { }

        public GetObjectStatusCommandResult(int code) : base(code) { }

        public GetObjectStatusCommandResult(int code, Notification notification) : base(code, notification) { }

        public GetObjectStatusCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public GetObjectStatusCommandResult(int code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Name { get; private set; }
    }
}