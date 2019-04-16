using System;
using FluentValidator;

namespace Chain.Shared.Context.Entities
{
    public class Entity : Notifiable
    {
        public Entity() => Id = Guid.NewGuid();
        public Guid Id { get; private set; }
    }
}