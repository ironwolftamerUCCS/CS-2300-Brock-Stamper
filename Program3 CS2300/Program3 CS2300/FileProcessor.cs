using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Reads files
/// </summary>
public class FileProcessor
{
    /// <summary>
    /// Turns a txt file into a list of strings based on the lines of the file
    /// </summary>
    /// <param name="fileName">File you wish to turn into a string</param>
    /// <returns>list of each line in the file</returns>
    public static List<float[,]> ReadInAAndB (string fileName)
    {
        //Instantiate A and B matrices
        float[,] A = new float[2, 2];
        float[,] B = new float[2, 1];
        
        // Instantiate the streamreader
        StreamReader input = null;
        try
        {
            // Instantiate the list
            List<float[,]> output = new List<float[,]>();

            // Create stream reader object
            input = File.OpenText(fileName);

            // Read and put each line into the string
            int i = 0; //Incrementer
            string line = input.ReadLine();
            while (line != null)
            {
                string[] substringOfLines = line.Split(' ');
                A[i, 0] = float.Parse(substringOfLines[0]);
                A[i, 1] = float.Parse(substringOfLines[1]);
                B[i, 0] = float.Parse(substringOfLines[2]);
                line = input.ReadLine();
                i++;
            }

            //Add A and B to the list of matrices, output
            output.Add(A);
            output.Add(B);
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