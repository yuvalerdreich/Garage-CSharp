using System;

namespace Ex03.GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelTankCapacity = 5.8f;
        private const eFuelType k_FuelType = eFuelType.Octan98;

        internal FuelMotorcycle(string i_LicencePlate, string i_ModelName)
            : base(i_LicencePlate, i_ModelName, new FuelProvider(k_MaxFuelTankCapacity, k_FuelType))
        {
        }
    }
}