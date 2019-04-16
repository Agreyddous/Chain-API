using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.Users
{
    public class GetUserCommandResult : CommandResult
    {
        public GetUserCommandResult() : base() { }

        public GetUserCommandResult(int code) : base(code) { }

        public GetUserCommandResult(int code, Notification notification) : base(code, notification) { }

        public GetUserCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public GetUserCommandResult(int code, string name, string username, List<Guid> groups)
        {
            Code = code;
            Name = name;
            Username = username;
            Groups = groups;
        }

        public string Name { get; private set; }
        public string Username { get; private set; }
        public List<Guid> Groups { get; private set; }
    }
}