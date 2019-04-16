using System;
using Chain.Shared.Context.Entities;
using FluentValidator.Validation;

namespace Chain.Domain.Context.Entities
{
    public class Object : Entity
    {
        protected Object() { }

        public Object(string title, string description, Guid creator, Guid father, string path, Guid type, Guid stauts)
        {
            Title = title;
            Description = description;
            Creator = creator;
            Father = father;
            Path = path;
            Type = type;
            Status = stauts;

            CreationDate = DateTime.Now;

            ValidationContract contract = new ValidationContract();

            if (contract.IsNotNullOrEmpty(Title, "Title", "Value is Invalid").Valid)
                contract.HasMinLen(Title, 3, "Title", "Is too short")
                    .HasMaxLen(Title, 45, "Title", "Is too long");

            if (contract.IsNotNullOrEmpty(Description, "Description", "Value is Invalid").Valid)
                contract.HasMinLen(Description, 3, "Description", "Is too short")
                    .HasMaxLen(Description, 45, "Description", "Is too long");

            contract.IsNotNull(Creator, "Creator", "Value is Invalid");

            contract.IsNotNull(Father, "Father", "Value is Invalid");

            if (contract.IsNotNullOrEmpty(Path, "Path", "Value is Invalid").Valid)
                contract.HasMinLen(Path, 3, "Path", "Is too short")
                    .HasMaxLen(Path, 45, "Path", "Is too long");

            contract.IsNotNull(Type, "Type", "Value is Invalid");

            contract.IsNotNull(Status, "Status", "Value is Invalid");


            AddNotifications(contract);
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid Creator { get; private set; }
        public Guid Father { get; private set; }
        public string Path { get; private set; }
        public Guid Type { get; private set; }
        public Guid Status { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}