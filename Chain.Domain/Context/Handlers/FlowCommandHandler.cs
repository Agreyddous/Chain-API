using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.Flows;
using Chain.Domain.Context.Entities;
using Chain.Domain.Context.Repositories;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Commands;
using Chain.Shared.Context.Enums;
using Chain.Shared.Context.Handlers;
using FluentValidator;

namespace Chain.Domain.Context.Handlers
{
    public class FlowCommandHandler : Notifiable,
        ICommandHandler<NewFlowCommand>,
        ICommandHandler<UpdateFlowCommand>,
        ICommandHandler<DeleteFlowCommand>,
        ICommandHandler<GetFlowCommand>
    {
        private readonly IFlowRepository _flowRepository;
        private readonly ILoggingService _loggingService;

        public FlowCommandHandler(IFlowRepository flowRepository, ILoggingService loggingService)
        {
            _flowRepository = flowRepository;
            _loggingService = loggingService;
        }

        public ICommandResult Handle(NewFlowCommand command)
        {
            ICommandResult result = new NewFlowCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Owner, command.OwnerType, command.Object, command.Path, command.RequestHost }, "FlowCommandHandler.Handle(New)");

            try
            {
                Flow flow = new Flow(command.Owner, command.OwnerType, command.Object, command.Path);

                if (flow.Valid)
                {
                    if (!_flowRepository.CheckExists(flow.Path))
                    {
                        if (_flowRepository.Create(flow))
                            result = new NewFlowCommandResult(200, flow.Id);
                    }

                    else if (_flowRepository.Valid)
                        result = new NewFlowCommandResult(400, new Notification("Path", "Already in Use"));
                }

                else
                    result = new NewFlowCommandResult(400, flow.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Owner, command.OwnerType, command.Object, command.Path, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(UpdateFlowCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Owner, command.OwnerType, command.Object, command.Path, command.RequestHost }, "FlowCommandHandler.Handle(Update)");

            try
            {
                Flow flow = new Flow(command.Owner, command.OwnerType, command.Object, command.Path);

                if (flow.Valid)
                {
                    if (_flowRepository.CheckExists(command.Id))
                    {
                        if (!_flowRepository.CheckExists(flow.Path))
                        {
                            if (_flowRepository.Update(command.Id, flow))
                                result = new CommandResult(200);
                        }

                        else if (_flowRepository.Valid)
                            result = new CommandResult(400, new Notification("Path", "Already in Use"));
                    }

                    else if (_flowRepository.Valid)
                        result = new CommandResult(400, new Notification("Flow", "Could not be found"));
                }

                else
                    result = new NewFlowCommandResult(400, flow.Notifications);
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Owner, command.OwnerType, command.Object, command.Path, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(DeleteFlowCommand command)
        {
            ICommandResult result = new CommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Flow, command.RequestHost }, "FlowCommandHandler.Handle(Delete)");

            try
            {
                if (_flowRepository.CheckExists(command.Flow))
                {
                    if (_flowRepository.Delete(command.Flow))
                        result = new CommandResult(200);
                }

                else if (_flowRepository.Valid)
                    result = new CommandResult(400, new Notification("Flow", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Flow, command.RequestHost }, e);
            }

            return result;
        }

        public ICommandResult Handle(GetFlowCommand command)
        {
            ICommandResult result = new GetFlowCommandResult();

            _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Debug, new { command.Flow, command.RequestHost }, "FlowCommandHandler.Handle(Get)");

            try
            {
                if (_flowRepository.CheckExists(command.Flow))
                {
                    Flow flow = _flowRepository.Get(command.Flow);

                    if (flow != null)
                        result = new GetFlowCommandResult(200, flow.Owner, flow.OwnerType, flow.Object, flow.CreationDate, flow.Path);
                }

                else if (_flowRepository.Valid)
                    result = new GetFlowCommandResult(400, new Notification("Flow", "Could not be found"));
            }
            catch (Exception e)
            {
                _loggingService.Log(this.GetType(), ELogType.Neutral, ELogLevel.Error, new { command.Flow, command.RequestHost }, e);
            }

            return result;
        }
    }
}