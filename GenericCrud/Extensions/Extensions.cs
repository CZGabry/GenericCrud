using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace GenericCrud.Extensions
{
    public static class Extensions
    {
        /// <summary>Converts a string (that must be in snake_case) into a PascalCase string.</summary>
        public static string ToPascalCase(this string input)
        {
            // If the input is already a PascalCase, return it without any conversion.
            if (input.Any(char.IsUpper) && !input.Contains('_')) return input;

            string temp = input.Replace("_", " ");
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;
            return info.ToTitleCase(temp).Replace(" ", string.Empty);
        }

        /// <summary>
        /// Converts a string (that must be in PascalCase) into a snake_case string.
        /// </summary>
        public static string ToSnakeCase(this string input)
        {
            var result = string.Concat(input.Select((character, index) =>
            {
                // Foreach character add underscore if is upper, first character is excluded.
                return index > 0 && char.IsUpper(character) ? "_" + character : character.ToString();
            }));

            return result.ToLower();
        }

        public static string SafeMaxLength(this string input, int maxLength)
        {
            if (input == null) return null;
            return input.Length > maxLength ? input.Substring(0, maxLength) : input;
        }

        /// <summary>Returns the age of a person, given his birthdate. Handles cases with leap years.</summary>
        public static int GetAge(this DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;

            // Go back to the year the person was born in case of a leap year.
            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }

        /// <summary>
        /// Returns all distinct elements of the given source, where "distinctness"
        /// is determined via a projection and the default equality comparer for the projected type.
        /// </summary>
        /// <remarks>
        /// This operator uses deferred execution and streams the results, although
        /// a set of already-seen keys is retained. If a key is seen multiple times,
        /// only the first element with that key is returned.
        /// </remarks>
        /// <typeparam name="TSource">Type of the source sequence</typeparam>
        /// <typeparam name="TKey">Type of the projected element</typeparam>
        /// <param name="source">Source sequence</param>
        /// <param name="keySelector">Projection for determining "distinctness"</param>
        /// <returns>A sequence consisting of distinct elements from the source sequence,
        /// comparing them by the specified key projection.</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Retrieves the default value of an object property via the PropertyInfo class
        /// </summary>
        /// <param name="property">PropertyInfo where get the default value</param>
        /// <returns></returns>
        public static object? GetDefaultValue(this PropertyInfo property)
        {
            var defaultAttr = property.GetCustomAttribute(typeof(DefaultValueAttribute));
            if (defaultAttr != null)
                return (defaultAttr as DefaultValueAttribute)?.Value;

            var propertyType = property.PropertyType;
            return propertyType.IsValueType ? Activator.CreateInstance(propertyType) : null;
        }

        /// <summary>Restituisce la descrizione di un enum</summary>
        public static string GetDescriptionAttribute(this System.Enum value)
        {
            if (value == null) return string.Empty;

            return value.GetType().GetMember(value.ToString()).First().GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute ? attribute.Description : value.ToString();
        }

        // Restituisce false se l'oggetto è nullo o una lista vuota, altrimenti true
        public static bool IsNullOrEmpty(this object item)
        {
            if (item == null)
                return true;

            if (item is object[] objectsArray)
                return objectsArray.Length == 0;

            if (item is IEnumerable<object> objectsEnumerable)
                return !objectsEnumerable.Any();

            return false;
        }

        public static decimal Round(this decimal _this, int digits = 2, MidpointRounding mode = MidpointRounding.AwayFromZero)
        {
            return Math.Round(_this, digits, mode);
        }
    }
}
