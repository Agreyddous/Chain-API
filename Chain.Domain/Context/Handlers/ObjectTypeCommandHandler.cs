using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.ObjectTypes;
using Chain.Domain.Context.Entities;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;
using Chain.Shared.Context.Handlers;
using FluentValidator;

namespace Chain.Domain.Context.Handlers
{
    public class ObjectTypeCommandHandler : Notifiable,
        ICommandHandler<NewObjectTypeCommand>,
        ICommandHandler<UpdateObjectTypeCommand>,
        ICommandHandler<DeleteObjectTypeCommand>,
        ICommandHandler<GetObjectTypeCommand>
    {
        private readonly IObjectTypeRepository _objectTypeRepository;
        private readonly ILoggingService _loggingService;

        public ObjectTypeCommandHandler(IObjectTypeRepository objectTypeRepository, ILoggingService loggingService)
        {
            _objectTypeRepository = objectTypeRepository;
            _loggingService = loggingService;
        }

        public ICommandResult Handle(NewObjectTypeCommand command)
        {
            ICommandResult result = new NewObjectTypeCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Name, command.RequestHost }, "ObjectTypeCommandHandler.Handle(New)");

            try
            {
                ObjectType objectType = new ObjectType(command.Name);

                if (objectType.Valid)
                {
                    if (!_objectTypeRepository.CheckExists(objectType.Name))
                    {
                        if (_objectTypeRepository.Create(objectType))
                            result = new NewObjectTypeCommandResult(200, objectType.Id);
                    }

                    else if (_objectTypeRepository.Valid)
                        result = new NewObjectTypeCommandResult(400, new Notification("Name", "Already in Use"));
                }

                else
                    result = new NewObjectTypeCommandResult(400, objectType.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Name, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(UpdateObjectTypeCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Id, command.Name, command.RequestHost }, "ObjectTypeCommandHandler.Handle(Update)");

            try
            {
                ObjectType objectType = new ObjectType(command.Name);

                if (objectType.Valid)
                {
                    if (_objectTypeRepository.CheckExists(command.Id))
                    {
                        if (!_objectTypeRepository.CheckExists(objectType.Name))
                        {
                            if (_objectTypeRepository.Update(command.Id, objectType))
                                result = new CommandResult(200);
                        }

                        else if (_objectTypeRepository.Valid)
                            result = new CommandResult(400, new Notification("Name", "Already in Use"));
                    }

                    else if (_objectTypeRepository.Valid)
                        result = new CommandResult(400, new Notification("Object type", "Could not be found"));
                }

                else
                    result = new NewObjectTypeCommandResult(400, objectType.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Id, command.Name, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(DeleteObjectTypeCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.ObjectType, command.RequestHost }, "ObjectTypeCommandHandler.Handle(Delete)");

            try
            {
                if (_objectTypeRepository.CheckExists(command.ObjectType))
                {
                    if (_objectTypeRepository.Delete(command.ObjectType))
                        result = new CommandResult(200);
                }

                else if (_objectTypeRepository.Valid)
                    result = new CommandResult(400, new Notification("Object type", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.ObjectType, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(GetObjectTypeCommand command)
        {
            ICommandResult result = new GetObjectTypeCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.ObjectType, command.RequestHost }, "ObjectTypeCommandHandler.Handle(Get)");

            try
            {
                if (_objectTypeRepository.CheckExists(command.ObjectType))
                {
                    ObjectType objectType = _objectTypeRepository.Get(command.ObjectType);

                    if (objectType != null)
                        result = new GetObjectTypeCommandResult(200, objectType.Name);
                }

                else if (_objectTypeRepository.Valid)
                    result = new GetObjectTypeCommandResult(400, new Notification("Object Type", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.ObjectType, command.RequestHost }, e);
            }

            return result;
        }
    }
}