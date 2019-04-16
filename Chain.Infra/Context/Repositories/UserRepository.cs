using System;
using System.Collections.Generic;
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
    public class UserRepository : Notifiable, IUserRepository
    {
        private readonly ChainContext _context;
        private readonly ILoggingService _loggingService;

        public UserRepository(ChainContext context, ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public bool CheckExists(string username)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckUserByUsername",
                    new { Username = username },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                exists = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Username = username }, e);
            }

            return exists;
        }

        public bool CheckExists(Guid id)
        {
            bool exists = true;

            try
            {
                exists = _context.Connection.Query<bool>(
                    "SP_CheckUser",
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

        public bool Create(User user)
        {
            bool Created = true;

            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("groupId", typeof(Guid));

                foreach (Guid group in user.Groups)
                    table.Rows.Add(group);

                _context.Connection.Execute(
                    "SP_NewUser",
                    new
                    {
                        user.Id,
                        user.Name,
                        user.Username,
                        user.Password,
                        Groups = table.AsTableValuedParameter("GroupsTable")
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Created = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { User = user }, e);
            }

            return Created;
        }

        public bool Delete(Guid id)
        {
            bool Deleted = true;

            try
            {
                _context.Connection.Execute(
                    "SP_DeleteUser",
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

        public User Get(Guid id)
        {
            User user = null;

            try
            {
                user = _context.Connection.Query<User>(
                    "SP_GetUser",
                    new
                    { Id = id },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception e)
            {
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id }, e);
            }

            return user;
        }

        public bool Update(Guid id, User user)
        {
            bool Updated = true;

            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("groupId", typeof(Guid));

                foreach (Guid group in user.Groups)
                    table.Rows.Add(group);

                _context.Connection.Execute(
                    "SP_UpdateUser",
                    new
                    {
                        Id = id,
                        user.Name,
                        user.Username,
                        user.Password,
                        Groups = table.AsTableValuedParameter("GroupsTable")
                    },
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
                Updated = false;
                AddNotification("Error", e.Message);

                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { Id = id, User = user }, e);
            }

            return Updated;
        }
    }
}