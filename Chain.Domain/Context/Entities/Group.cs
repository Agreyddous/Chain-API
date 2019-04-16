using Chain.Shared.Context.Entities;
using FluentValidator.Validation;

namespace Chain.Domain.Context.Entities
{
    public class Group : Entity
    {
        protected Group() { }

        public Group(string name, string description)
        {
            Name = name;
            Description = description;

            ValidationContract contract = new ValidationContract();

            if (contract.IsNotNullOrEmpty(Name, "Name", "Value is Invalid").Valid)
                contract.HasMinLen(Name, 3, "Name", "Is too short")
                    .HasMaxLen(Name, 45, "Name", "Is too long");

            if (contract.IsNotNullOrEmpty(Description, "Description", "Value is Invalid").Valid)
                contract.HasMinLen(Description, 3, "Description", "Is too short")
                    .HasMaxLen(Description, 255, "Description", "Is too long");

            AddNotifications(contract);
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}