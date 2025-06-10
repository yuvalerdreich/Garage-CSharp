using System;

namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private string m_Name;
        private string m_PhoneNumber;

        public VehicleOwner(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Name = i_OwnerName;
            m_PhoneNumber = i_OwnerPhoneNumber;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_PhoneNumber;
            }
            set
            {
                m_PhoneNumber = value;
            }
        }
    }
}