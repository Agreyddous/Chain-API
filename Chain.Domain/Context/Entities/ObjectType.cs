using Chain.Shared.Context.Entities;
using FluentValidator.Validation;

namespace Chain.Domain.Context.Entities
{
    public class ObjectType : Entity
    {
        protected ObjectType() { }

        public ObjectType(string name)
        {
            Name = name;

            ValidationContract contract = new ValidationContract();

            if (contract.IsNotNullOrEmpty(Name, "Name", "Value is Invalid").Valid)
                contract.HasMinLen(Name, 3, "Name", "Is too short")
                    .HasMaxLen(Name, 100, "Name", "Is too long");

            AddNotifications(contract);
        }

        public string Name { get; private set; }
    }
}