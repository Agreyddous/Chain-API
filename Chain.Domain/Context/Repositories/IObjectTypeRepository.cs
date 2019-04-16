using System;
using Chain.Domain.Context.Entities;
using Chain.Shared.Context.Interfaces;

namespace Chain.Domain.Context.Repositories
{
    public interface IObjectTypeRepository : INotifiable
    {
        bool CheckExists(string name);
        bool Create(ObjectType objectType);
        bool CheckExists(Guid id);
        bool Update(Guid id, ObjectType objectType);
        bool Delete(Guid id);
        ObjectType Get(Guid id);
    }
}