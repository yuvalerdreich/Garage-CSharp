using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ProducerName;
        private readonly float r_MaxAirPressure;
        private float m_CurrAirPressure;

        public float CurrAirPressure
        {
            get
            {
                return CurrAirPressure;
            }
            set
            {
                if (value < 0.0f || value > r_MaxAirPressure)
                {
                    throw new ValueRangeException("Air pressure value is out of range.", r_MaxAirPressure, 0.0f);
                }
                else
                {
                    m_CurrAirPressure = value;
                }
            }
        }

        public Wheel(string i_ProducerName, float i_MaxAirPressure)
        {
            r_ProducerName = i_ProducerName;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void InflateTire(float i_AirToAdd)
        {
            if (m_CurrAirPressure + i_AirToAdd <= r_MaxAirPressure && i_AirToAdd >= 0)
            {
                m_CurrAirPressure += i_AirToAdd;
            }
            else
            {
                throw new ValueRangeException("Out of range of possible air to add to tire", r_MaxAirPressure - m_CurrAirPressure, 0.0f);
            }
        }

        public void InflateTireToMax()
        {
            float amountToAdd = r_MaxAirPressure - m_CurrAirPressure;
            InflateTire(amountToAdd);
        }

        public override string ToString()
        {
            return String.Format(
@"Producer name: {0}
Max air pressure: {1}
Current air pressure: {2}",
r_ProducerName, r_MaxAirPressure, m_CurrAirPressure);
        }
    }
}