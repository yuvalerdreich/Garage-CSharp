using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_Color;
        private eCarDoorsNumber m_DoorsNumber;
        private const int k_MaxTireAirPressure = 32;
        private const int k_NumOfWheels = 5;
        public override int NumberOfWheels { get; } = k_NumOfWheels;
        public override float MaxAirPressure { get; } = k_MaxTireAirPressure;

        public Car(string i_LicencePlate, string i_ModelName, EnergySource i_EnergySource)
            : base(i_LicencePlate, i_ModelName, i_EnergySource)
        {
        }

        public override List<VehicleSpecificField> GetSpecificFields()
        {
            List<VehicleSpecificField> carSpecificFields = new List<VehicleSpecificField>();
            VehicleSpecificField carColor, carDoorNumber;

            carColor = new VehicleSpecificField("color", typeof(eCarColor));
            carDoorNumber = new VehicleSpecificField("doorNumber", typeof(eCarDoorsNumber));
            carSpecificFields.Add(carColor);
            carSpecificFields.Add(carDoorNumber);

            return carSpecificFields;
        }

        public override void SetSpecificFields(string i_FieldName, string i_Value)
        {
            switch (i_FieldName)
            {
                case "color":
                    m_Color = EnumUtils.ParseEnumByString<eCarColor>(i_Value, "Not a possible color value, the possible colors are: Yellow, Black, White, Silver");
                    break;
                case "doorNumber":
                    m_DoorsNumber = EnumUtils.ParseEnumByInt<eCarDoorsNumber>(i_Value, "Not a possible doors number value, the possible numbers are: 2, 3, 4, 5");
                    break;
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format(
@"Color: {0}
Number of doors: {1}", m_Color, m_DoorsNumber);
        }
    }
}