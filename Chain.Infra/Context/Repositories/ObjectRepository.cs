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
using Object = Chain.Domain.Context.Entities.Object;

namespace Chain.Infra.Context.Repositories
{
    public class ObjectRepository : Notifiable, IObjectRepository
    {
        private readonly ChainContext _context;
        private readonly ILoggingService _loggingService;

        public ObjectRepository(ChainContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public bool CheckExists(string title)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckObjectByName",
                    new { Title = title },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                exists = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Title = title }, e);
            }

            return exists;
        }

        public bool CheckExists(Guid id)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckObject",
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

        public bool Create(Object obj)
        {
            bool Created = true;

            try
            {
                _context.Connection.Execute(
                    "SP_NewObject",
                    new
                    {
                        obj.Id,
                        obj.Title,
                        obj.Description,
                        obj.Creator,
                        obj.Father,
                        obj.Path,
                        obj.Type,
                        obj.Status,
                        obj.CreationDate
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Created = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Object = obj }, e);
            }

            return Created;
        }

        public bool Delete(Guid id)
        {
            bool Deleted = true;

            try
            {
                _context.Connection.Execute(
                    "SP_DeleteObject",
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

        public Object Get(Guid id)
        {
            Object obj = null;

            try
            {
                obj = _context.Connection.Query<Object>(
                    "SP_GetObject",
                    new
                    { Id = id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id }, e);
            }

            return obj;
        }

        public bool Update(Guid id, Object obj)
        {
            bool Updated = true;

            try
            {
                _context.Connection.Execute(
                    "SP_UpdateObject",
                    new
                    {
                        Id = id,
                        obj.Title,
                        obj.Description,
                        obj.Creator,
                        obj.Father,
                        obj.Path,
                        obj.Type,
                        obj.Status,
                        obj.CreationDate
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Updated = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id, Object = obj }, e);
            }

            return Updated;
        }
    }
}