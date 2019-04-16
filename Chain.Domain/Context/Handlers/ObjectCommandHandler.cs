using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.Object;
using Chain.Domain.Context.Entities;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;
using Chain.Shared.Context.Handlers;
using FluentValidator;
using Object = Chain.Domain.Context.Entities.Object;

namespace Chain.Domain.Context.Handlers
{
    public class ObjectCommandHandler : Notifiable,
        ICommandHandler<NewObjectCommand>,
        ICommandHandler<UpdateObjectCommand>,
        ICommandHandler<DeleteObjectCommand>,
        ICommandHandler<GetObjectCommand>
    {
        private readonly IObjectRepository _objectRepository;
        private readonly ILoggingService _loggingService;

        public ObjectCommandHandler(IObjectRepository objectRepository, ILoggingService loggingService)
        {
            _objectRepository = objectRepository;
            _loggingService = loggingService;
        }

        public ICommandResult Handle(NewObjectCommand command)
        {
            ICommandResult result = new NewObjectCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Title, command.Description, command.Creator, command.Father, command.Path, command.Type, command.Status, command.RequestHost }, "ObjectCommandHandler.Handle(New)");

            try
            {
                Object obj = new Object(command.Title, command.Description, command.Creator, command.Father, command.Path, command.Type, command.Status);

                if (obj.Valid)
                {
                    if (!_objectRepository.CheckExists(obj.Title))
                    {
                        if (_objectRepository.Create(obj))
                            result = new NewObjectCommandResult(200, obj.Id);
                    }

                    else if (_objectRepository.Valid)
                        result = new NewObjectCommandResult(400, new Notification("Title", "Already in Use"));
                }

                else
                    result = new NewObjectCommandResult(400, obj.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Title, command.Description, command.Creator, command.Father, command.Path, command.Type, command.Status, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(UpdateObjectCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Id, command.Title, command.Description, command.Creator, command.Father, command.Path, command.Type, command.Status, command.RequestHost }, "ObjectCommandHandler.Handle(Update)");

            try
            {
                Object obj = new Object(command.Title, command.Description, command.Creator, command.Father, command.Path, command.Type, command.Status);

                if (obj.Valid)
                {
                    if (_objectRepository.CheckExists(command.Id))
                    {
                        if (!_objectRepository.CheckExists(obj.Title))
                        {
                            if (_objectRepository.Update(command.Id, obj))
                                result = new CommandResult(200);
                        }

                        else if (_objectRepository.Valid)
                            result = new CommandResult(400, new Notification("Title", "Already in Use"));
                    }

                    else if (_objectRepository.Valid)
                        result = new CommandResult(400, new Notification("Object", "Could not be found"));
                }

                else
                    result = new NewObjectCommandResult(400, obj.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Id, command.Title, command.Description, command.Creator, command.Father, command.Path, command.Type, command.Status, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(DeleteObjectCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Object, command.RequestHost }, "ObjectCommandHandler.Handle(Delete)");

            try
            {
                if (_objectRepository.CheckExists(command.Object))
                {
                    if (_objectRepository.Delete(command.Object))
                        result = new CommandResult(200);
                }

                else if (_objectRepository.Valid)
                    result = new CommandResult(400, new Notification("Object", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Object, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(GetObjectCommand command)
        {
            ICommandResult result = new GetObjectCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Object, command.RequestHost }, "ObjectCommandHandler.Handle(Get)");

            try
            {
                if (_objectRepository.CheckExists(command.Object))
                {
                    Object obj = _objectRepository.Get(command.Object);

                    if (obj != null)
                        result = new GetObjectCommandResult(200, obj.Title, obj.Description, obj.Creator, obj.Father, obj.Path, obj.Type, obj.Status, obj.CreationDate);
                }

                else if (_objectRepository.Valid)
                    result = new GetObjectCommandResult(400, new Notification("Object", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Object, command.RequestHost }, e);
            }

            return result;
        }
    }
}