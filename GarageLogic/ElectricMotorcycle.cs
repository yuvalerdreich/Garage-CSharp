using System;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryTime = 3.2f;

        public ElectricMotorcycle(string i_LicencePlate, string i_ModelName)
            : base(i_LicencePlate, i_ModelName, new ChargeProvider(k_MaxBatteryTime))
        {
        }
    }
}