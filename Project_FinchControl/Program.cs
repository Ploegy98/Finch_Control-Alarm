﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinchAPI;

namespace Project_FinchControl
{
    public enum Command
    {
        NONE,
        MOVEFORWARD,
        MOVEBACKWARD,
        STOPMOTORS,
        WAIT,
        TURNRIGHT,
        TURNLEFT,
        LEDON,
        LEDOFF,
        GETTEMPERATURE,
        GETLIGHTLEVELRIGHT,
        GETLIGHTLEVELLEFT,
        GETLIGHTLEVELAVERAGE,
        NOTEON,
        NOTEOFF,
        DONE
    }

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Revised solution of finch project, original project had mutliple syntax errors
    // Application Type: Console
    // Author: Vanderploeg, Casey
    // Dated Created: 6/10/2020
    // Last Modified: 6/22/2020
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            DisplayLoginRegister();

            //
            // Call the application menu
            //

            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }
        /// <summary>
        /// *****************************
        /// *   Login/Register Screen   *
        /// *****************************
        /// </summary>
        static void DisplayLoginRegister()
        {
            DisplayScreenHeader("Login and/or Register Menu");
            Console.WriteLine();
            Console.WriteLine("Have you registered with the program previously? ( yes | no )");
            if (Console.ReadLine().ToLower() == "yes")
            {
                DisplayLogin();
            }
            else
            {
                DisplayRegisterUser();
                DisplayLogin();
            }
        }
        static void DisplayLogin()
        {
            string userName;
            string password;
            bool validLogin;

            do
            {
                DisplayScreenHeader("Login");

                Console.WriteLine();
                Console.Write("\tEnter your user-name:");
                userName = Console.ReadLine();
                Console.WriteLine("\tEnter your password: ");
                password = Console.ReadLine();

                validLogin = IsValidLoginInfo(userName, password);

                Console.WriteLine();
                if (validLogin)
                {
                    Console.WriteLine("\tYou are now logged in.");
                }
                else
                {
                    Console.WriteLine("\tIt appears either the user-name or password is incorrect.");
                    Console.WriteLine("\tPlease try again.");
                }

                DisplayContinuePrompt();
            } while (!validLogin); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        static bool IsValidLoginInfo(string userName, string password)
        {
            List<(string userName, string password)> registerUserLoginInfo = new List<(string userName, string password)>();

            bool validUser = false;

            registerUserLoginInfo = ReadLoginInfoData();

            //
            // loop through the list of registered users login tuples and check each one against the login info
            //
            
            foreach((string userName, string password) userLoginInfo in registerUserLoginInfo)
            {
                if ((userLoginInfo.userName == userName) && (userLoginInfo.password == password))
                {
                    validUser = true;
                    break;
                }
            }
            return validUser;
        }

        static void DisplayRegisterUser()
        {
            string userName;
            string password;

            DisplayScreenHeader("Register Screen");

            Console.WriteLine();
            Console.Write("Please enter a new user-name for your program credentials: ");
            userName = Console.ReadLine();
            Console.Write("Please enter a new password for your program credentials: ");
            password = Console.ReadLine();
            Console.WriteLine();
            WriteLoginInfoData(userName, password);

            Console.WriteLine("User-name and password credentials will now be saved to the program");
            Console.WriteLine();
            Console.WriteLine($"\tGiven Username: {userName}");
            Console.WriteLine();
            Console.WriteLine($"\tGiven Password: {password}");

            DisplayContinuePrompt();
        }

        static void WriteLoginInfoData(string userName, string password)
        {
            string dataPath = @"Data/Logins.txt";
            string loginInfoText;

            loginInfoText = userName + "," + password;

            File.WriteAllText(dataPath, loginInfoText);
        }

        static List<(string userName, string password)> ReadLoginInfoData()
        {
            string dataPath = @"Data/Logins.txt";

            string[] loginInfoArray;
            (string userName, string password) loginInfoTuple;

            List<(string userName, string password)> registeredUserLoginInfo = new List<(string userName, string password)>();

            loginInfoArray = File.ReadAllLines(dataPath);

            //
            // loop through the array
            // split the user-name and password into a tuple
            // add the tuple to the list
            //
            foreach (string loginInfoText in loginInfoArray)
            {
                //
                // use the split method to separate the user-name and password into an array
                //
                loginInfoArray = loginInfoText.Split(',');

                loginInfoTuple.userName = loginInfoArray[0];
                loginInfoTuple.password = loginInfoArray[1];

                registeredUserLoginInfo.Add(loginInfoTuple);

            }


            return registeredUserLoginInfo;
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.White;
        }

 /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        DisplayTalentShowMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        LightAlarmDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void DisplayTalentShowMenuScreen(Finch myFinch)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Light and Sound");
                Console.WriteLine("\tb) Dance ");
                Console.WriteLine("\tc) Mixing it up ");
                Console.WriteLine("\td) Movement");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayLightAndSound(myFinch);

                        break;

                    case "b":
                        DisplayDance(myFinch);
                        break;

                    case "c":
                        DisplayMixing(myFinch);
                        break;

                    case "d":
                        DisplayMovement(myFinch);
                        break;

                    case "q":
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        static void DisplayDance(Finch myFinch)
        {
            //
            // Finch robot got the funk
            //

            DisplayScreenHeader("Finch's Dance");

            Console.WriteLine("\tThe Finch robot will now dance!");
            DisplayContinuePrompt();

            myFinch.setMotors(200, 00);  //adjust 1
            myFinch.wait(400);
            myFinch.setMotors(0, 0);     //stop 1
            myFinch.wait(400);
            myFinch.setMotors(250, 250); //move 1
            myFinch.wait(400);
            myFinch.setMotors(0, 0);
            myFinch.wait(400);

            myFinch.setMotors(0, 200);  //adjust 2
            myFinch.wait(400);
            myFinch.setMotors(0, 0);     //stop 2
            myFinch.wait(400);
            myFinch.setMotors(-250, -250); //move 2
            myFinch.wait(400);
            myFinch.setMotors(0, 0);
            myFinch.wait(400);

            myFinch.setMotors(200, 00);  //adjust 3
            myFinch.wait(400);
            myFinch.setMotors(0, 0);     //stop 3
            myFinch.wait(400);
            myFinch.setMotors(250, 250); //move 3
            myFinch.wait(400);
            myFinch.setMotors(0, 0);
            myFinch.wait(400);

            myFinch.setMotors(0, 200);  //adjust 4
            myFinch.wait(400);
            myFinch.setMotors(0, 0);     //stop 4
            myFinch.wait(400);
            myFinch.setMotors(-250, -250); //move 4
            myFinch.wait(400);
            myFinch.setMotors(0, 0);
            myFinch.wait(400);

            myFinch.setMotors(200, 00);  //adjust 5
            myFinch.wait(400);
            myFinch.setMotors(0, 0);     //stop 5
            myFinch.wait(400);
            myFinch.setMotors(250, 250); //move 5
            myFinch.wait(400);
            myFinch.setMotors(0, 0);
            myFinch.wait(400);

            DisplayMenuPrompt("Talent show!");
        }

        static void DisplayMixing(Finch myFinch)
        {
            //
            // Display of Finch's functions in mixing
            //
            Console.CursorVisible = false;

            DisplayScreenHeader("Mixing It Up");

            Console.WriteLine("\tThe Finch robot will show off it's moves!");
            DisplayContinuePrompt();
            for (int looptwo = 0; looptwo < 8; looptwo++)
            {
                myFinch.setMotors(150, 150);    // move
                myFinch.wait(200);
                myFinch.setMotors(0, 0);        // stop
                myFinch.setLED(100, 100, 100);  // turn on LEDs
                myFinch.noteOn(450);            // turn on note
                myFinch.wait(200);
                myFinch.noteOff();              // turn off note
                myFinch.setLED(0, 0, 0);        // turn off LEDs
            }


            DisplayMenuPrompt("Talent Show!");

        }

        static void DisplayMovement(Finch myFinch)
        {
            //
            // Display of Finch robot's movement
            //

            DisplayScreenHeader("Finch Movement");

            myFinch.setMotors(350, 350);   // move
            myFinch.wait(450);
            myFinch.setMotors(0, 0);       // stop
            myFinch.setMotors(-350, -350); // reverse
            myFinch.wait(450);
            myFinch.setMotors(0, 0);
            myFinch.setMotors(300, 0);
            myFinch.wait(400);
            myFinch.setMotors(0, 0);
            myFinch.setMotors(0, 300);
            myFinch.wait(400);
            myFinch.setMotors(0, 0);

            DisplayMenuPrompt("Talent Show");
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will not show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
                finchRobot.wait(200);
            }



            DisplayContinuePrompt();
            DisplayMenuPrompt("Talent Show Menu");

        }

        #endregion

        #region DATA RECORDER

        static void DataRecorderDisplayMenuScreen(Finch finchRobot)
        {
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0;
            double[] temperatures = null;
            double[] fahrenheitTemperatures = null;
            double[] lightRightLevel = null;
            double[] lightLeftLevel = null;
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Number of Data Points");
                Console.WriteLine("\tb) Frequency of Data Points");
                Console.WriteLine("\tc) Get Data");
                Console.WriteLine("\td) Show Data ");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "b":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequency();
                        break;

                    case "c":
                        temperatures = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, finchRobot);
                        fahrenheitTemperatures = DataRecorderGetFahrenheit(numberOfDataPoints, dataPointFrequency, finchRobot);
                        lightRightLevel = DataRecorderGetDataRightLight(numberOfDataPoints, dataPointFrequency, finchRobot);
                        lightLeftLevel = DataRecorderGetDataLeftLight(numberOfDataPoints, dataPointFrequency, finchRobot);
                        break;

                    case "d":
                        DataRecorderDisplayGetData(temperatures);
                        DataRecorderDisplayGetDataFahrenheit(fahrenheitTemperatures);
                        DataRecorderDisplayGetDataRightLights(lightRightLevel);
                        DataRecorderDisplayGetDataLeftLights(lightLeftLevel);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitMenu);
        }
        //
        // Right lights for data
        //

        static double[] DataRecorderGetDataRightLight(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] lightRightLevel = new double[numberOfDataPoints];
            DisplayScreenHeader("Get Data");
            Console.WriteLine($"Amount of data points provided: {numberOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine($"Data Point Frequency given: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record data.");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                lightRightLevel[index] = finchRobot.getRightLightSensor();
                Console.WriteLine($"\tReading {index + 1}: {lightRightLevel[index].ToString("n1")}" + "Right light level");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);
            }
            DisplayContinuePrompt();
            return lightRightLevel;
        }
        static double[] DataRecorderGetDataLeftLight(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] lightLeftLevel = new double[numberOfDataPoints];
            DisplayScreenHeader("Get Data");
            Console.WriteLine($"Amount of data points provided: {numberOfDataPoints}");
            Console.WriteLine();
            Console.WriteLine($"Data Point Frequency given: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("The Finch Robot is ready to record data.");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                lightLeftLevel[index] = finchRobot.getRightLightSensor();
                Console.WriteLine($"\tReading {index + 1}: {lightLeftLevel[index].ToString("n1")}" + "Left light level");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);
            }
            DisplayContinuePrompt();
            return lightLeftLevel;
        }
        //
        // Right light display
        //
        static void DataRecorderDisplayGetDataRightLights(double[] lightRightLevel)
        {
            DisplayScreenHeader("Display Data");
            DataRecorderDisplayTableRight(lightRightLevel);
            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayTableRight(double[] lightRightLevel)
        {
            DisplayScreenHeader("Show Data");

            //
            // Display table headers
            //
            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temp".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                 );

            //
            // display table data
            //
            for (int index = 0; index < lightRightLevel.Length; index++)
            {
                Console.WriteLine(
                  (index + 1).ToString().PadLeft(15) +
                  lightRightLevel[index].ToString("n2").PadLeft(15) + "light right level"
                  );
            }
        }
        //
        // Left light display
        //
        static void DataRecorderDisplayGetDataLeftLights(double[] lightLeftLevel)
        {
            DisplayScreenHeader("Display Data");
            DataRecorderDisplayTableLeft(lightLeftLevel);
            DisplayContinuePrompt();
        }

        static void DataRecorderDisplayTableLeft(double[] lightLeftLevel)
        {
            DisplayScreenHeader("Show Data");

            //
            // Display table headers
            //
            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temp".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                 );

            //
            // display table data
            //
            for (int index = 0; index < lightLeftLevel.Length; index++)
            {
                Console.WriteLine(
                  (index + 1).ToString().PadLeft(15) +
                  lightLeftLevel[index].ToString("n2").PadLeft(15) + "light left level"
                  );
            }
        }
        //
        // Celcius
        //
        static void DataRecorderDisplayGetData(double[] temperatures)
        {
            DisplayScreenHeader("Display Celcius Data");
            DataRecorderTableCelcius(temperatures);
            DisplayContinuePrompt();
        }

        static void DataRecorderTableCelcius(double[] temperatures)
        {
            DisplayScreenHeader("Show Celcius Data");

            //
            // Display table headers
            //
            Console.WriteLine(
                "Recording #".PadLeft(15) +
                "Temp".PadLeft(15)
                );
            Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                 );

            //
            // display table data
            //
            for (int index = 0; index < temperatures.Length; index++)
            {
                Console.WriteLine(
                  (index + 1).ToString().PadLeft(15) +
                  temperatures[index].ToString("n2").PadLeft(15) + " °C"
                   );
            }

            DisplayContinuePrompt();
        }
        //
        // Fahrenheit
        //
        static void DataRecorderDisplayGetDataFahrenheit(double[] fahrenheitTemperatures)
        {
            DisplayScreenHeader("Display Fahrenheit Data");
            DataRecorderTableFahrenheit(fahrenheitTemperatures);
            DisplayContinuePrompt();
        }
        static void DataRecorderTableFahrenheit(double[] fahrenheitTemperatures)
        {
            {
                DisplayScreenHeader("Show Fahrenheit Data");

                //
                // display table headers
                //
                Console.WriteLine(
                    "Recording #".PadLeft(15) +
                    "Temp".PadLeft(15)
                    );
                Console.WriteLine(
                "-----------".PadLeft(15) +
                "-----------".PadLeft(15)
                 );

                //
                // display table date
                //
                for (int index = 0; index < fahrenheitTemperatures.Length; index++)
                {
                    Console.WriteLine(
                      (index + 1).ToString().PadLeft(15) +
                      fahrenheitTemperatures[index].ToString("n2").PadLeft(15) + " °F"
                        );
                }
            }
        }
        //
        // celcius temperatures
        //
        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] temperatures = new double[numberOfDataPoints];

            DisplayScreenHeader("Get Celcius Data");

            Console.WriteLine($"\tNumber of data points: {numberOfDataPoints}");
            Console.WriteLine($"\tData Point Frequency: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("\tThe Finch Robot is ready to begin recording the temperature data.");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = finchRobot.getTemperature();
                Console.WriteLine($"\tReading {index + 1}: {temperatures[index].ToString("n2")}°C");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);

                Console.WriteLine("Data Recording is complete");
            }

            DisplayContinuePrompt();
            DisplayScreenHeader("Get Data");

            Console.WriteLine();
            Console.WriteLine("\t Table of Celcius Temperatures");
            Console.WriteLine();
            DataRecorderTableCelcius(temperatures);

            DisplayContinuePrompt();

            return temperatures;
        }
        //
        // fahrenheit temperatures
        //
        static double[] DataRecorderGetFahrenheit(int numberOfDataPoints, double dataPointFrequency, Finch finchRobot)
        {
            double[] fahrenheitTemperatures = new double[numberOfDataPoints];

            DisplayScreenHeader("Get Fahrenheit Data");

            Console.WriteLine($"\t Number of data points; {numberOfDataPoints}");
            Console.WriteLine($"\tData Point Frequency: {dataPointFrequency}");
            Console.WriteLine();
            Console.WriteLine("\tThe Finch Robot is ready to begin recording the temperature data.");
            DisplayContinuePrompt();

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                double inputTemp;
                double outputTemp;
                inputTemp = Convert.ToDouble(finchRobot.getTemperature());
                outputTemp = (inputTemp * 1.8) + 32;
                fahrenheitTemperatures[index] = outputTemp;
                Console.WriteLine($"\tReading {index + 1}: {fahrenheitTemperatures[index].ToString("n2")}°F");
                int waitInSeconds = (int)(dataPointFrequency * 1000);
                finchRobot.wait(waitInSeconds);
            }

            DisplayContinuePrompt();
            DisplayScreenHeader("Get Data");

            Console.WriteLine();
            Console.WriteLine("\t Table of Fahrenheit Temperatures");
            Console.WriteLine();
            DataRecorderTableFahrenheit(fahrenheitTemperatures);

            DisplayContinuePrompt();

            return fahrenheitTemperatures;
        }

        /// <summary>
        /// Get the frequency of data points
        /// </summary>
        /// <returns>frequency of data points</returns>
        static double DataRecorderDisplayGetDataPointFrequency()
        {
            double dataPointFrequency;
            string userResponse;
            //
            // Validate
            //

            do
            {
                DisplayScreenHeader("Data Point Frequency");

                Console.Write("\tFrequency of data points: ");

                Console.Write("Please enter the frequency of data points in seconds, between 0 and 100.");
                Console.Write("Enter number here: ");
                userResponse = Console.ReadLine();
                double.TryParse(userResponse, out dataPointFrequency);
                if (dataPointFrequency >= 0 && dataPointFrequency <= 99)
                {
                    Console.WriteLine("Frequency of data points:" + dataPointFrequency + " seconds.");
                    DisplayContinuePrompt();
                    return dataPointFrequency;
                }
                else
                {
                    Console.WriteLine("You've entered a non-valid value, please enter a value ranging from 0 - 99: ");
                    userResponse = Console.ReadLine();
                    double.TryParse(userResponse, out dataPointFrequency);
                    DisplayContinuePrompt();
                }
            }
            while (dataPointFrequency >= 0 && dataPointFrequency <= 99);
            double.TryParse(userResponse, out dataPointFrequency);
            return dataPointFrequency;
        }

        /// <summary>
        /// get the number of data points from user
        /// </summary>
        /// <returns>number of data points</returns>
        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;
            string userResponse;
            //
            // validate
            //
            do
            {
                DisplayScreenHeader("Number of Data Points");

                Console.Write("\tNumber of data points: ");
                Console.Write("Enter a number between 0 and 100: ");
                userResponse = Console.ReadLine();
                int.TryParse(userResponse, out numberOfDataPoints);
                if (numberOfDataPoints >= 0 && numberOfDataPoints <= 99)
                {
                    Console.WriteLine("Valid value: " + numberOfDataPoints);
                    DisplayContinuePrompt();
                    return numberOfDataPoints;
                }
                else
                {
                    Console.WriteLine("Invalid value, please use a number between 0 and 100: ");
                    userResponse = Console.ReadLine();
                    int.TryParse(userResponse, out numberOfDataPoints);
                    DisplayContinuePrompt();
                }
            }
            while (numberOfDataPoints >= 0 && numberOfDataPoints <= 99);
            int.TryParse(userResponse, out numberOfDataPoints);
            return numberOfDataPoints;
        }

        #endregion

        #region Alarm System
        static void LightAlarmDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitMenu = false;
            string menuChoice;


            string sensorsToMonitor = "";
            string rangeType = "";
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Light Alarm Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Set Sensors to Monitor");
                Console.WriteLine("\tb) Set Range Type");
                Console.WriteLine("\tc) Set Minimum/Maximum Threshold Value");
                Console.WriteLine("\td) Set Time to Monitor");
                Console.WriteLine("\te Set Alarm");
                Console.WriteLine("\tq) Main Menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = LightAlarmDisplaySetSensorsToMonitor();
                        break;

                    case "b":
                        rangeType = LightAlarmDisplaySetRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = LightAlarmDisplaySetMinimumMaximumThresholdValue(rangeType, finchRobot);
                        break;

                    case "d":
                        timeToMonitor = LightAlarmDisplaySetTimeToMonitor();
                        break;

                    case "e":
                        LightAlarmSetAlarm(finchRobot, sensorsToMonitor, rangeType, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitMenu);
        }
        static void LightAlarmSetAlarm(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            bool thresholdExceeded = false;
            int secElapsed = 0;
            int lightValueNow = 0;

            DisplayScreenHeader("Set Alarm");

            Console.WriteLine($"\tSensor(s) to Monitor: {sensorsToMonitor}");
            Console.WriteLine($"\tRange Type: {rangeType}");
            Console.WriteLine($"\t{rangeType} Threshold Value: {minMaxThresholdValue}");
            Console.WriteLine($"\tTime to Monitor: {timeToMonitor}");
            Console.WriteLine();

            Console.WriteLine("Press any key to begin monitoring.");
            Console.ReadKey();

            while((secElapsed < timeToMonitor) && !thresholdExceeded)
            {
                switch (sensorsToMonitor)
                {
                    case "left":
                        lightValueNow = finchRobot.getLeftLightSensor();
                        break;

                    case "right":
                        lightValueNow = finchRobot.getRightLightSensor();
                        break;

                    case "both":
                        lightValueNow = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                        break;

                }
                switch (rangeType)
                {
                    case "Maximum":
                        if (lightValueNow > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "Minimum":
                        if (lightValueNow < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }

                finchRobot.wait(700);
                secElapsed++;
            }
            if (thresholdExceeded)
            {
                Console.WriteLine($"The {rangeType} threshold value: {minMaxThresholdValue} was exceeded by the light sensor value of {lightValueNow}.");
                finchRobot.noteOn(1000);
                finchRobot.wait(2000);
                finchRobot.noteOff();
            }
            else
            {
                Console.WriteLine($"The {rangeType} threshold value of {minMaxThresholdValue} was not exceeded.");
            }

            DisplayMenuPrompt("Light Alarm");
        }

        static int LightAlarmDisplaySetMinimumMaximumThresholdValue(string rangeType, Finch finchRobot)
        {
            int minMaxThresholdValue;
            bool validResponse;
            string userResponse;

            do
            {
                DisplayScreenHeader("Min/Max Threshold Value");

                Console.WriteLine($"Left Light Sensor: {finchRobot.getLeftLightSensor()}");
                Console.WriteLine($"Current Right Light Sensor: {finchRobot.getRightLightSensor()}");
                Console.WriteLine();

                Console.WriteLine($"{rangeType} Light Sensor Value: ");
                userResponse = Console.ReadLine();
                int.TryParse(userResponse, out minMaxThresholdValue);

                validResponse = int.TryParse(userResponse, out minMaxThresholdValue);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a number.");
                    DisplayContinuePrompt();
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);

            Console.WriteLine($"The {rangeType} threshold is: {minMaxThresholdValue}.");

            DisplayMenuPrompt("Light Alarm");

            return minMaxThresholdValue;
        }

        static int LightAlarmDisplaySetTimeToMonitor()
        {
            int timeToMonitor;
            bool validResponse;
            string userResponse;

            do
            {
                DisplayScreenHeader("Time to Monitor");

                Console.Write("Time to Monitor [seconds]:");
                userResponse = Console.ReadLine();

                int.TryParse(userResponse, out timeToMonitor);

                validResponse = int.TryParse(userResponse, out timeToMonitor);

                if (!validResponse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a number.");
                    DisplayContinuePrompt();
                }
                else
                {
                    validResponse = true;
                }

            } while (!validResponse);

            Console.WriteLine($"Light will be monitored for {timeToMonitor} seconds.");

            DisplayMenuPrompt("Light Alarm");

            return timeToMonitor;
        }

        static string LightAlarmDisplaySetRangeType()
        {
            string rangeType;
            string userResponse;
            bool validResponse;
            bool minimumRes = false;

            do
            {
                validResponse = true;

                DisplayScreenHeader("Range Type");

                Console.Write("Range Type: [Minimum, Maximum]");
                userResponse = Console.ReadLine();

                if (userResponse == "Minimum")
                {
                    minimumRes = true;
                }
                else if (userResponse == "Maximum")
                {
                    minimumRes = false;
                }
                else
                {
                    Console.WriteLine("Please enter either minimum or maximum.");
                    DisplayContinuePrompt();
                    validResponse = false;
                }

            } while (!validResponse);

            if (minimumRes == true)
            {
                rangeType = "Minimum";
            }
            else
            {
                rangeType = "Maximum";
            }

            Console.WriteLine($"Range type is {rangeType}.");

            DisplayMenuPrompt("Light alarm");

            return rangeType;
        }

        static string LightAlarmDisplaySetSensorsToMonitor()
        {
            string sensorsToMonitor = "";
            string userResponse;
            bool validResponse;

            do
            {
                validResponse = true;
                DisplayScreenHeader("Sensors to Monitor");

                Console.Write("Sensors to Monitor: [right, left, both]");
                userResponse = Console.ReadLine().ToLower();

                if (userResponse == "left")
                {
                    sensorsToMonitor = "left";
                }
                else if (userResponse == "right")
                {
                    sensorsToMonitor = "right";
                }
                else if (userResponse == "both")
                {
                    sensorsToMonitor = "both";
                }
                else
                {
                    Console.WriteLine("Please enter a valid response: right, left, or both for the sensors.");
                    Console.WriteLine();
                    DisplayContinuePrompt();
                    validResponse = false;
                }
            } while (!validResponse);

            Console.WriteLine($"{sensorsToMonitor} will be monitored");

            DisplayMenuPrompt("Light Alarm");

            return sensorsToMonitor;
        }

        #endregion

        #region USER PROGRAMMING

        /// *****************************
        /// *   User Programming Menu   *
        /// *****************************

        static void UserProgrammingDisplayMenuScreen (Finch finchRobot)
        {
            string menuChoice;
            bool quitMenu = false;

            //
            // tuple to store all three command parameters
            //
            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            List<Command> commands = new List<Command>();

            do
            {
                DisplayScreenHeader("User Programming Menu");

                //
                // Get user menu choice
                //
                Console.WriteLine("\ta) Set Command Parameters");
                Console.WriteLine("\tb) Add Commands");
                Console.WriteLine("\tc) View Commands");
                Console.WriteLine("\td) Execute Commands");
                Console.WriteLine("\tq) Quit to Menu");
                Console.WriteLine("\t\t Enter Choice");
                menuChoice = Console.ReadLine().ToLower();

                //
                // Menu Choice 
                //
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = UserProgrammingDisplayGetCommandParameters();
                        break;

                    case "b":
                        UserProgrammingDisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        DisplayFinchCommands(commands);
                        break;

                    case "d":
                        DisplayExecuteFinchCommands(finchRobot, commands, commandParameters);
                        break;

                    case "q":
                        quitMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Enter one of the letters to go it's menu.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!quitMenu);
        }
        /// <summary>
        ///  ***********************************************
        /// *  User Programming -- Execute Finch Commands  *
        /// ************************************************
        /// </summary>
        /// <param name="finchRobot"></param>
        /// <param name="commands"></param>
        /// <param name="commandParameters"></param>
        static void DisplayExecuteFinchCommands(Finch finchRobot, List<Command> commands, (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliSeconds = (int)(commandParameters.waitSeconds * 1000);

            string commandOutput = "";

            const int MOTOR_SPEED_TURN = 100;

            DisplayScreenHeader("Execute Finch Commands Menu");

            Console.WriteLine();
            Console.WriteLine("\tFinch Robot Will Execute Requested Commands");
            Console.WriteLine("Please enter a Finch Robot Command");
            DisplayContinuePrompt();

            //
            // Available commands block
            //

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;

                    case Command.MOVEFORWARD:
                        finchRobot.setMotors(motorSpeed, motorSpeed);
                        commandOutput = Command.MOVEFORWARD.ToString();
                        break;

                    case Command.MOVEBACKWARD:
                        finchRobot.setMotors(-motorSpeed, -motorSpeed);
                        commandOutput = Command.MOVEBACKWARD.ToString();
                        break;

                    case Command.STOPMOTORS:
                        finchRobot.setMotors(0, 0);
                        commandOutput = Command.STOPMOTORS.ToString();
                        break;

                    case Command.WAIT:
                        finchRobot.wait(waitMilliSeconds);
                        commandOutput = Command.WAIT.ToString();
                        break;

                    case Command.TURNRIGHT:
                        finchRobot.setMotors(MOTOR_SPEED_TURN, -MOTOR_SPEED_TURN);
                        commandOutput = Command.TURNRIGHT.ToString();
                        break;

                    case Command.TURNLEFT:
                        finchRobot.setMotors(-MOTOR_SPEED_TURN, MOTOR_SPEED_TURN);
                        commandOutput = Command.TURNLEFT.ToString();
                        break;

                    case Command.LEDON:
                        finchRobot.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;

                    case Command.LEDOFF:
                        finchRobot.setLED(0, 0, 0);
                        break;

                    case Command.GETTEMPERATURE:
                        commandOutput = $"Temperature: {finchRobot.getTemperature().ToString("n2")}\n";
                        break;

                    case Command.GETLIGHTLEVELRIGHT:
                        commandOutput = $"Right Light Sensor Level VaLue: {finchRobot.getRightLightSensor().ToString("n2")}\n";
                        break;

                    case Command.GETLIGHTLEVELLEFT:
                        commandOutput = $"Left Light Sensor Level Value: {finchRobot.getLeftLightSensor().ToString("n2")}\n";
                        break;

                    case Command.GETLIGHTLEVELAVERAGE:
                        int lightLevelAverage;
                        lightLevelAverage = ((finchRobot.getRightLightSensor() + finchRobot.getLeftLightSensor()) / 2);
                        commandOutput = $"Light Level Average For Both Sensors: {lightLevelAverage.ToString("n2")}\n";
                        break;

                    case Command.NOTEON:
                        finchRobot.noteOn(700);
                        commandOutput = Command.NOTEON.ToString();
                        break;

                    case Command.NOTEOFF:
                        finchRobot.noteOff();
                        commandOutput = Command.NOTEOFF.ToString();
                        break;

                    case Command.DONE:
                        commandOutput = Command.DONE.ToString();
                        break;

                    default:

                        break;
                }
                Console.WriteLine($"\t{commandOutput}");
            }
            Console.WriteLine();
            Console.WriteLine("" + commandOutput);
            Console.WriteLine();
            DisplayContinuePrompt();

        }
        /// <summary>
        ///  ***********************************************
        /// *  User Programming -- Display Finch Commands  *
        /// ************************************************
        /// </summary>
        /// <param name="commands"></param>
        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");
            foreach (Command command in commands)
            {
                Console.WriteLine();
                Console.WriteLine($"\t{command}");
                Console.WriteLine();
            }
            DisplayMenuPrompt("User Programming Menu");
        }
        /// <summary>
        /// ********************************************
        /// *  User Programming -- Get Finch Commands  *
        /// ********************************************
        /// </summary>
        /// <param name="commands"></param>
        static void UserProgrammingDisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;

            DisplayScreenHeader("Finch Robot Commands");

            //
            // List of commands
            //

            int amountCommand = 1;
            Console.WriteLine("\tCommands Available to Use:");
            Console.WriteLine();
            Console.WriteLine("\t");

            foreach (string nameCommand in Enum.GetNames(typeof(Command)))
            {
                Console.Write($"-{nameCommand.ToLower()} -\n");

                if (amountCommand == 0) Console.Write("-\n\t-");
                amountCommand++;
            }

            Console.WriteLine();
            
            while (command != Command.DONE)
            {
                Console.WriteLine("\tEnter Command:");

                if (Enum.TryParse(Console.ReadLine().ToUpper(), out command))
                {
                    commands.Add(command);
                }
                else
                {
                    Console.WriteLine("\t\t**********************************************");
                    Console.WriteLine("\t\t*** Invalid Value Please Enter Valid Value ***");
                    Console.WriteLine("\t\t**********************************************");

                }
            }
            DisplayMenuPrompt("User Programming");
        }
        /// <summary>
        ///  **********************************************
        /// *  User Programming -- Get Command Parameters *
        /// ***********************************************
        /// </summary>
        /// <returns></returns>
        static (int motorSpeed, int ledBrightness, double waitSeconds) UserProgrammingDisplayGetCommandParameters()
        {
            DisplayScreenHeader("Command Paramters Menu");

            (int motorSpeed, int ledBrightness, double waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            //
            // Get user desired speed
            //

            Console.WriteLine("\tEnter Motor Speed Between the Parameters: [1 - 255]:");
            int.TryParse(Console.ReadLine(), out commandParameters.motorSpeed);
            while (commandParameters.motorSpeed < 0 || commandParameters.motorSpeed > 255 || commandParameters.motorSpeed == 0)
            {
                Console.WriteLine("Invalid value, please enter a valid value [1 - 255]:");
                commandParameters.motorSpeed = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            Console.WriteLine("Your value {0} is valid", commandParameters.motorSpeed);
            Console.WriteLine();

            //
            // Get user desired brightness
            //

            Console.WriteLine("\tEnter Led Brightness Between the Parameters: [1 - 255]:");
            int.TryParse(Console.ReadLine(), out commandParameters.ledBrightness);
            while (commandParameters.ledBrightness < 0 || commandParameters.ledBrightness > 255 || commandParameters.ledBrightness == 0)
            {
                Console.WriteLine("Invalid value, please enter a valid value [1 - 255]:");
                commandParameters.ledBrightness = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            Console.WriteLine("Your value {0} is valid", commandParameters.ledBrightness);
            Console.WriteLine();

            //
            // Get user desired time
            //

            Console.WriteLine("\tEnter Wait Time Between the Parameters: [1 - 10]:");
            double.TryParse(Console.ReadLine(), out commandParameters.waitSeconds);
            while (commandParameters.waitSeconds < 0 || commandParameters.waitSeconds > 10 || commandParameters.waitSeconds == 0)
            {
                Console.WriteLine("Invalid value, please enter a valid value [1 - 10]:");
                commandParameters.waitSeconds = double.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            Console.WriteLine("Your value {0} is valid", commandParameters.waitSeconds);
            Console.WriteLine();

            //
            // Show user their inputs
            //

            Console.WriteLine();
            Console.WriteLine($"\tMotor Speed Input: {commandParameters.motorSpeed}");
            Console.WriteLine();
            Console.WriteLine($"\tLed Brightness Input: {commandParameters.ledBrightness}");
            Console.WriteLine();
            Console.WriteLine($"\tWait Time Input: {commandParameters.waitSeconds}");
            Console.WriteLine();

            DisplayMenuPrompt("User Programming Menu");

            return commandParameters;

        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();

            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion


    }
}
