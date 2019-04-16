using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.Flows;
using Chain.Domain.Context.Handlers;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Chain.API.Controllers
{
    public class FlowController : Controller
    {
        private readonly FlowCommandHandler _flowHandler;
        private readonly ILoggingService _loggingService;

        public FlowController(FlowCommandHandler flowHandler, ILoggingService loggingService)
        {
            _flowHandler = flowHandler;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates a new Flow
        /// </summary>
        ///
        /// <param name="command">New Flow Command</param>
        ///
        /// <response code="200">Returns the new Flow's Id</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("V1/Flows")]
        public NewFlowCommandResult New([FromBody] NewFlowCommand command)
        {
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            NewFlowCommandResult result = (NewFlowCommandResult)_flowHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Updates a Flow
        /// </summary>
        ///
        /// <param name="id">Flow to be Updated</param>
        /// <param name="command">Update Flow Command</param>
        ///
        /// <response code="200">Returns the update's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("V1/Flows/{id}")]
        public CommandResult Update(Guid id, [FromBody] UpdateFlowCommand command)
        {
            command.setFlowId(id);
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_flowHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Deletes a Flow
        /// </summary>
        ///
        /// <param name="id">Flow to be deleted</param>
        ///
        /// <response code="200">Returns the delete's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("V1/Flows/{id}")]
        public CommandResult Delete(Guid id)
        {
            DeleteFlowCommand command = new DeleteFlowCommand() { Flow = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_flowHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Get Flow by Id
        /// </summary>
        ///
        /// <param name="id">Flow Id</param>
        ///
        /// <response code="200">Returns the Server</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("V1/Flows/{id}")]
        public GetFlowCommandResult Get(Guid id)
        {
            GetFlowCommand command = new GetFlowCommand() { Flow = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            GetFlowCommandResult result = (GetFlowCommandResult)_flowHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Flow = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }
    }
}