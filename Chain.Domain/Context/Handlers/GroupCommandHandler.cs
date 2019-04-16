using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.Groups;
using Chain.Domain.Context.Entities;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;
using Chain.Shared.Context.Handlers;
using FluentValidator;

namespace Chain.Domain.Context.Handlers
{
    public class GroupCommandHandler : Notifiable,
        ICommandHandler<NewGroupCommand>,
        ICommandHandler<UpdateGroupCommand>,
        ICommandHandler<DeleteGroupCommand>,
        ICommandHandler<GetGroupCommand>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly ILoggingService _loggingService;

        public GroupCommandHandler(IGroupRepository groupRepository, ILoggingService loggingService)
        {
            _groupRepository = groupRepository;
            _loggingService = loggingService;
        }

        public ICommandResult Handle(NewGroupCommand command)
        {
            ICommandResult result = new NewGroupCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Name, command.Description, command.RequestHost }, "GroupCommandHandler.Handle(New)");

            try
            {
                Group group = new Group(command.Name, command.Description);

                if (group.Valid)
                {
                    if (!_groupRepository.CheckExists(group.Name))
                    {
                        if (_groupRepository.Create(group))
                            result = new NewGroupCommandResult(200, group.Id);
                    }

                    else if (_groupRepository.Valid)
                        result = new NewGroupCommandResult(400, new Notification("Name", "Already in Use"));
                }

                else
                    result = new NewGroupCommandResult(400, group.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Name, command.Description, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(UpdateGroupCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Id, command.Name, command.Description, command.RequestHost }, "GroupCommandHandler.Handle(Update)");

            try
            {
                Group group = new Group(command.Name, command.Description);

                if (group.Valid)
                {
                    if (_groupRepository.CheckExists(command.Id))
                    {
                        if (!_groupRepository.CheckExists(group.Name))
                        {
                            if (_groupRepository.Update(command.Id, group))
                                result = new CommandResult(200);
                        }

                        else if (_groupRepository.Valid)
                            result = new CommandResult(400, new Notification("Name", "Already in Use"));
                    }

                    else if (_groupRepository.Valid)
                        result = new CommandResult(400, new Notification("Group", "Could not be found"));
                }

                else
                    result = new NewGroupCommandResult(400, group.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Id, command.Name, command.Description, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(DeleteGroupCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Group, command.RequestHost }, "GroupCommandHandler.Handle(Delete)");

            try
            {
                if (_groupRepository.CheckExists(command.Group))
                {
                    if (_groupRepository.Delete(command.Group))
                        result = new CommandResult(200);
                }

                else if (_groupRepository.Valid)
                    result = new CommandResult(400, new Notification("Group", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Group, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(GetGroupCommand command)
        {
            ICommandResult result = new GetGroupCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Group, command.RequestHost }, "GroupCommandHandler.Handle(Get)");

            try
            {
                if (_groupRepository.CheckExists(command.Group))
                {
                    Group group = _groupRepository.Get(command.Group);

                    if (group != null)
                        result = new GetGroupCommandResult(200, group.Name, group.Description);
                }

                else if (_groupRepository.Valid)
                    result = new GetGroupCommandResult(400, new Notification("Group", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Group, command.RequestHost }, e);
            }

            return result;
        }
    }
}