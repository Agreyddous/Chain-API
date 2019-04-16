using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.Users
{
    public class NewUserCommandResult : CommandResult
    {
        public NewUserCommandResult() : base() { }

        public NewUserCommandResult(int code) : base(code) { }

        public NewUserCommandResult(int code, Notification notification) : base(code, notification) { }

        public NewUserCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public NewUserCommandResult(int code, Guid id)
        {
            Code = code;
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}