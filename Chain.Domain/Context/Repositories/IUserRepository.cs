using System;
using Chain.Domain.Context.Entities;
using Chain.Shared.Context.Interfaces;

namespace Chain.Domain.Context.Repositories
{
    public interface IUserRepository : INotifiable
    {
        bool CheckExists(string username);
        bool CheckExists(Guid id);
        bool Create(User user);
        bool Update(Guid id, User user);
        bool Delete(Guid id);
        User Get(Guid id);
    }
}