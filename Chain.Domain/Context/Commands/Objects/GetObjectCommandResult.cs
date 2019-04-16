using System;
using System.Collections.Generic;
using FluentValidator;
using Chain.Domain.Context.Commands;
using Chain.Shared.Context.Enums;

namespace Chain.Domain.Context.Commands.Object
{
    public class GetObjectCommandResult : CommandResult
    {
        public GetObjectCommandResult() : base() { }

        public GetObjectCommandResult(int code) : base(code) { }

        public GetObjectCommandResult(int code, Notification notification) : base(code, notification) { }

        public GetObjectCommandResult(int code, IReadOnlyCollection<Notification> notifications) : base(code, notifications) { }

        public GetObjectCommandResult(int code, string title, string description, Guid creator, Guid father, string path, Guid type, Guid status, DateTime creationDate)
        {
            Code = code;
            Title = title;
            Description = description;
            Creator = creator;
            Father = father;
            Path = path;
            Type = type;
            Status = status;
            CreationDate = creationDate;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public Guid Creator { get; set; }
        public Guid Father { get; set; }
        public string Path { get; set; }
        public Guid Type { get; set; }
        public Guid Status { get; set; }
        public DateTime CreationDate { get; set; }
    }
}