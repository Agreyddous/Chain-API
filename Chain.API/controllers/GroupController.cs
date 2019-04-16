using System;
using Chain.Domain.Context.Commands;
using Chain.Domain.Context.Commands.Groups;
using Chain.Domain.Context.Handlers;
using Chain.Domain.Context.Services;
using Chain.Shared.Context.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Chain.API.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupCommandHandler _groupHandler;
        private readonly ILoggingService _loggingService;

        public GroupController(GroupCommandHandler groupHandler, ILoggingService loggingService)
        {
            _groupHandler = groupHandler;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Creates a new Group
        /// </summary>
        ///
        /// <param name="command">New Group Command</param>
        ///
        /// <response code="200">Returns the new Group's Id</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("V1/Groups")]
        public NewGroupCommandResult New([FromBody] NewGroupCommand command)
        {
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            NewGroupCommandResult result = (NewGroupCommandResult)_groupHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Updates a Group
        /// </summary>
        ///
        /// <param name="id">Group to be Updated</param>
        /// <param name="command">Update Group Command</param>
        ///
        /// <response code="200">Returns the update's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("V1/Groups/{id}")]
        public CommandResult Update(Guid id, [FromBody] UpdateGroupCommand command)
        {
            command.setGroupId(id);
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_groupHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Deletes a Group
        /// </summary>
        ///
        /// <param name="id">Group to be deleted</param>
        ///
        /// <response code="200">Returns the delete's output</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("V1/Groups/{id}")]
        public CommandResult Delete(Guid id)
        {
            DeleteGroupCommand command = new DeleteGroupCommand() { Group = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            CommandResult result = (CommandResult)_groupHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }

        /// <summary>
        /// Get Group by Id
        /// </summary>
        ///
        /// <param name="id">Group Id</param>
        ///
        /// <response code="200">Returns the Server</response>
        /// <response code="400">Invalid request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("V1/Groups/{id}")]
        public GetGroupCommandResult Get(Guid id)
        {
            GetGroupCommand command = new GetGroupCommand() { Group = id };
            command.setRequestHost(HttpContext.Request.Host.ToString());

            _loggingService.Log(this.GetType(), ELogType.Input, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method });

            GetGroupCommandResult result = (GetGroupCommandResult)_groupHandler.Handle(command);

            _loggingService.Log(this.GetType(), ELogType.Output, ELogLevel.Info, new { Group = this.User.Identity.Name, Path = this.Request.Path, Method = this.Request.Method, Code = this.Response.StatusCode });

            HttpContext.Response.StatusCode = result.Code;

            return result;
        }
    }
}