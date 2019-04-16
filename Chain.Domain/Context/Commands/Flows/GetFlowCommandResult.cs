using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;
using Chain.Shared.Context.Enums;

namespace Chain.Domain.Context.Commands.Flows
{
    public class GetFlowCommandResult : CommandResult
    {
        public GetFlowCommandResult() : base() { }

        public GetFlowCommandResult(int code) : base(code) { }

        public GetFlowCommandResult(int code, Notification notification) : base(code, notification) { }

        public GetFlowCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public GetFlowCommandResult(int code, Guid owner, EOwnerType ownerType, Guid objectId, DateTime creationDate, string path)
        {
            Code = code;
            Owner = owner;
            OwnerType = ownerType;
            Object = objectId;
            CreationDate = creationDate;
            Path = path;
        }

        public Guid Owner { get; private set; }
        public EOwnerType OwnerType { get; private set; }
        public Guid Object { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Path { get; private set; }
    }
}