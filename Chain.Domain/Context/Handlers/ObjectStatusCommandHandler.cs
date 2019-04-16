using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.ObjectStatus;
using Chain.Domain.Context.Entities;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;
using Chain.Shared.Context.Handlers;
using FluentValidator;

namespace Chain.Domain.Context.Handlers
{
    public class ObjectStatusCommandHandler : Notifiable,
        ICommandHandler<NewObjectStatusCommand>,
        ICommandHandler<UpdateObjectStatusCommand>,
        ICommandHandler<DeleteObjectStatusCommand>,
        ICommandHandler<GetObjectStatusCommand>
    {
        private readonly IObjectStatusRepository _objectStatusRepository;
        private readonly ILoggingService _loggingService;

        public ObjectStatusCommandHandler(IObjectStatusRepository objectStatusRepository, ILoggingService loggingService)
        {
            _objectStatusRepository = objectStatusRepository;
            _loggingService = loggingService;
        }

        public ICommandResult Handle(NewObjectStatusCommand command)
        {
            ICommandResult result = new NewObjectStatusCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Name, command.RequestHost }, "ObjectStatusCommandHandler.Handle(New)");

            try
            {
                ObjectStatus objectStatus = new ObjectStatus(command.Name);

                if (objectStatus.Valid)
                {
                    if (!_objectStatusRepository.CheckExists(objectStatus.Name))
                    {
                        if (_objectStatusRepository.Create(objectStatus))
                            result = new NewObjectStatusCommandResult(200, objectStatus.Id);
                    }

                    else if (_objectStatusRepository.Valid)
                        result = new NewObjectStatusCommandResult(400, new Notification("Name", "Already in Use"));
                }

                else
                    result = new NewObjectStatusCommandResult(400, objectStatus.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Name, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(UpdateObjectStatusCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Id, command.Name, command.RequestHost }, "ObjectStatusCommandHandler.Handle(Update)");

            try
            {
                ObjectStatus objectStatus = new ObjectStatus(command.Name);

                if (objectStatus.Valid)
                {
                    if (_objectStatusRepository.CheckExists(command.Id))
                    {
                        if (!_objectStatusRepository.CheckExists(objectStatus.Name))
                        {
                            if (_objectStatusRepository.Update(command.Id, objectStatus))
                                result = new CommandResult(200);
                        }

                        else if (_objectStatusRepository.Valid)
                            result = new CommandResult(400, new Notification("Name", "Already in Use"));
                    }

                    else if (_objectStatusRepository.Valid)
                        result = new CommandResult(400, new Notification("Object Status", "Could not be found"));
                }

                else
                    result = new NewObjectStatusCommandResult(400, objectStatus.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Id, command.Name, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(DeleteObjectStatusCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.ObjectStatus, command.RequestHost }, "ObjectStatusCommandHandler.Handle(Delete)");

            try
            {
                if (_objectStatusRepository.CheckExists(command.ObjectStatus))
                {
                    if (_objectStatusRepository.Delete(command.ObjectStatus))
                        result = new CommandResult(200);
                }

                else if (_objectStatusRepository.Valid)
                    result = new CommandResult(400, new Notification("Object Status", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.ObjectStatus, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(GetObjectStatusCommand command)
        {
            ICommandResult result = new GetObjectStatusCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.ObjectStatus, command.RequestHost }, "ObjectStatusCommandHandler.Handle(Get)");

            try
            {
                if (_objectStatusRepository.CheckExists(command.ObjectStatus))
                {
                    ObjectStatus objectStatus = _objectStatusRepository.Get(command.ObjectStatus);

                    if (objectStatus != null)
                        result = new GetObjectStatusCommandResult(200, objectStatus.Name);
                }

                else if (_objectStatusRepository.Valid)
                    result = new GetObjectStatusCommandResult(400, new Notification("Object Status", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.ObjectStatus, command.RequestHost }, e);
            }

            return result;
        }
    }
}