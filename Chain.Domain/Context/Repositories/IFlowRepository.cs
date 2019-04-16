using System;
using Chain.Domain.Context.Entities;
using Chain.Shared.Context.Interfaces;

namespace Chain.Domain.Context.Repositories
{
    public interface IFlowRepository : INotifiable
    {
        bool CheckExists(string path);
        bool Create(Flow flow);
        bool CheckExists(Guid id);
        bool Update(Guid id, Flow flow);
        bool Delete(Guid id);
        Flow Get(Guid id);
    }
}