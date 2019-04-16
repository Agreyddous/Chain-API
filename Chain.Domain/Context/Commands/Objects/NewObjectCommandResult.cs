using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.Object
{
    public class NewObjectCommandResult : CommandResult
    {
        public NewObjectCommandResult() : base() { }

        public NewObjectCommandResult(int code) : base(code) { }

        public NewObjectCommandResult(int code, Notification notification) : base(code, notification) { }

        public NewObjectCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public NewObjectCommandResult(int code, Guid id)
        {
            Code = code;
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}