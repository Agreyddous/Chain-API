using System;
using Chain.Domain.Context.Entities;
using Chain.Shared.Context.Interfaces;

namespace Chain.Domain.Context.Repositories
{
    public interface IGroupRepository : INotifiable
    {
        bool CheckExists(Guid group);
        bool CheckExists(string name);
        bool Create(Group group);
        bool Update(Guid id, Group group);
        bool Delete(Guid id);
        Group Get(Guid id);
    }
}