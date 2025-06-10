using System;

namespace Ex03.GarageLogic
{
    public class ChargeProvider : EnergySource
    {
        public ChargeProvider(float i_MaxBatteryTime)
            : base(i_MaxBatteryTime)
        {
        }

        public void ChargeBattery(float i_HoursToAdd)
        {
            float totalFuelAmount = base.CurrEnergyLeft + i_HoursToAdd;

            if (totalFuelAmount <= base.MaxEnergyAmount && totalFuelAmount >= 0.0f)
            {
                this.CurrEnergyLeft += i_HoursToAdd;
            }
            else
            {
                throw new ValueRangeException("Energy amount to fill is out of range!", base.MaxEnergyAmount - base.CurrEnergyLeft, 0.0f);
            }
        }
    }
}