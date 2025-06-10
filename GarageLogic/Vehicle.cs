using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private readonly string r_LicencePlateNumber;
        private float m_RemainingEnergyPrecentege;
        private List<Wheel> m_VehicleWheels = new List<Wheel>();
        private VehicleOwner m_Owner;
        private eVehicleStatus m_VehicleStatus;
        protected EnergySource m_EnergySource;
        public abstract int NumberOfWheels { get; }
        public abstract float MaxAirPressure { get; }

        protected Vehicle(string i_LicencePlate, string i_ModelName, EnergySource i_EnergySource)
        {
            r_LicencePlateNumber = i_LicencePlate;
            m_ModelName = i_ModelName;
            m_EnergySource = i_EnergySource;
        }

        public string ModelName
        {
            get
            {
                return this.m_ModelName;
            }
        }

        public string LicencePlate
        {
            get
            {
                return this.r_LicencePlateNumber;
            }
        }

        public float RemainingEnergyPrecentege
        {
            get
            {
                return this.m_RemainingEnergyPrecentege;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    m_RemainingEnergyPrecentege = value;
                }
                else
                {
                    throw new ValueRangeException("Current energy precentage out of range.", 100, 0);
                }
            }
        }

        public List<Wheel> VehicleWheels
        {
            get
            {
                return this.m_VehicleWheels;
            }
        }

        public VehicleOwner Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                m_VehicleStatus = value;
            }
        }

        public EnergySource EnergySource
        {
            get
            {
                return m_EnergySource;
            }
        }

        public abstract List<VehicleSpecificField> GetSpecificFields();

        public abstract void SetSpecificFields(string i_FieldName, string i_Value);

        public void SetVehicleOwner(string i_Name, string i_PhoneNumber)
        {
            m_Owner = new VehicleOwner(i_Name, i_PhoneNumber);
        }

        public void SetRemainingEnergyPrecentageWithCurrPrecentage(float i_CurrEnergyAmount)
        {
            m_EnergySource.CurrEnergyLeft = i_CurrEnergyAmount;
            m_RemainingEnergyPrecentege = m_EnergySource.CalcEnergyPrecentage();
        }

        public void SetCurrEnergyWithPrecentage(float i_CurrEnergyPrecentage)
        {
            this.m_RemainingEnergyPrecentege = i_CurrEnergyPrecentage;
            m_EnergySource.CurrEnergyLeft = (i_CurrEnergyPrecentage / 100) * m_EnergySource.MaxEnergyAmount;
        }

        public void SetWheelsOfVehicle(string i_ProducerName, float i_MaxAirPressure, List<float> i_CurrAirPressureLst)
        {
            Wheel vehicleWheel;

            foreach (float airPressure in i_CurrAirPressureLst)
            {
                vehicleWheel = new Wheel(i_ProducerName, i_MaxAirPressure);
                vehicleWheel.CurrAirPressure = airPressure;
                m_VehicleWheels.Add(vehicleWheel);
            }
        }

        public void PumpVehicleWheelsToMax()
        {
            foreach (Wheel wheel in m_VehicleWheels)
            {
                wheel.InflateTireToMax();
            }
        }

        private string wheelsInfoAsStr()
        {
            StringBuilder wheelsIfoConcatenated = new StringBuilder();
            int currTireIdx = 0;

            foreach (Wheel wheel in m_VehicleWheels)
            {
                currTireIdx++;
                wheelsIfoConcatenated.AppendLine(String.Format("Wheel {0} info: {1}", currTireIdx, wheel.ToString()));
            }

            return wheelsIfoConcatenated.ToString();
        }

        public override string ToString()
        {
            return String.Format(
@"licence plate number: {0}
Model name: {1}
Owner name: {2}
Vehicle status in garage: {3}
Wheels information:
{4}Energy source information:
{5}", r_LicencePlateNumber, m_ModelName, m_Owner.Name, this.VehicleStatus, wheelsInfoAsStr(), m_EnergySource.ToString());
        }
    }
}