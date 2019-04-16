using System.Collections.Generic;
using FluentValidator;

namespace Chain.Shared.Context.Interfaces
{
    public interface INotifiable
    {
        bool Valid { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
    }
}