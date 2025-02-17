using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Domain.Entities.Common
{

    public partial class Enumeration : IComparable
    {
        public const string NameField = "Name";

        public string Code { get; set; }
        public string Name { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EnumId { get; private set; }

        //public List<Translate> Translates { get; set; } = new List<Translate>();


        protected Enumeration()
        {
        }

        protected Enumeration(int enumId, string name)
        {
            EnumId = enumId;
            Name = name;
        }

        protected Enumeration(int enumId, string code, string name)
        {
            EnumId = enumId;
            Name = name;
            Code = code;
        }


        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = EnumId.Equals(otherValue.EnumId);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => EnumId.GetHashCode();

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.EnumId - secondValue.EnumId);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.EnumId == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        public int CompareTo(object other) => EnumId.CompareTo(((Enumeration)other).EnumId);

        public static T GetById<T>(int id) where T : Enumeration
        {
            return Enumeration.GetAll<T>().FirstOrDefault(x => x.EnumId == id);
        }

        public virtual void SeedTranslate()
        {
        }

        public static int GetMinEnumId<T>() where T : Enumeration
        {
            return Enumeration.GetAll<T>().Min(x => x.EnumId);
        }

        public static int GetMaxEnumId<T>() where T : Enumeration
        {
            return Enumeration.GetAll<T>().Max(x => x.EnumId);
        }
    }
}
