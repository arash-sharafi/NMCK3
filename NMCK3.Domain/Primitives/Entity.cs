﻿using System;

namespace NMCK3.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        protected Entity()
        {

        }
        protected Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private init; }

        public static bool operator ==(Entity left, Entity right)
        {
            return left is not null && right is not null & left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType() || obj is not Entity entity)
                return false;

            return entity.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool Equals(Entity other)
        {
            if (other is null || other.GetType() != GetType())
                return false;

            return other.Id == Id;
        }
    }
}
