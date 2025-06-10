using System;

namespace Ex03.GarageLogic
{
    public class ValueRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return r_MinValue;
            }
        }

        public ValueRangeException(string i_Message, float i_MaxValue, float i_MinValue)
            : base(String.Format("{0}. Value must be between {1} to {2}", i_Message, i_MinValue, i_MaxValue))
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }
    }
}