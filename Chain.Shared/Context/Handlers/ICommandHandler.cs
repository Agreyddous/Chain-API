using Chain.Shared.Context.Commands;

namespace Chain.Shared.Context.Handlers
{
    public interface ICommandHandler<T>
    {
        ICommandResult Handle(T command);
    }
}