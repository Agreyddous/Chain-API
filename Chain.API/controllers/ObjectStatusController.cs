using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.ObjectStatus;
using Chain.Domain.Context.Handlers;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Chain.API.Controllers
{
    public class ObjectStatusController : Controller
    {
        private readonly ObjectStatusCommandHandler _objectStatusHandler;
        private readonly ILoggingService _loggingService;

        public ObjectStatusController(ObjectStatusCommandHandler objectStatusHandler, ILoggingService loggingService)
        {
            _objectStatusHandler = objectStatusHandler;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates a new Object Status
        /// </summary>
        ///
        /// <param name="command">New Object Status Command</param>
        ///
        /// <response code="200">Returns the new ObjectStatus's Id</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("V1/ObjectStatuss")]
        public NewObjectStatusCommandResult New([FromBody] NewObjectStatusCommand command)
        {
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            NewObjectStatusCommandResult result = (NewObjectStatusCommandResult)_objectStatusHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Updates a Object Status
        /// </summary>
        ///
        /// <param name="id">Object Status to be Updated</param>
        /// <param name="command">Update Object Status Command</param>
        ///
        /// <response code="200">Returns the update's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("V1/ObjectStatuss/{id}")]
        public CommandResult Update(Guid id, [FromBody] UpdateObjectStatusCommand command)
        {
            command.setObjectStatusId(id);
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_objectStatusHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Deletes a Object Status
        /// </summary>
        ///
        /// <param name="id">Object Status to be deleted</param>
        ///
        /// <response code="200">Returns the delete's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("V1/ObjectStatuss/{id}")]
        public CommandResult Delete(Guid id)
        {
            DeleteObjectStatusCommand command = new DeleteObjectStatusCommand() { ObjectStatus = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_objectStatusHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Get Object Status by Id
        /// </summary>
        ///
        /// <param name="id">Object Status Id</param>
        ///
        /// <response code="200">Returns the Server</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("V1/ObjectStatuss/{id}")]
        public GetObjectStatusCommandResult Get(Guid id)
        {
            GetObjectStatusCommand command = new GetObjectStatusCommand() { ObjectStatus = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            GetObjectStatusCommandResult result = (GetObjectStatusCommandResult)_objectStatusHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectStatus = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }
    }
}