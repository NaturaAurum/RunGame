using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Stella.Utils
{
    public sealed class EnumComparer<TEnum> : IEqualityComparer<TEnum>
        where TEnum : struct, IComparable, IConvertible, IFormattable
    {
        private static readonly Func<TEnum, TEnum, bool> equals;
        private static readonly Func<TEnum, int> getHashCode;

        /// <summary>
        /// The singleton accessor.
        /// </summary>
        public static readonly EnumComparer<TEnum> Instance;


        /// <summary>
        /// Initializes the <see cref="EnumComparer{TEnum}"/> class
        /// by generating the GetHashCode and Equals methods.
        /// </summary>
        static EnumComparer()
        {
            getHashCode = generateGetHashCode();
            equals = generateEquals();
            Instance = new EnumComparer<TEnum>();
        }

        /// <summary>
        /// A private constructor to prevent user instantiation.
        /// </summary>
        private EnumComparer()
        {
            assertTypeIsEnum();
            assertUnderlyingTypeIsSupported();
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <typeparamref name="TEnum"/> to compare.</param>
        /// <param name="y">The second object of type <typeparamref name="TEnum"/> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(TEnum x, TEnum y)
        {
            // call the generated method
            return equals(x, y);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.
        /// </exception>
        public int GetHashCode(TEnum obj)
        {
            // call the generated method
            return getHashCode(obj);
        }

        private static void assertTypeIsEnum()
        {
            if (typeof(TEnum).IsEnum)
                return;

            var message =
                string.Format("The type parameter {0} is not an Enum. LcgEnumComparer supports Enums only.",
                    typeof(TEnum));
            throw new NotSupportedException(message);
        }

        private static void assertUnderlyingTypeIsSupported()
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            ICollection<Type> supportedTypes =
                new[]
                {
                    typeof(byte), typeof(sbyte), typeof(short), typeof(ushort),
                    typeof(int), typeof(uint), typeof(long), typeof(ulong)
                };

            if (supportedTypes.Contains(underlyingType))
                return;

            var message =
                string.Format("The underlying type of the type parameter {0} is {1}. " +
                              "LcgEnumComparer only supports Enums with underlying type of " +
                              "byte, sbyte, short, ushort, int, uint, long, or ulong.",
                    typeof(TEnum), underlyingType);
            throw new NotSupportedException(message);
        }

        /// <summary>
        /// Generates a comparison method similiar to this:
        /// <code>
        /// bool Equals(TEnum x, TEnum y)
        /// {
        ///     return x == y;
        /// }
        /// </code>
        /// </summary>
        /// <returns>The generated method.</returns>
        private static Func<TEnum, TEnum, bool> generateEquals()
        {
            var xParam = Expression.Parameter(typeof(TEnum), "x");
            var yParam = Expression.Parameter(typeof(TEnum), "y");
            var equalExpression = Expression.Equal(xParam, yParam);
            return Expression.Lambda<Func<TEnum, TEnum, bool>>(equalExpression, new[] {xParam, yParam}).Compile();
        }

        /// <summary>
        /// Generates a GetHashCode method similar to this:
        /// <code>
        /// int GetHashCode(TEnum obj)
        /// {
        ///     return ((int)obj).GetHashCode();
        /// }
        /// </code>
        /// </summary>
        /// <returns>The generated method.</returns>
        private static Func<TEnum, int> generateGetHashCode()
        {
            var objParam = Expression.Parameter(typeof(TEnum), "obj");
            var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
            var convertExpression = Expression.Convert(objParam, underlyingType);
            var getHashCodeMethod = underlyingType.GetMethod("GetHashCode");
            var getHashCodeExpression = Expression.Call(convertExpression, getHashCodeMethod);
            return Expression.Lambda<Func<TEnum, int>>(getHashCodeExpression, new[] {objParam}).Compile();
        }
    }

    public class EnumUtil<TEnum> where TEnum : struct, IComparable, IConvertible, IFormattable
    {
        public static TEnum[] All;

        static readonly Dictionary<TEnum, string> nameTable = new Dictionary<TEnum, string>(EnumComparer<TEnum>.Instance);
        static readonly Dictionary<string, TEnum> keyTable = new Dictionary<string, TEnum>();

        public static int TotalCount;

        static EnumUtil() {
            Initialize();
        }

        protected static void Initialize() {
            nameTable.Clear();
            keyTable.Clear();

            var values = Enum.GetValues(typeof(TEnum));
            foreach (var val in values) {
                nameTable[(TEnum)val] = val.ToString();
                keyTable[val.ToString()] = (TEnum)val;
            }

            TotalCount = values.Length;

            All = new TEnum[values.Length];
            for (int i = 0; i < values.Length; i++) {
                All[i] = (TEnum)values.GetValue(i);
            }
        }

        public static string ConvertString(TEnum t) {
            string val;
            if(nameTable.TryGetValue(t, out val)) {
                return val;
            }

            Debug.Assert(false, $"unknown enum str : {t.ToString()}");
            return "";
        }

        public static TEnum ConvertEnum(string name) {
            TEnum val;
            if (keyTable.TryGetValue(name, out val)) {
                return val;
            }
            Debug.Assert(false, $"Unknown enum name : {name}");
            return default;
        }
    }
}
