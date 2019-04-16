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
    public class GroupRepository : Notifiable, IGroupRepository
    {
        private readonly ChainContext _context;
        private readonly ILoggingService _loggingService;

        public GroupRepository(ChainContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public bool CheckExists(string name)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckGroupByName",
                    new { Name = name },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                exists = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Name = name }, e);
            }

            return exists;
        }

        public bool CheckExists(Guid id)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckGroup",
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

        public bool Create(Group group)
        {
            bool Created = true;

            try
            {
                _context.Connection.Execute(
                    "SP_NewGroup",
                    new
                    {
                        group.Id,
                        group.Name,
                        group.Description
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Created = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Group = group }, e);
            }

            return Created;
        }

        public bool Delete(Guid id)
        {
            bool Deleted = true;

            try
            {
                _context.Connection.Execute(
                    "SP_DeleteGroup",
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

        public Group Get(Guid id)
        {
            Group group = null;

            try
            {
                group = _context.Connection.Query<Group>(
                    "SP_GetGroup",
                    new
                    { Id = id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id }, e);
            }

            return group;
        }

        public bool Update(Guid id, Group group)
        {
            bool Updated = true;

            try
            {
                _context.Connection.Execute(
                    "SP_UpdateGroup",
                    new
                    {
                        Id = id,
                        group.Name,
                        group.Description
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Updated = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id, Group = group }, e);
            }

            return Updated;
        }
    }
}