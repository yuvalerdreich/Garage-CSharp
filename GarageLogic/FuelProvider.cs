using System;

namespace Ex03.GarageLogic
{
    public class FuelProvider : EnergySource
    {
        private readonly eFuelType r_FuelType;

        public FuelProvider(float i_MaxFuelCapacity, eFuelType i_FuelType)
            : base(i_MaxFuelCapacity)
        {
            r_FuelType = i_FuelType;
        }

        public void FillFuelTank(float i_FuelAmountToAdd, eFuelType i_FuelType)
        {
            float totalFuelAmount = base.CurrEnergyLeft + i_FuelAmountToAdd;

            if (i_FuelType == r_FuelType)
            {
                if (totalFuelAmount <= base.MaxEnergyAmount && totalFuelAmount >= 0.0f)
                {
                    this.CurrEnergyLeft += i_FuelAmountToAdd;
                }
                else
                {
                    throw new ValueRangeException("Energy amount to fill is out of range!", base.MaxEnergyAmount - base.CurrEnergyLeft, 0.0f);
                }
            }
            else
            {
                throw new ArgumentException(String.Format("Fuel type sould be {0}", r_FuelType));
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format(
@"Fuel type: {0}
", r_FuelType.ToString());
        }
    }
}