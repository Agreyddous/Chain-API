using System.Collections.Generic;
using Chain.Shared.Context.Commands;
using FluentValidator;

namespace Chain.Domain.Context.Commands
{
    public class CommandResult : ICommandResult
    {
        private List<Notification> _notifications;

        public CommandResult() : this(500) { }

        public CommandResult(int code) : this(code, new Notification("Action", "Finished execution")) { }

        public CommandResult(int code, Notification notification)
        {
            Code = code;

            _notifications = new List<Notification>();
            _notifications.Add(notification);
        }

        public CommandResult(int code, IReadOnlyCollection<Notification> notifications)
        {
            Code = code;

            _notifications = new List<Notification>();
            _notifications.AddRange(notifications);
        }

        public int Code { get; set; }
        public IReadOnlyCollection<Notification> Notifications => _notifications;
    }
}