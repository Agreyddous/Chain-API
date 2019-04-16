using System;
using Chain.Shared.Context.Entities;
using Chain.Shared.Context.Enums;
using FluentValidator.Validation;

namespace Chain.Domain.Context.Entities
{
    public class Flow : Entity
    {
        protected Flow() { }

        public Flow(Guid owner, EOwnerType ownerType, Guid objectId, string path)
        {
            Owner = owner;
            OwnerType = ownerType;
            Object = objectId;
            Path = path;

            CreationDate = DateTime.Now;

            ValidationContract contract = new ValidationContract();

            contract.IsNotNull(Owner, "Owner", "Value is Invalid");

            contract.IsNotNull(OwnerType, "OwnerType", "Value is Invalid");

            contract.IsNotNull(Object, "Object", "Value is Invalid");

            if (contract.IsNotNullOrEmpty(Path, "Path", "Value is Invalid").Valid)
                contract.HasMinLen(Path, 3, "Path", "Is too short")
                    .HasMaxLen(Path, 800, "Path", "Is too long");

            AddNotifications(contract);
        }

        public Guid Owner { get; private set; }
        public EOwnerType OwnerType { get; private set; }
        public Guid Object { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Path { get; private set; }
    }
}