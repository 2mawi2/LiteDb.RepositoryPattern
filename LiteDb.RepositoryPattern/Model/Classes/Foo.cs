using System;
using LiteDB;

namespace LiteDb.RepositoryPattern.Model.Classes
{
    public class Foo : IEquatable<Foo>
    {
        /// <summary>
        /// Qualified Id of the foo object for the LiteDb
        /// </summary>
        [BsonId(true)]
        public int Id { get; set; } = Guid.NewGuid().GetHashCode();

        public string Name { get; set; }

        /// <summary>
        /// Make sure to implement IEquatable for objects stored in LiteDb
        /// </summary>
        #region IEquatable

        public bool Equals(Foo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Foo)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(Foo left, Foo right) => Equals(left, right);

        public static bool operator !=(Foo left, Foo right) => !Equals(left, right);

        #endregion
    }
}