using System;
using System.Collections.Generic;


namespace Addressbook
{
    class Program
    {
        static FileChanger Changer;
        static void Main(string[] args)
        {
            Changer = new FileChanger(System.AppDomain.CurrentDomain.BaseDirectory + "addressbook.csv");

            Console.WriteLine("Welcome to your personal address book");
            while (true)
            {
                string entry = getLine(
                    "\nAdd a new entry\t\t\t(0)\n" +
                    "Search for an entry\t\t(1)\n" +
                    "Update an existing entry\t(2)\n" +
                    "Delete an existing entry\t(3)\n" +
                    "Save your changes and Exit\t(4)\n" +
                    "Exit without Changes\t\t(Any Key)\n" +
                    "\nPlease enter your action: "
                    );

                switch (entry)
                {
                    case "0":
                        List<string> newEntry = new List<string>();
                        newEntry.Add(getLine("Please enter a name: "));
                        string input = "";
                        while (input != "*")
                        {
                            input = getLine("Please enter an entry (or * to stop): ");
                            if (input != "*") newEntry.Add(input);
                        }
                        Changer.AddNewEntry(newEntry);
                        Console.WriteLine("We added a new entry for: " + newEntry[1] + "\n");
                        break;
                    case "1":
                        string searchKey = getLine("Please enter a search key: ");
                        List<List<string>> temp = Changer.Search(searchKey);
                        Console.WriteLine("Search Results: ");
                        if (temp.Count == 0)
                        {
                            Console.WriteLine("Nothing\n");
                        }
                        else
                        {
                            foreach (List<string> address in temp)
                            {
                                string str = "[ ";
                                foreach (string entrie in address)
                                {
                                    str += entrie + ", ";
                                }
                                Console.WriteLine(str.Substring(0, str.Length - 2) + " ]");
                            }
                            Console.WriteLine("\n");
                        }
                        break;
                    case "2":
                        string[] updateKeys = new string[3];
                        updateKeys[0] = getLine("Please enter the name of the person to update: ");
                        updateKeys[1] = getLine("Please enter the entry you wish to update: ");
                        updateKeys[2] = getLine("Please enter the new value: ");
                        if (Changer.UpdateEntry(updateKeys[0], updateKeys[1], updateKeys[2]))
                        {
                            Console.WriteLine("\nUpdated the entry for " + updateKeys[0] + "\n");
                        }
                        else
                        {
                            Console.WriteLine("\nSorry, the update failed. Please try a different key\n");
                        }
                        break;
                    case "3":
                        string deleteName = getLine("Please enter the name of the person whose entry you wish to delete: ");
                        if (Changer.DeleteEntry(deleteName))
                        {
                            Console.WriteLine("\nDeleted the entry for " + deleteName + "\n");
                        }
                        else
                        {
                            Console.WriteLine("\nSorry, we could not find the entry to delete\n");
                        }
                        break;
                    case "4":
                        Console.WriteLine("\nYour addressbook is saved, goodbye.\n");
                        Changer.PrintAddressBook();
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nThank you and have a nice day\n");
                        System.Environment.Exit(0);
                        break;
                }
            }
        }

        /// <summary>
        /// Reads user input from the console and prepares it.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string getLine(string input)
        {
            Console.WriteLine(input);
            return Console.ReadLine().Trim();
        }
    }
}
