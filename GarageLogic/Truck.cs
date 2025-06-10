using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsCarryingHazardousSubstances;
        private float m_CargoVolume;
        private const float k_MaxFuelTankCapacity = 135f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const int k_MaxTireAirPressure = 27;
        private const int k_NumOfWheels = 12;
        public override int NumberOfWheels { get; } = k_NumOfWheels;
        public override float MaxAirPressure { get; } = k_MaxTireAirPressure;

        internal Truck(string i_LicencePlate, string i_ModelName)
            : base(i_LicencePlate, i_ModelName, new FuelProvider(k_MaxFuelTankCapacity, k_FuelType))
        {
        }

        public override List<VehicleSpecificField> GetSpecificFields()
        {
            List<VehicleSpecificField> truckSpecificFields = new List<VehicleSpecificField>();
            VehicleSpecificField carringHazadarousSubstances, cargoVolume;

            carringHazadarousSubstances = new VehicleSpecificField("isCarringHazardousSubstances", typeof(bool));
            cargoVolume = new VehicleSpecificField("cargoVolume", typeof(float));
            truckSpecificFields.Add(carringHazadarousSubstances);
            truckSpecificFields.Add(cargoVolume);

            return truckSpecificFields;
        }

        public override void SetSpecificFields(string i_FieldName, string i_Value)
        {
            switch (i_FieldName)
            {
                case "isCarringHazardousSubstances":
                    if (bool.TryParse(i_Value, out bool isCarringHazardousSubstances))
                    {
                        m_IsCarryingHazardousSubstances = isCarringHazardousSubstances;
                    }
                    else
                    {
                        throw new FormatException("The entered value for the carriage of hazardous substances is not true/flase.");
                    }

                    break;
                case "cargoVolume":
                    if (float.TryParse(i_Value, out float cargoVolume))
                    {
                        if (cargoVolume >= 0)
                        {
                            m_CargoVolume = cargoVolume;
                        }
                        else
                        {
                            throw new ValueRangeException("The cargoVolume must be a positive number", float.MaxValue, 0.0F);
                        }
                    }
                    else
                    {
                        throw new FormatException("The entered value for cargo volume is not a number.");
                    }

                    break;
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format(
@"Is carrying hazardous substances: {0}
Cargo volume: {1}", m_IsCarryingHazardousSubstances, m_CargoVolume);
        }
    }
}