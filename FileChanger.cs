using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.IO;

namespace Addressbook
{
    class FileChanger
    {
        internal string Path;
        private List<List<string>> addressList = new List<List<string>>();

        public FileChanger(string path)
        {
            Path = path;
            onLoad();
        }

        /// <summary>
        /// Opens and reads a file at the current path variable.
        /// If no file exists, the method creates a new one.
        /// </summary>
        private void onLoad()
        {
            //reads in all the data in  the csv file
            if (!File.Exists("addressbook.csv"))
            {
                File.Create(Path);
                return;
            }
            TextFieldParser parser = new TextFieldParser(Path);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            string[] dataSnippets = parser.ReadFields();
            List<string> tempEntry = new List<string>();

            //prepares the raw data into Lists
            if (dataSnippets == null) return;
            foreach (string snippet in dataSnippets)
            {
                //the "New" keyword marks a new entry
                if (snippet == "New")
                {
                    if (tempEntry.Count != 0)
                    {
                        addressList.Add(tempEntry);
                    }
                    tempEntry = new List<string>();
                }
                else
                {
                    tempEntry.Add(snippet);
                }
            }
            addressList.Add(tempEntry);
        }

        /// <summary>
        /// Searches the address book for a given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>all addresses which contain an entry which contains the key</returns>
        public List<List<string>> Search(string key)
        {
            List<List<string>> result = new List<List<string>>();
            foreach (List<string> address in addressList)
            {
                foreach (string entry in address)
                {
                    if (entry.Contains(key))
                    {
                        result.Add(address);
                        break;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Adds a new Entry
        /// </summary>
        /// <param name="newAddress"></param>
        public void AddNewEntry(List<string> newAddress)
        {
            addressList.Add(newAddress);
        }

        /// <summary>
        /// Changes a variable within an address.
        /// DOES NOT change all entries in that address.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="oldKey">used to find the variable which needs replacement</param>
        /// <param name="newKey"></param>
        public bool UpdateEntry(string name, string oldKey, string newVariable)
        {
            bool updated = false;
            foreach (List<string> address in addressList)
            {
                if (address[0].Contains(name))
                {
                    for (int i = 0; i < address.Count; ++i)
                    {
                        if (address[i].Contains(oldKey))
                        {
                            address[i] = newVariable;
                            updated = true;
                            break;
                        }
                    }
                    break;
                }
            }
            return updated;
        }

        /// <summary>
        /// Deletes an existing entry.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool DeleteEntry(string name)
        {
            bool isDeleted = false;
            foreach (List<string> address in addressList)
            {
                if (address[0] == name)
                {
                    addressList.Remove(address);
                    isDeleted = true;
                    break;
                }
            }
            return isDeleted;
        }

        /// <summary>
        /// Prints the current addressList into the current path.
        /// </summary>
        public void PrintAddressBook()
        {
            string text = "";
            foreach (List<string> address in addressList)
            {
                text += "New, ";
                foreach (string entry in address)
                {
                    text += entry + ", ";
                }
            }
            text = text.Substring(0, text.Length - 2);
            System.IO.File.WriteAllText(Path, text);
        }
    }
}
