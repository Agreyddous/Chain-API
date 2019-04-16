using System.Collections.Generic;
using FluentValidator;

namespace Chain.Shared.Context.Commands
{
    public interface ICommandResult
    {
        int Code { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}