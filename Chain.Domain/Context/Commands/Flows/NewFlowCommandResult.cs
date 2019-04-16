using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Commands.Flows
{
    public class NewFlowCommandResult : CommandResult
    {
        public NewFlowCommandResult() : base() { }

        public NewFlowCommandResult(int code) : base(code) { }

        public NewFlowCommandResult(int code, Notification notification) : base(code, notification) { }

        public NewFlowCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public NewFlowCommandResult(int code, Guid id)
        {
            Code = code;
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}