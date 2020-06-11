using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Revised solution of finch project, original project had mutliple syntax errors
    // Application Type: Console
    // Author: Vanderploeg, Casey
    // Dated Created: 6/10/2020
    // Last Modified: 6/10/2020
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
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
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

        static void LightAlarmDisplayElapsedTime(int elapsedTime)
        {
            Console.SetCursorPosition(20, 20);
            Console.WriteLine($"Elapsed Time:              ");
            Console.WriteLine($"Elapsed Time: {elapsedTime}");
        }

        static bool LightAlarmMonitorLightSensors(Finch finchRobot, string sensorsToMonitor, string rangeType, int minMaxThresholdValue, int timeToMonitor)
        {
            bool thresholdExceeded = false;
            int elapsedTime = 0;
            int currentLightSensorValue = 0;
            string sensorsToMonitor = "";

            while (!thresholdExceeded && elapsedTime < timeToMonitor)
            {
                currentLightSensorValue = LightAlarmCurrentSensorValue(finchRobot, sensorsToMonitor);

                switch (rangeType)
                {
                    case "minimum":
                        if (currentLightSensorValue < minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;

                    case "maximum":
                        if (currentLightSensorValue > minMaxThresholdValue)
                        {
                            thresholdExceeded = true;
                        }
                        break;
                }

                finchRobot.wait(1000);
                elapsedTime++;
                LightAlarmDisplayElapsedTime(elapsedTime);
            }

            return thresholdExceeded;

        }

        static int LightAlarmCurrentSensorValue(Finch finchRobot, string sensorsToMonitor)
        {
            int currentLightSensorValue = 0;

            switch (sensorsToMonitor)
            {
                case "left":
                    currentLightSensorValue = finchRobot.getLeftLightSensor();
                    break;

                case "right":
                    currentLightSensorValue = finchRobot.getRightLightSensor();
                    break;

                case "both":
                    currentLightSensorValue = (finchRobot.getLeftLightSensor() + finchRobot.getRightLightSensor()) / 2;
                    currentLightSensorValue = (int)(finchRobot.getLightSensors().Average());
                    break;
            }
            return currentLightSensorValue;
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

                Console.Write("Range Type: [minimum, maximum]");
                userResponse = Console.ReadLine().ToLower();

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
