using System;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const float k_MaxFuelTankCapacity = 48f;
        private const eFuelType k_FuelType = eFuelType.Octan95;

        internal FuelCar(string i_LicencePlate, string i_ModelName)
            : base(i_LicencePlate, i_ModelName, new FuelProvider(k_MaxFuelTankCapacity, k_FuelType))
        {
        }
    }
}