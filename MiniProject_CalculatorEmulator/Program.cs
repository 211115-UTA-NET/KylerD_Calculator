using System.IO;
using System;

public class Calculator
{
    static bool calcHistory = false;

    public static void Main(string[] args)
    {
        MainMenu();
    }

    static void MainMenu()
    {
        Console.WriteLine("Welcome! Please enter a number:");
        Console.WriteLine("1: Basic Calculator \n" + "2: Advanced Calculator \n" + "3: Toggle calculation history: " + calcHistory + "\n" + "4: Delete calculation history \n" + "5: Exit");
        bool inMenu = true;

        while (inMenu == true)
        {
            //Test the entry for a valid int
            string calcType = Console.ReadLine();
            int calcTypeParsed;
            bool calcTypeTryParse = int.TryParse(calcType, out calcTypeParsed);

            if (calcTypeTryParse == true && calcTypeParsed == 1) //Open the basic emulator
            {
                BasicEmulator();
            }
            else if (calcTypeTryParse == true && calcTypeParsed == 2) //Open the advanced emulator
            {
                AdvancedEmulator();
            }
            else if (calcTypeTryParse == true && calcTypeParsed == 3) //Toggle calculation history to file output
            {
                ToggleCalculationHistory();
            }
            else if (calcTypeTryParse == true && calcTypeParsed == 4)
            {
                DeleteCalculationHistory();
            }
            else if (calcTypeTryParse == true && calcTypeParsed == 5) //Close the program
            {
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
            }
            else //Invalid entry
                Console.WriteLine("Please enter a valid number.");
        }
    }

    static void ToggleCalculationHistory()
    {
        if (calcHistory == false)
        {
            calcHistory = true;
            Console.WriteLine("Calculation history has been toggled to: " + calcHistory);
        }
        else if (calcHistory == true)
        {
            calcHistory = false;
            Console.WriteLine("Calculation history has been toggled to: " + calcHistory);
        }
        MainMenu();
    }

    static async Task DeleteCalculationHistory()
    {
        Console.WriteLine("Are you sure you want to delete your calculation history? (Y / N)");
        string deleteCheck = Console.ReadLine();
        bool deleteResponse = (deleteCheck == "Y");
        if (deleteResponse == true)
        {
            using StreamWriter file = new("CalculatorEmulatorHistory.txt", append: false);
            file.Dispose();
            Console.WriteLine("Successfully cleared calculation history!");
        }
        else
        {
            Console.WriteLine("Nothing was deleted.");
        }
        MainMenu();
    }

    static async Task BasicEmulator()
    {
        Console.WriteLine("Welcome to the basic calculator emulator! \n" + "Type 'Menu' to return to the main menu. \n" + "Type 'Exit' to close the application. \n" + "Otherwise, please enter your first number:");
        float firstNum = ConvertMyNumber();

        Console.WriteLine("Please enter an operator:"); //What is the operator?
        int operatorTest = 0;
        bool activeOperator = true;
        while (activeOperator == true)
        {
            string operatorEntry = Console.ReadLine();
            operatorTest = TestMyOperator(operatorEntry);
            if (operatorTest >= 1 && operatorTest <= 4)
                break;
            else if (operatorTest == 5)
                MainMenu();
            else if (operatorTest == 6)
                Environment.Exit(0);
            else
                Console.WriteLine("Please enter a valid operator.");
        }

        Console.WriteLine("Please enter your second number:");
        float secondNum = ConvertMyNumber();

        float finalResults = 0.0f;
        if (operatorTest == 1) //Perform operators based on results
        {
            finalResults = firstNum + secondNum;
            Console.WriteLine(firstNum + " " + "+" + " " + secondNum + " " + "=" + " " + finalResults);
            if (calcHistory == true)
            {
                using StreamWriter file = new("CalculatorEmulatorHistory.txt", append: true);
                await file.WriteLineAsync(firstNum + " " + "+" + " " + secondNum + " " + "=" + " " + finalResults);
            }
        }
        else if (operatorTest == 2)
        {
            finalResults = firstNum - secondNum;
            Console.WriteLine(firstNum + " " + "-" + " " + secondNum + " " + "=" + " " + finalResults);
            if (calcHistory == true)
            {
                using StreamWriter file = new("CalculatorEmulatorHistory.txt", append: true);
                await file.WriteLineAsync(firstNum + " " + "-" + " " + secondNum + " " + "=" + " " + finalResults);
            }
        }
        else if (operatorTest == 3)
        {
            finalResults = firstNum * secondNum;
            Console.WriteLine(firstNum + " " + "*" + " " + secondNum + " " + "=" + " " + finalResults);
            if (calcHistory == true)
            {
                using StreamWriter file = new("CalculatorEmulatorHistory.txt", append: true);
                await file.WriteLineAsync(firstNum + " " + "*" + " " + secondNum + " " + "=" + " " + finalResults);
            }
        }
        else if (operatorTest == 4)
        {
            finalResults = firstNum / secondNum;
            Console.WriteLine(firstNum + " " + "/" + " " + secondNum + " " + "=" + " " + finalResults);
            if (calcHistory == true)
            {
                using StreamWriter file = new("CalculatorEmulatorHistory.txt", append: true);
                await file.WriteLineAsync(firstNum + " " + "/" + " " + secondNum + " " + "=" + " " + finalResults);
            }
        }

        BasicEmulator();
    }

    static float ConvertMyNumber()
    {
        bool numTest;
        bool activeNum = true;
        float myNum = 0;
        while (activeNum == true)
        {
            string myEntry = Console.ReadLine();
            numTest = TestMyNumber(myEntry); //Is the first entry valid?
            if (numTest == true) //Valid entry
            {
                myNum = float.Parse(myEntry);
                //Console.WriteLine(firstNum); //For testing purposes
                activeNum = false;
                return myNum;
            }
            else //Invalid entry
            {
                int myNumExtra = ExtraNumberTest(myEntry); //Is the entry a spelled out number?
                if (myNumExtra >= 0 && myNumExtra <= 9)
                {
                    myNum = myNumExtra;
                    activeNum = false;
                    //Console.WriteLine(firstNum); //For testing purposes
                    return myNum;
                }
                else
                    Console.WriteLine("Please enter a valid number.");
            }
        }
        return myNum;
    }

    static bool TestMyNumber(string myEntry)
    {
        //Test the entry for valid int
        float myNum;
        bool myEntryTryParse = float.TryParse(myEntry, out myNum);

        if (myEntryTryParse == true) //If valid
        {
            return true;
        }
        else if (myEntryTryParse == false && myEntry == "Menu") //If 'Menu'
        {
            MainMenu();
            return false;
        }
        else if (myEntryTryParse == false && myEntry == "Exit") //If 'Exit'
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
            return false;
        }
        else //Invalid Input
        {
            return false;
        }
    }

    static int ExtraNumberTest(string myEntry)
    {
        if (myEntry == "zero")
            return 0;
        else if (myEntry == "one")
            return 1;
        else if (myEntry == "two")
            return 2;
        else if (myEntry == "three")
            return 3;
        else if (myEntry == "four")
            return 4;
        else if (myEntry == "five")
            return 5;
        else if (myEntry == "six")
            return 6;
        else if (myEntry == "seven")
            return 7;
        else if (myEntry == "eight")
            return 8;
        else if (myEntry == "nine")
            return 9;
        else
            return 10;
    }

    static int TestMyOperator(string myOperator)
    {
        if (myOperator == "+" || myOperator == "plus")
            return 1;
        else if (myOperator == "-" || myOperator == "minus")
            return 2;
        else if (myOperator == "*" || myOperator == "times")
            return 3;
        else if (myOperator == "/" || myOperator == "divided-by")
            return 4;
        else if (myOperator == "Menu")
            return 5;
        else if (myOperator == "Exit")
            return 6;
        else
            return 7;
    }

    static async Task AdvancedEmulator()
    {
        Console.WriteLine("Welcome to the advanced emulator! \n" + "Type 'Menu' to return to the main menu. \n" + "Type 'Exit' to close the application. \n" + "Otherwise, please enter your problem:");
        while (true)
        {
            string myProblem = Console.ReadLine();
            if (myProblem == "Menu")
                MainMenu();
            else if (myProblem == "Exit")
            {
                Console.WriteLine("Goodbye!");
                Environment.Exit(0);
            }
            int counter = 0;
            float myFirstNumber = 0;
            float myNumber;
            float finalResults = 0.0f;
            string[] splitProblem = myProblem.Split(" ");
            int validNumOfEntries = splitProblem.Length % 2;
            //Console.WriteLine(splitProblem.Length); //For testing purposes
            if (validNumOfEntries == 0 || splitProblem.Length == 1)
            {
                Console.WriteLine("Error: Invalid number of entries");
            }
            else
            {
                for (int i = 0; i < splitProblem.Length; i++)
                {
                    if (counter == 0) //Validate the first entry
                    {
                        bool tryMyFirstNumber = AdvancedNumberConversion(splitProblem[i]);
                        if (tryMyFirstNumber == true)
                        {
                            myFirstNumber = float.Parse(splitProblem[i]);
                            //Console.WriteLine("1: " + myFirstNumber); //For testing purposes
                        }
                        else
                        {
                            int myFirstNumExtra = ExtraNumberTest(splitProblem[i]); //Is the entry a spelled out number?
                            if (myFirstNumExtra >= 0 && myFirstNumExtra <= 9)
                            {
                                string myFirstNumExtraString = Convert.ToString(myFirstNumExtra);
                                splitProblem[i] = myFirstNumExtraString;
                                myFirstNumber = myFirstNumExtra;
                                //Console.WriteLine("2: " + myFirstNumber); //For testing purposes
                            }
                            else
                            {
                                Console.WriteLine("Error: Your entry '" + splitProblem[i] + "' is not a valid digit.");
                                break;
                            }
                        }
                        counter++;
                    }
                    else if (counter % 2 == 0 && counter != 0) //Get our next number to apply to previous
                    {
                        bool tryMyNumber = AdvancedNumberConversion(splitProblem[i]);
                        if (tryMyNumber == true)
                        {
                            myNumber = float.Parse(splitProblem[i]);
                            //Console.WriteLine("3: " + myNumber); //For testing purposes
                        }
                        else
                        {
                            int myNumExtra = ExtraNumberTest(splitProblem[i]); //Is the entry a spelled out number?
                            if (myNumExtra >= 0 && myNumExtra <= 9)
                            {
                                string myNumExtraString = Convert.ToString(myNumExtra);
                                splitProblem[i] = myNumExtraString;
                                myNumber = myNumExtra;
                                //Console.WriteLine("4: " + myNumber); //For testing purposes
                            }
                            else
                            {
                                Console.WriteLine("Error: Your entry '" + splitProblem[i] + "' is not a valid digit.");
                                break;
                            }
                        }
                        //Console.WriteLine("6: " + finalResults); //For testing purposes

                        int testMyOperator = AdvancedTestMyOperator(splitProblem[i - 1]);
                        if (testMyOperator == 1)
                        {
                            finalResults = finalResults + myNumber;
                            splitProblem[i - 1] = "+";
                        }
                        else if (testMyOperator == 2)
                        {
                            finalResults = finalResults - myNumber;
                            splitProblem[i - 1] = "-";
                        }
                        else if (testMyOperator == 3)
                        {
                            finalResults = finalResults * myNumber;
                            splitProblem[i - 1] = "*";
                        }
                        else if (testMyOperator == 4)
                        {
                            finalResults = finalResults / myNumber;
                            splitProblem[i - 1] = "/";
                        }
                        else if (testMyOperator == 5)
                        {
                            Console.WriteLine("Error: Your entry '" + splitProblem[i - 1] + "' is not a valid operator.");
                            break;
                        }
                        counter = counter + 2;
                        i++;
                    }

                    else if (counter == 1) //If we loop through our first sign, do nothing
                    {
                        int testMyOperator = AdvancedTestMyOperator(splitProblem[i]);
                        if (testMyOperator >= 1 || testMyOperator <= 4)
                        {
                            finalResults = myFirstNumber;
                            //Console.WriteLine("5: " + finalResults); //For testing purposes
                        }
                        else if (testMyOperator == 5)
                        {
                            Console.WriteLine("Error: Your entry '" + splitProblem[i] + "' is not a valid operator.");
                            break;
                        }
                        counter++;
                    }
                    else
                    {
                        Console.WriteLine("Error: Please try again.");
                    }
                }
                if (counter - 1 == splitProblem.Length) //Only print if loop was fully finished
                {
                    myProblem = "";
                    for (int j = 0; j < splitProblem.Length; j++)
                    {
                        myProblem = myProblem + splitProblem[j] + " ";
                    }
                    Console.WriteLine(myProblem + "= " + finalResults);
                    if (calcHistory == true)
                    {
                        using StreamWriter file = new("CalculatorEmulatorHistory.txt", append: true);
                        await file.WriteLineAsync(myProblem + "= " + finalResults);
                    }
                }
            }
        }
    }

    static bool AdvancedNumberConversion(string myEntry)
    {
        bool numTest;
        numTest = AdvancedNumberTest(myEntry); //Is the first entry valid?
        if (numTest == true) //Valid entry
        {
            return true;
        }
        else //Invalid entry
            return false;
    }

    static bool AdvancedNumberTest(string myEntry)
    {
        //Test the entry for valid int
        float myNum;
        bool myEntryTryParse = float.TryParse(myEntry, out myNum);

        if (myEntryTryParse == true) //If valid
        {
            return true;
        }
        else //Invalid Input
        {
            return false;
        }
    }

    static int AdvancedTestMyOperator(string myOperator)
    {
        if (myOperator == "+" || myOperator == "plus")
            return 1;
        else if (myOperator == "-" || myOperator == "minus")
            return 2;
        else if (myOperator == "*" || myOperator == "times")
            return 3;
        else if (myOperator == "/" || myOperator == "divided-by")
            return 4;
        else
            return 5;
    }
}