using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.Groups
{
    public class GetGroupCommandResult : CommandResult
    {
        public GetGroupCommandResult() : base() { }

        public GetGroupCommandResult(int code) : base(code) { }

        public GetGroupCommandResult(int code, Notification notification) : base(code, notification) { }

        public GetGroupCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public GetGroupCommandResult(int code, string name, string description)
        {
            Code = code;
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}