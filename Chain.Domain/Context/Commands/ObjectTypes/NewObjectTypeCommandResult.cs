using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectTypes
{
    public class NewObjectTypeCommandResult : CommandResult
    {
        public NewObjectTypeCommandResult() : base() { }

        public NewObjectTypeCommandResult(int code) : base(code) { }

        public NewObjectTypeCommandResult(int code, Notification notification) : base(code, notification) { }

        public NewObjectTypeCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public NewObjectTypeCommandResult(int code, Guid id)
        {
            Code = code;
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}