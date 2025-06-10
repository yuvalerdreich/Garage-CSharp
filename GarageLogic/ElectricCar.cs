using System;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryTime = 4.8f;

        public ElectricCar(string i_LicencePlate, string i_ModelName)
            : base(i_LicencePlate, i_ModelName, new ChargeProvider(k_MaxBatteryTime))
        {
        }
    }
}