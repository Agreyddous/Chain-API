using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.Users;
using Chain.Domain.Context.Handlers;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Chain.API.Controllers
{
    public class UserController : Controller
    {
        private readonly UserCommandHandler _userHandler;
        private readonly ILoggingService _loggingService;

        public UserController(UserCommandHandler userHandler, ILoggingService loggingService)
        {
            _userHandler = userHandler;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates a new User
        /// </summary>
        ///
        /// <param name="command">New User Command</param>
        ///
        /// <response code="200">Returns the new User's Id</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("V1/Users")]
        public NewUserCommandResult New([FromBody] NewUserCommand command)
        {
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            NewUserCommandResult result = (NewUserCommandResult)_userHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Updates a User
        /// </summary>
        ///
        /// <param name="id">User to be Updated</param>
        /// <param name="command">Update User Command</param>
        ///
        /// <response code="200">Returns the update's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("V1/Users/{id}")]
        public CommandResult Update(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.setUserId(id);
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_userHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Deletes a User
        /// </summary>
        ///
        /// <param name="id">User to be deleted</param>
        ///
        /// <response code="200">Returns the delete's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("V1/Users/{id}")]
        public CommandResult Delete(Guid id)
        {
            DeleteUserCommand command = new DeleteUserCommand() { User = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_userHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Get Server by Id
        /// </summary>
        ///
        /// <param name="id">User Id</param>
        ///
        /// <response code="200">Returns the Server</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("V1/Users/{id}")]
        public GetUserCommandResult Get(Guid id)
        {
            GetUserCommand command = new GetUserCommand() { User = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            GetUserCommandResult result = (GetUserCommandResult)_userHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { User = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }
    }
}