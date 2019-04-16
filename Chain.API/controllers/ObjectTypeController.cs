using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.ObjectTypes;
using Chain.Domain.Context.Handlers;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Chain.API.Controllers
{
    public class ObjectTypeController : Controller
    {
        private readonly ObjectTypeCommandHandler _objectTypeHandler;
        private readonly ILoggingService _loggingService;

        public ObjectTypeController(ObjectTypeCommandHandler objectTypeHandler, ILoggingService loggingService)
        {
            _objectTypeHandler = objectTypeHandler;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates a new Object Type
        /// </summary>
        ///
        /// <param name="command">New Object Type Command</param>
        ///
        /// <response code="200">Returns the new ObjectType's Id</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("V1/ObjectTypes")]
        public NewObjectTypeCommandResult New([FromBody] NewObjectTypeCommand command)
        {
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            NewObjectTypeCommandResult result = (NewObjectTypeCommandResult)_objectTypeHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Updates a Object Type
        /// </summary>
        ///
        /// <param name="id">Object Type to be Updated</param>
        /// <param name="command">Update Object Type Command</param>
        ///
        /// <response code="200">Returns the update's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("V1/ObjectTypes/{id}")]
        public CommandResult Update(Guid id, [FromBody] UpdateObjectTypeCommand command)
        {
            command.setObjectTypeId(id);
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_objectTypeHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Deletes a Object Type
        /// </summary>
        ///
        /// <param name="id">Object Type to be deleted</param>
        ///
        /// <response code="200">Returns the delete's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("V1/ObjectTypes/{id}")]
        public CommandResult Delete(Guid id)
        {
            DeleteObjectTypeCommand command = new DeleteObjectTypeCommand() { ObjectType = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_objectTypeHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Get Object Type by Id
        /// </summary>
        ///
        /// <param name="id">Object Type Id</param>
        ///
        /// <response code="200">Returns the Server</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("V1/ObjectTypes/{id}")]
        public GetObjectTypeCommandResult Get(Guid id)
        {
            GetObjectTypeCommand command = new GetObjectTypeCommand() { ObjectType = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            GetObjectTypeCommandResult result = (GetObjectTypeCommandResult)_objectTypeHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { ObjectType = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }
    }
}