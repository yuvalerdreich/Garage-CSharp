using System;

namespace Ex03.GarageLogic
{
    public class EnergySource
    {
        private float m_CurrEnergyLeft;
        private float m_MaxEnergyAmount;

        public EnergySource(float i_MaxEnergyAnount)
        {
            m_MaxEnergyAmount = i_MaxEnergyAnount;
        }

        public float CurrEnergyLeft
        {
            get
            {
                return m_CurrEnergyLeft;
            }
            set
            {
                if (value < 0.0f || value > m_MaxEnergyAmount)
                {
                    throw new ValueRangeException("Current energy value out of range.", m_MaxEnergyAmount, 0.0f);
                }

                m_CurrEnergyLeft = value;
            }
        }

        public float MaxEnergyAmount
        {
            get
            {
                return m_MaxEnergyAmount;
            }
        }

        public float CalcEnergyPrecentage()
        {
            return m_CurrEnergyLeft / m_MaxEnergyAmount * 100f;
        }

        public override string ToString()
        {
            return String.Format(
@"Current energy left: {0}
Max energy: {1}
",
m_CurrEnergyLeft, m_MaxEnergyAmount);
        }
    }
}