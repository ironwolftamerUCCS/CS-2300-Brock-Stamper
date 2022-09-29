using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

public class FileReader
{
    /// <summary>
    /// Turns a txt file into a list of strings based on the lines of the file
    /// </summary>
    /// <param name="fileName">File you wish to turn into a string</param>
    /// <returns>list of each line in the file</returns>
    public static List<string> FileInputToList (string fileName)
    {
        // Instantiate the streamreader
        StreamReader input = null;
        try
        {
            File.OpenText(fileName);

            // Instantiate the list
            List<string> output = new List<string>();

            // Create stream reader object
            input = File.OpenText(fileName);

            // Read and put each line into the string
            string line = input.ReadLine();
            while (line != null)
            {
                output.Add(line);
                line = input.ReadLine();
            }

            return output;
        }

        // Catch any exceptions and print the message then return null
        catch (Exception e)
        {
            // Print exception
            Console.WriteLine(e.Message);
            return null;
        }

        finally
        {
            // Always close input file
            if (input != null)
            {
                input.Close();
            }
        }
    }
}
