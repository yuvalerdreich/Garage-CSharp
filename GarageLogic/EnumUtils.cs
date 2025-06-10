using System;

namespace Ex03.GarageLogic
{
    public static class EnumUtils
    {
        public static T ParseEnumByString<T>(string i_Str, string i_Message)
        {
            T parsedToEnumValue = default(T);

            if (Enum.IsDefined(typeof(T), i_Str))
            {
                parsedToEnumValue = (T)Enum.Parse(typeof(T), i_Str, true);
            }
            else
            {
                throw new FormatException(i_Message);
            }

            return parsedToEnumValue;
        }

        public static T ParseEnumByInt<T>(string i_Str, string i_Message)
        {
            T parsedToEnumValue = default(T);

            if (int.TryParse(i_Str, out int intValue))
            {
                if (Enum.IsDefined(typeof(T), intValue))
                {
                    parsedToEnumValue = (T)Enum.ToObject(typeof(T), intValue);
                }
                else
                {
                    throw new FormatException(i_Message);
                }
            }
            else
            {
                throw new FormatException(i_Message);
            }

            return parsedToEnumValue;
        }
    }
}