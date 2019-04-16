using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.Object;
using Chain.Domain.Context.Handlers;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Chain.API.Controllers
{
    public class ObjectController : Controller
    {
        private readonly ObjectCommandHandler _objectHandler;
        private readonly ILoggingService _loggingService;

        public ObjectController(ObjectCommandHandler objectHandler, ILoggingService loggingService)
        {
            _objectHandler = objectHandler;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates a new Object
        /// </summary>
        ///
        /// <param name="command">New Object Command</param>
        ///
        /// <response code="200">Returns the new Object's Id</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("V1/Objects")]
        public NewObjectCommandResult New([FromBody] NewObjectCommand command)
        {
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            NewObjectCommandResult result = (NewObjectCommandResult)_objectHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Updates a Object
        /// </summary>
        ///
        /// <param name="id">Object to be Updated</param>
        /// <param name="command">Update Object Command</param>
        ///
        /// <response code="200">Returns the update's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("V1/Objects/{id}")]
        public CommandResult Update(Guid id, [FromBody] UpdateObjectCommand command)
        {
            command.setObjectId(id);
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_objectHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Deletes a Object
        /// </summary>
        ///
        /// <param name="id">Object to be deleted</param>
        ///
        /// <response code="200">Returns the delete's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("V1/Objects/{id}")]
        public CommandResult Delete(Guid id)
        {
            DeleteObjectCommand command = new DeleteObjectCommand() { Object = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_objectHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Get Object by Id
        /// </summary>
        ///
        /// <param name="id">Object Id</param>
        ///
        /// <response code="200">Returns the Server</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("V1/Objects/{id}")]
        public GetObjectCommandResult Get(Guid id)
        {
            GetObjectCommand command = new GetObjectCommand() { Object = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            GetObjectCommandResult result = (GetObjectCommandResult)_objectHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Object = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }
    }
}