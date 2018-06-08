using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planar.Model
{
    //TODO:Nullables!

    public interface IMaybe<T>
    {
        bool Exists { get; }
        T Value { get; }

    }

    public class MaybeValue<T> : IMaybe<T> where T : struct
    {
        public bool Exists { get; }
        public T Value => Exists ? _value : throw new InvalidOperationException("No value");

        private T _value;

        public MaybeValue(T value)
        {
            Exists = true;
            _value = value;
        }

        public MaybeValue()
        {
            Exists = false;
        }
    }

    public class ReferenceMaybe<T> : IMaybe<T> where T : class
    {
        public bool Exists => Value != null;

        public T Value { get; }

        public ReferenceMaybe(T value)
        {
            Value = value;
        }

        public ReferenceMaybe()
        {
        }
    }


    public static class GenericExtensions
    {
        public static bool IsNullable<T>(this T value)
        {
            var type = typeof(T);
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }

}
