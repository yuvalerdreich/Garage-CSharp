using System;

namespace Ex03.GarageLogic
{
    public class VehicleSpecificField
    {
        public string FieldName { get; }
        public Type FieldType { get; }

        public VehicleSpecificField(string i_FieldName, Type i_FieldType)
        {
            FieldName = i_FieldName;
            FieldType = i_FieldType;
        }
    }
}