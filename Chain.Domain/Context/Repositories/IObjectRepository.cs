using System;
using Chain.Domain.Context.Entities;
using Chain.Shared.Context.Interfaces;
using Object = Chain.Domain.Context.Entities.Object;

namespace Chain.Domain.Context.Repositories
{
    public interface IObjectRepository : INotifiable
    {
        bool CheckExists(Guid id);
        bool CheckExists(string title);
        bool Create(Object obj);
        bool Update(Guid id, Object obj);
        bool Delete(Guid id);
        Object Get(Guid id);
    }
}