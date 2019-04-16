using System;
using System.Data;
using System.Linq;
using Chain.Domain.Context.Entities;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Infra.Context.DataContexts;
using Chain.Shared.Context.Enums;
using Dapper;
using FluentValidator;

namespace Chain.Infra.Context.Repositories
{
    public class FlowRepository : Notifiable, IFlowRepository
    {
        private readonly ChainContext _context;
        private readonly ILoggingService _loggingService;

        public FlowRepository(ChainContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public bool CheckExists(string path)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckFlowByPath",
                    new { Path = path },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                exists = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Path = path }, e);
            }

            return exists;
        }

        public bool CheckExists(Guid id)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckFlow",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                exists = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id }, e);
            }

            return exists;
        }

        public bool Create(Flow flow)
        {
            bool Created = true;

            try
            {
                _context.Connection.Execute(
                    "SP_NewFlow",
                    new
                    {
                        flow.Id,
                        flow.Owner,
                        flow.OwnerType,
                        flow.Object,
                        flow.CreationDate,
                        flow.Path
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Created = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Flow = flow }, e);
            }

            return Created;
        }

        public bool Delete(Guid id)
        {
            bool Deleted = true;

            try
            {
                _context.Connection.Execute(
                    "SP_DeleteFlow",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Deleted = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { User = id }, e);
            }

            return Deleted;
        }

        public Flow Get(Guid id)
        {
            Flow flow = null;

            try
            {
                flow = _context.Connection.Query<Flow>(
                    "SP_GetFlow",
                    new
                    { Id = id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id }, e);
            }

            return flow;
        }

        public bool Update(Guid id, Flow flow)
        {
            bool Updated = true;

            try
            {
                _context.Connection.Execute(
                    "SP_UpdateFlow",
                    new
                    {
                        Id = id,
                        flow.Owner,
                        flow.OwnerType,
                        flow.Object,
                        flow.CreationDate,
                        flow.Path
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Updated = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id, Flow = flow }, e);
            }

            return Updated;
        }
    }
}