using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.Groups
{
    public class NewGroupCommandResult : CommandResult
    {
        public NewGroupCommandResult() : base() { }

        public NewGroupCommandResult(int code) : base(code) { }

        public NewGroupCommandResult(int code, Notification notification) : base(code, notification) { }

        public NewGroupCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public NewGroupCommandResult(int code, Guid id)
        {
            Code = code;
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}