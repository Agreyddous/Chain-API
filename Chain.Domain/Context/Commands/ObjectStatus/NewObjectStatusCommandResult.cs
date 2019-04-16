using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.ObjectStatus
{
    public class NewObjectStatusCommandResult : CommandResult
    {
        public NewObjectStatusCommandResult() : base() { }

        public NewObjectStatusCommandResult(int code) : base(code) { }

        public NewObjectStatusCommandResult(int code, Notification notification) : base(code, notification) { }

        public NewObjectStatusCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public NewObjectStatusCommandResult(int code, Guid id)
        {
            Code = code;
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}