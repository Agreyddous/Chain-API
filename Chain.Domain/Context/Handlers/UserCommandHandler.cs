using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using FluentValidator;
using Chain.Shared.Context.Handlers;
using Chain.Shared.Context.Commands;
using Chain.Domain.Context.Commands.Users;
using Chain.Shared.Context.Enums;
using Chain.Domain.Context.Entities;
using System;
using Chain.Domain.Context.Commands;

namespace Chain.Domain.Context.Handlers
{
    public class UserCommandHandler : Notifiable,
        ICommandHandler<NewUserCommand>,
        ICommandHandler<UpdateUserCommand>,
        ICommandHandler<DeleteUserCommand>,
        ICommandHandler<GetUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly ILoggingService _loggingService;

        public UserCommandHandler(IUserRepository userRepository, IGroupRepository groupRepository, ILoggingService loggingService)
        {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _loggingService = loggingService;
        }

        public ICommandResult Handle(NewUserCommand command)
        {
            ICommandResult result = new NewUserCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Name, command.Username, command.Groups, command.RequestHost }, "UserCommandHandler.Handle(New)");

            try
            {
                User user = new User(command.Name, command.Username, command.Password, command.Groups);

                if (user.Valid)
                {
                    if (!_userRepository.CheckExists(user.Username))
                    {
                        foreach (Guid group in command.Groups)
                            if (_groupRepository.CheckExists(group))
                                AddNotification($"Group {group}", "Could not find");

                        if (Valid)
                        {
                            if (_userRepository.Create(user))
                                result = new NewUserCommandResult(200, user.Id);
                        }

                        else if (_groupRepository.Valid)
                            result = new NewUserCommandResult(400, Notifications);
                    }

                    else if (_userRepository.Valid)
                        result = new NewUserCommandResult(400, new Notification("Username", "Already in Use"));
                }

                else
                    result = new NewUserCommandResult(400, user.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Name, command.Username, command.Groups, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(UpdateUserCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Id, command.Name, command.Username, command.Group, command.RequestHost }, "UserCommandHandler.Handle(Update)");

            try
            {
                User user = new User(command.Name, command.Username, command.Password, command.Group);

                if (user.Valid)
                {
                    if (_userRepository.CheckExists(command.Id))
                    {
                        if (_userRepository.CheckExists(user.Name))
                        {
                            foreach (Guid group in user.Groups)
                                if (_groupRepository.CheckExists(group))
                                    AddNotification($"Group {group}", "Could not find");

                            if (Valid)
                            {
                                if (_userRepository.Update(command.Id, user))
                                    result = new CommandResult(200);
                            }

                            else if (_groupRepository.Valid)
                                result = new CommandResult(400, Notifications);
                        }

                        else if (_userRepository.Valid)
                            result = new CommandResult(400, new Notification("Username", "Already in Use"));
                    }

                    else if (_userRepository.Valid)
                        result = new CommandResult(400, new Notification("User", "Could not be found"));
                }

                else
                    result = new CommandResult(400, user.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Id, command.Name, command.Username, command.Group, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(DeleteUserCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.User, command.RequestHost }, "UserCommandHandler.Handle(Delete)");

            try
            {
                if (_userRepository.CheckExists(command.User))
                {
                    if (_userRepository.Delete(command.User))
                        result = new CommandResult(200);
                }

                else if (_userRepository.Valid)
                    result = new CommandResult(400, new Notification("User", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.User, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(GetUserCommand command)
        {
            ICommandResult result = new GetUserCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.User, command.RequestHost }, "UserCommandHandler.Handle(Get)");

            try
            {
                if (_userRepository.CheckExists(command.User))
                {
                    User user = _userRepository.Get(command.User);

                    if (user != null)
                        result = new GetUserCommandResult(200, user.Name, user.Username, user.Groups);
                }

                else if (_userRepository.Valid)
                    result = new GetUserCommandResult(400, new Notification("User", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.User, command.RequestHost }, e);
            }

            return result;
        }
    }
}