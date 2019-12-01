using Plumsail.Api.Models.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Plumsail.Api.Utils
{
    public static class EnumService
    {
        public static string GetDescription(this Enum @enum)
        {
            return @enum
                .GetType()
                .GetMember(@enum.ToString())
                .FirstOrDefault()?
                    .GetCustomAttribute<DescriptionAttribute>()?.Description;
        }

        public static List<IntOption> GetIntOptions<T>()
        //where T: Enum
        {
            var underLyingType = Enum.GetUnderlyingType(typeof(T));
            if (!underLyingType.Equals(typeof(int))) throw new InvalidCastException($"Cannot cast {underLyingType} to int");

            List<IntOption> opts = new List<IntOption>();
            foreach (Enum enumItem in Enum.GetValues(typeof(T)))
            {
                opts.Add(new IntOption { Value = Convert.ToInt32(enumItem), Text = enumItem.GetDescription() });
            }

            return opts;
        }
    }
}
