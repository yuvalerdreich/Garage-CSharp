using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eMotorcycleLicenceType m_LicenceType;
        private int m_EngineVolume;
        private const int k_MaxTireAirPressure = 30;
        private const int k_NumOfWheels = 2;
        public override int NumberOfWheels { get; } = k_NumOfWheels;
        public override float MaxAirPressure { get; } = k_MaxTireAirPressure;

        internal Motorcycle(string i_LicencePlate, string i_ModelName, EnergySource i_EnergySource)
            : base(i_LicencePlate, i_ModelName, i_EnergySource)
        {
        }

        public override List<VehicleSpecificField> GetSpecificFields()
        {
            List<VehicleSpecificField> CarSpecificFields = new List<VehicleSpecificField>();
            VehicleSpecificField licenceType, engineVolume;

            licenceType = new VehicleSpecificField("licenceType", typeof(eMotorcycleLicenceType));
            engineVolume = new VehicleSpecificField("engineVolume", typeof(int));
            CarSpecificFields.Add(licenceType);
            CarSpecificFields.Add(engineVolume);

            return CarSpecificFields;
        }

        public override void SetSpecificFields(string i_FieldName, string i_Value)
        {
            switch (i_FieldName)
            {
                case "licenceType":
                    m_LicenceType = EnumUtils.ParseEnumByString<eMotorcycleLicenceType>(i_Value, "Not a possible licence type value. The possible values are: A, A2, AB, B2.");
                    break;
                case "engineVolume":
                    if (int.TryParse(i_Value, out int engineVolume))
                    {
                        if (engineVolume >= 0)
                        {
                            m_EngineVolume = engineVolume;
                        }
                        else 
                        {
                            throw new ValueRangeException("The engineVolume must be a positive number", float.MaxValue, 0.0F);
                        }
                    }
                    else
                    {
                        throw new FormatException("The entered value for engine volume is not a number.");
                    }

                    break;
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format(
@"licence type: {0}
Engine volume: {1}", m_LicenceType, m_EngineVolume);
        }
    }
}