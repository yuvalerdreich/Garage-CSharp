using System;
using System.Collections.Generic;
using System.IO;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private readonly GarageManager r_Garage = new GarageManager();

        private void showUserOptions()
        {
            Console.WriteLine(@"
Garage Menu Options:
1. Load sysytem with vehicles data.
2. Insert a new vehicle.
3. Show garage vehicles filtered by licence plate number.
4. Change vehicle state.
5. Inflate tires air pressure to maximum.
6. Fuel vehicle.
7. Charge vehicle.
8. Show full vehicle data by licence plate.
9. Quit");
        }

        public void RunSystem()
        {
            bool isUserActive = true;
            eUserMenuOption userOption;

            while (isUserActive)
            {
                showUserOptions();
                try
                {
                    userOption = (eUserMenuOption)getUserSelectionOption(1, 9);
                    switch (userOption)
                    {
                        case eUserMenuOption.LoadVehicleData:
                            this.loadVehiclesDataFromFile();
                            break;
                        case eUserMenuOption.InsertNewVehicle:
                            this.insertNewVehicle();
                            break;
                        case eUserMenuOption.ShowVehicleLicencePlateListByStatus:
                            this.showlicencePlateList();
                            break;
                        case eUserMenuOption.ChangeVehicleStatus:
                            this.changeVehicleStatus();
                            break;
                        case eUserMenuOption.InflateWheelsToMax:
                            this.inflateWheelsToMax();
                            break;
                        case eUserMenuOption.FuelVehicle:
                            this.fuelVehicle();
                            break;
                        case eUserMenuOption.RechargeVehicle:
                            this.chargeVehicle();
                            break;
                        case eUserMenuOption.ShowFullVehicleDetailsByLicencePlate:
                            this.showFullVehicleDetails();
                            break;
                        case eUserMenuOption.Quit:
                            isUserActive = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                if (isUserActive)
                {
                    Console.WriteLine();
                    Console.WriteLine("Enter your next choice");
                }
            }
        }

        private void loadVehiclesDataFromFile()
        {
            string[] fileRecords = File.ReadAllLines("Vehicles.db");
            Vehicle vehicleToAdd;

            foreach (string record in fileRecords)
            {
                string[] recordFields = record.Split(',');
                vehicleToAdd = createVehicleByRecordFields(recordFields);
                r_Garage.AddNewVehicleToGarage(vehicleToAdd, vehicleToAdd.LicencePlate);
            }
        }

        private Vehicle createVehicleByRecordFields(string[] i_RecordFields)
        {
            Vehicle newVehicle = null;
            List<float> vehicleWheelsAirPressure = new List<float>();
            List<VehicleSpecificField> VehicleSpecificFields;
            float currAirPressure;
            int fieldIdx;

            try
            {
                if (validateVehicleType(i_RecordFields[0].Trim()))
                {
                    newVehicle = VehicleCreator.CreateVehicle(i_RecordFields[0].Trim(), i_RecordFields[1], i_RecordFields[2]);
                    newVehicle.SetCurrEnergyWithPrecentage(float.Parse(i_RecordFields[3]));
                    currAirPressure = float.Parse(i_RecordFields[5]);
                    autoFillWheelsAirPressure(vehicleWheelsAirPressure, currAirPressure, newVehicle.NumberOfWheels);
                    newVehicle.SetWheelsOfVehicle(i_RecordFields[4], newVehicle.MaxAirPressure, vehicleWheelsAirPressure);
                    newVehicle.SetVehicleOwner(i_RecordFields[6], i_RecordFields[7]);
                    newVehicle.VehicleStatus = eVehicleStatus.InRepair;
                    VehicleSpecificFields = newVehicle.GetSpecificFields();
                    fieldIdx = 8;
                    foreach (VehicleSpecificField field in VehicleSpecificFields)
                    {
                        newVehicle.SetSpecificFields(field.FieldName, i_RecordFields[fieldIdx]);
                        fieldIdx++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("File format is not valid");
            }

            return newVehicle;
        }

        private void insertNewVehicle()
        {
            string licencePlate, modelName;
            string vehicleType;
            Vehicle userVehicleToInsert;
            bool vehicleAdded;

            licencePlate = getStrFieldFromUser("licence plate");
            if (r_Garage.CheckIfVehicleExistsInGarage(licencePlate))
            {
                Console.WriteLine("Vehicle already exists in the Garage, in repair.");
                r_Garage.UpdateVehicleStatus(licencePlate, eVehicleStatus.InRepair);
            }
            else
            {
                vehicleAdded = false;
                while (!vehicleAdded)
                {
                    try
                    {
                        vehicleType = getVehicleType();
                        modelName = getStrFieldFromUser("model name");
                        userVehicleToInsert = VehicleCreator.CreateVehicle(vehicleType, licencePlate, modelName);
                        initializeVehicleGeneralAttributes(userVehicleToInsert);
                        initializeVehicleSpecificAttributes(userVehicleToInsert);
                        r_Garage.AddNewVehicleToGarage(userVehicleToInsert, licencePlate);
                        vehicleAdded = true;
                    }
                    catch (ValueRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
        }

        private void showlicencePlateList()
        {
            int userOption;
            List<string> licencePlateList;
            eVehicleStatus vehicleStatusToFilterBy;

            Console.WriteLine("Filter list by:");
            showEnumOptions<eVehicleStatus>();
            Console.WriteLine("4- None");
            userOption = getUserSelectionOption(1, 4);
            if (userOption == 4)
            {
                licencePlateList = r_Garage.GetLicencePlateList();
                printListToUser(licencePlateList);
            }
            else
            {
                vehicleStatusToFilterBy = (eVehicleStatus)userOption;
                licencePlateList = r_Garage.GarageVehicleListFilteredByStatus(vehicleStatusToFilterBy);
                printListToUser(licencePlateList);
            }
        }

        private void printListToUser(List<string> i_StrList)
        {
            foreach (string str in i_StrList)
            {
                Console.WriteLine(str);
            }
        }

        private void changeVehicleStatus()
        {
            string licencePlate = getStrFieldFromUser("licence plate number");
            eVehicleStatus requestedVehicleStatus = getEnumOptionFromUser<eVehicleStatus>(1, 3);
            r_Garage.UpdateVehicleStatus(licencePlate, requestedVehicleStatus);
        }

        private void inflateWheelsToMax()
        {
            string licencePlate = getStrFieldFromUser("licence plate number");
            r_Garage.InflateVehiclesTiresToMax(licencePlate);
        }

        private void fuelVehicle()
        {
            string licencePlate = getStrFieldFromUser("licence plate number");
            eFuelType fuelType = getEnumOptionFromUser<eFuelType>(1, 4);
            float amountOfFuelToFill = getAmountOfEnergyToFill();
            r_Garage.PumpFuelToVehicle(licencePlate, fuelType, amountOfFuelToFill);
        }

        private void chargeVehicle()
        {
            string licencePlate = getStrFieldFromUser("licence plate number");
            float amountOfHoursToCharge = getAmountOfEnergyToFill();
            r_Garage.ChargeVehicle(licencePlate, amountOfHoursToCharge);
        }

        private void showFullVehicleDetails()
        {
            string licencePlate = getStrFieldFromUser("licence plate number");
            Console.WriteLine(@"
Your vehicle details:
{0}", r_Garage.ProvideFullVehicleDetails(licencePlate));
        }

        private float getAmountOfEnergyToFill()
        {
            string userInput;
            float amountToFill;

            while (true)
            {
                Console.WriteLine("Please enter the the anount of energy you wish to fill(hours if electric and liters if powers by fuel)");
                userInput = Console.ReadLine();
                try
                {
                    if (!float.TryParse(userInput, out amountToFill))
                    {
                        throw new FormatException("The enrgy amount must be a number");
                    }
                    else
                    {
                        break;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return amountToFill;
        }

        private void initializeVehicleGeneralAttributes(Vehicle i_Vehicle)
        {
            string wheelProducerName, ownerName, ownerPhoneNumber;
            List<float> wheelsCurrentAirPressure;
            float enrgyAmountLeft;

            ownerName = getStrFieldFromUser("owner name");
            ownerPhoneNumber = getStrFieldFromUser("OwnerPhoneNumber");
            i_Vehicle.SetVehicleOwner(ownerName, ownerPhoneNumber);
            enrgyAmountLeft = getCurrEnergyAmountFromUser();
            i_Vehicle.SetRemainingEnergyPrecentageWithCurrPrecentage(enrgyAmountLeft);
            wheelProducerName = getStrFieldFromUser("wheel's producer name");
            wheelsCurrentAirPressure = getCurrWheelsAirPressureFromUser(i_Vehicle);
            i_Vehicle.SetWheelsOfVehicle(wheelProducerName, i_Vehicle.MaxAirPressure, wheelsCurrentAirPressure); 
            i_Vehicle.VehicleStatus = eVehicleStatus.InRepair;
        }

        private void initializeVehicleSpecificAttributes(Vehicle i_Vehicle)
        {
            string userInput;

            foreach (VehicleSpecificField field in i_Vehicle.GetSpecificFields())
            {
                Console.WriteLine("Please enter a value for {0}.", field.FieldName);
                userInput = Console.ReadLine();
                i_Vehicle.SetSpecificFields(field.FieldName, userInput);
            }
        }
       
        private string getVehicleType()
        {
            string userInput;

            Console.WriteLine("Please enter you vehicle type. It should be one of the following options:");
            showPossibleVehicleTypes();
            userInput = Console.ReadLine();
            validateVehicleType(userInput.Trim());

            return userInput;
        }

        private string getStrFieldFromUser(string i_RequestedField)
        {
            string userInput;

            Console.WriteLine("Please enter the vehicle's {0}", i_RequestedField);
            userInput = Console.ReadLine();

            return userInput;
        }

        private float getCurrEnergyAmountFromUser()
        {
            string enrgyLeftStr;
            float currentEnergy;

            Console.WriteLine("Enter vehicle's current energy state.");
            enrgyLeftStr = Console.ReadLine();
            if (!float.TryParse(enrgyLeftStr, out currentEnergy))
            {
                throw new FormatException("Invalid input. The input must be a number.");
            }

            return currentEnergy;
        }

        private List<float> getCurrWheelsAirPressureFromUser(Vehicle i_Vehicle)
        {
            List<float> wheelsAirPressure = new List<float>();
            int numOfWheels = i_Vehicle.NumberOfWheels;
            float maxAirPressure = i_Vehicle.MaxAirPressure;
            float currWheelAirPressure;
            int userOption;

            Console.WriteLine(@"
Possible options:
1- All wheels have the same air pressure state.
2- Other.");
            userOption = getUserSelectionOption(1, 2);
            if (userOption == 1)
            {
                currWheelAirPressure = validateUserAirPressureInput(maxAirPressure);
                autoFillWheelsAirPressure(wheelsAirPressure, currWheelAirPressure, numOfWheels);
            }
            else
            {
                for (int i = 0; i < numOfWheels; i++)
                {
                    currWheelAirPressure = validateUserAirPressureInput(maxAirPressure);
                    wheelsAirPressure.Add(currWheelAirPressure);
                }
            }

            return wheelsAirPressure;
        }

        private void autoFillWheelsAirPressure(List<float> i_WheelsAirPressure, float i_CurrAirPressure, int i_NumOfWheels)
        {
            for (int i = 0; i < i_NumOfWheels; i++)
            {
                i_WheelsAirPressure.Add(i_CurrAirPressure);
            }
        }

        private bool validateVehicleType(string i_VehicleType)
        {
            bool isValid = true;

            if (!VehicleCreator.SupportedTypes.Contains(i_VehicleType))
            {
                throw new FormatException("Invalid vehicle type.");
            }

            return isValid;
        }

        private void showPossibleVehicleTypes()
        {
            int optionIdx = 1;

            foreach (string type in VehicleCreator.SupportedTypes)
            {
                Console.WriteLine("{0}- {1}", optionIdx, type);
                optionIdx++;
            }
        }

        private float validateUserAirPressureInput(float i_MaxAirPressure)
        {
            string userInput;
            float wheelAirPressure;

            Console.WriteLine("Please enter wheel current air pressure in range 0 to {0}.", i_MaxAirPressure);
            userInput = Console.ReadLine();
            if (!float.TryParse(userInput, out wheelAirPressure))
            {
                throw new FormatException("Your curr pressure input must be a number.");
            }

            return wheelAirPressure;
        }

        private int getUserSelectionOption(int i_LowerBound, int i_UpperBoound)
        {
            bool isValid;
            int selectedOption;
            string userInput;

            while (true)
            {
                Console.WriteLine("Please enter you prefered option between {0} and {1}", i_LowerBound, i_UpperBoound);
                userInput = Console.ReadLine();
                try
                {
                    isValid = validateInputSelection(userInput, i_LowerBound, i_UpperBoound, out selectedOption);
                    break;
                }
                catch (ValueRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return selectedOption;
        }

        private bool validateInputSelection(string i_UserInput, int i_LowerBound, int i_UpperBoound, out int o_SelectedOption)
        {
            bool isValid = true;

            if (int.TryParse(i_UserInput, out o_SelectedOption))
            {
                if (o_SelectedOption < i_LowerBound || o_SelectedOption > i_UpperBoound)
                {
                    throw new ValueRangeException("Selection option out of range.", i_UpperBoound, i_LowerBound);
                }
            }
            else
            {
                throw new FormatException("Your option input must be a number.");
            }

            return isValid;
        }

        private T getEnumOptionFromUser<T>(int i_LowerBound, int i_UpperBound)
        {
            showEnumOptions<T>();
            int userOption = getUserSelectionOption(i_LowerBound, i_UpperBound);
            return (T)Enum.ToObject(typeof(T), userOption);
        }

        public void showEnumOptions<T>()
        {
            int optionIdx = 1;

            foreach (T value in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine("{0}- {1}", optionIdx, value);
                optionIdx++;
            }
        }
    }
}