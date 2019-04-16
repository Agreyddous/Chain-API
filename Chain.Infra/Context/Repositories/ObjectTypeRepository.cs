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
    public class ObjectTypeRepository : Notifiable, IObjectTypeRepository
    {
        private readonly ChainContext _context;
        private readonly ILoggingService _loggingService;

        public ObjectTypeRepository(ChainContext context, ILoggingService loggingService)
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
                    "SP_CheckObjectTypeByName",
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
                    "SP_CheckObjectType",
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

        public bool Create(ObjectType objectType)
        {
            bool Created = true;

            try
            {
                _context.Connection.Execute(
                    "SP_NewObjectType",
                    new
                    {
                        objectType.Id,
                        objectType.Name
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Created = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { ObjectType = objectType }, e);
            }

            return Created;
        }

        public bool Delete(Guid id)
        {
            bool Deleted = true;

            try
            {
                _context.Connection.Execute(
                    "SP_DeleteObjectType",
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

        public ObjectType Get(Guid id)
        {
            ObjectType objectType = null;

            try
            {
                objectType = _context.Connection.Query<ObjectType>(
                    "SP_GetObjectType",
                    new
                    { Id = id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id }, e);
            }

            return objectType;
        }

        public bool Update(Guid id, ObjectType objectType)
        {
            bool Updated = true;

            try
            {
                _context.Connection.Execute(
                    "SP_UpdateObjectType",
                    new
                    {
                        Id = id,
                        objectType.Name
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Updated = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id, ObjectType = objectType }, e);
            }

            return Updated;
        }
    }
}