using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, Vehicle> r_GarageVehicles = new Dictionary<string, Vehicle>();

        public Dictionary<string, Vehicle> GarageVehicles
        {
            get
            {
                return r_GarageVehicles;
            }
        }

        public bool CheckIfVehicleExistsInGarage(string i_LicencePlate)
        {
            bool isExists = false;

            if (r_GarageVehicles.ContainsKey(i_LicencePlate))
            {
                isExists = true;
            }

            return isExists;
        }

        public void UpdateVehicleStatus(string i_LicencePlate, eVehicleStatus i_VehicleStatus)
        {
            Vehicle vehicle = GetVehicleByLicencePlate(i_LicencePlate);
            vehicle.VehicleStatus = i_VehicleStatus;
        }

        public void AddNewVehicleToGarage(Vehicle i_NewVehicle, string i_LicencePlate)
        {
            r_GarageVehicles.Add(i_LicencePlate, i_NewVehicle);
        }

        public List<string> GetLicencePlateList()
        {
            List<string> licencePlates = new List<string>();

            foreach (string licencePlate in r_GarageVehicles.Keys)
            {
                licencePlates.Add(licencePlate);
            }

            return licencePlates;
        }

        public List<string> GarageVehicleListFilteredByStatus(eVehicleStatus i_VehicleStatus)
        {
            List<string> garageVehicles = new List<string>();
            Vehicle vehicle;

            foreach (string licencePlate in r_GarageVehicles.Keys)
            {
                vehicle = r_GarageVehicles[licencePlate];
                if (vehicle.VehicleStatus == i_VehicleStatus)
                {
                    garageVehicles.Add(licencePlate);
                }
            }

            return garageVehicles;
        }

        public void InflateVehiclesTiresToMax(string i_LicencePlate)
        {
            Vehicle currVehicle = GetVehicleByLicencePlate(i_LicencePlate);
            currVehicle.PumpVehicleWheelsToMax();
        }

        public void PumpFuelToVehicle(string i_LicencePlate, eFuelType i_FuelType, float i_AmountToFill)
        {
            Vehicle currVehicle = GetVehicleByLicencePlate(i_LicencePlate);
            FuelProvider fuelProvider = currVehicle.EnergySource as FuelProvider;

            if (fuelProvider != null)
            {
                fuelProvider.FillFuelTank(i_AmountToFill, i_FuelType);
            }
            else
            {
                throw new ArgumentException("To add fuel, your vehicle type must be powered with fuel.");
            }
        }

        public void ChargeVehicle(string i_LicencePlate, float i_TimeToCharge)
        {
            Vehicle currVehicle = GetVehicleByLicencePlate(i_LicencePlate);
            ChargeProvider chargeProvider = currVehicle.EnergySource as ChargeProvider;

            if (chargeProvider != null)
            {
                chargeProvider.ChargeBattery(i_TimeToCharge);
            }
            else
            {
                throw new ArgumentException("To to charge your vehicle, it's type must be powered with electricity.");
            }
        }

        public string ProvideFullVehicleDetails(string i_LicencePlate)
        {
            Vehicle vehicle = GetVehicleByLicencePlate(i_LicencePlate);
            return vehicle.ToString();
        }

        public Vehicle GetVehicleByLicencePlate(string i_LicencePlate)
        {
            Vehicle vehicleRequested;

            if (CheckIfVehicleExistsInGarage(i_LicencePlate))
            {
                vehicleRequested = r_GarageVehicles[i_LicencePlate];
            }
            else
            {
                throw new ArgumentException("The requested vehicle does not exists in our garage");
            }

            return vehicleRequested;
        }
    }
}