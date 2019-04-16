using System;
using Chain.Domain.Context.Entities;
using Chain.Shared.Context.Interfaces;

namespace Chain.Domain.Context.Repositories
{
    public interface IObjectStatusRepository : INotifiable
    {
        bool CheckExists(string name);
        bool Create(ObjectStatus objectStatus);
        bool CheckExists(Guid id);
        bool Update(Guid id, ObjectStatus objectStatus);
        ObjectStatus Get(Guid id);
        bool Delete(Guid id);
    }
}