using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Enumeration;
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Reads files
/// </summary>
public class FileProcessor
{
    #region Program4 Writers

    /// <summary>
    /// For Part 1 txt files. Prints 3 points per line with the given list
    /// </summary>
    /// <param name="thingToPrint"></param>
    /// <param name="fileName"></param>
    public static void OutputTxtFile(List<float[]> thingToPrint, string fileName)
    {
        float[] temp;
        int i = 0;
        int j = 1;
        FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);

        try
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (float[] array in thingToPrint)
                {
                    temp = array;
                    while (i < temp.Length * j)
                    {
                        if ((i + 1) % 9 == 0)
                        {
                            writer.WriteLine(temp[i % 3]);
                        }
                        else
                        {
                            writer.Write(temp[i % 3] + " ");
                        }
                        i++;
                    }
                    j++;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            stream.Close();
        }
    }

    /// <summary>
    /// For part 2 output. Prints one thing per line
    /// </summary>
    /// <param name="thingToPrint"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static void OutputSingleLineTxtFile(List<float> thingToPrint, string fileName)
    {
        FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);

        try
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (float distance in thingToPrint)
                {
                    writer.WriteLine(distance);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            stream.Close();
        }
    }

    #endregion

    #region Program4 Readers

    public static float[,] GooglePageRankReader(string fileName)
    {
        //Instantiate streamreader
        StreamReader input = null;

        try
        {
            //Create stream reader object
            input = File.OpenText(fileName);

            //Read in the first line
            string line = input.ReadLine();

            //Split it so that we can set the size of the matrix
            string[] lineSplit = line.Split(' ');

            //Instantiate matrix
            float[,] output = new float[lineSplit.Length, lineSplit.Length];

            //Add the values in line split to the matrix
            for (int i = 0; i < output.GetLength(0); i++)
            {
                output[0, i] = float.Parse(lineSplit[i]);
            }

            //Iterate through the rest of the file and add it to the matrix
            for (int i = 1; i < output.GetLength(0); i++)
            {
                //Read in next line and split it
                line = input.ReadLine();
                lineSplit = line.Split(' ');

                //Iterate through the columns and add the appropriate value
                int j = 0;
                while (j < output.GetLength(0))
                {
                    output[i, j] = float.Parse(lineSplit[j]);
                    j++;
                }
            }

            return output;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            if (input != null)
            {
                input.Close(); 
            }
        }

        return new float[1, 1];
    }

    /// <summary>
    /// Reads in the input file for the projection problems
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<float[]> ReadInForProjectionProblem(string fileName)
    {
        // Instantiate the streamreader
        StreamReader input = null;
        try
        {
            // Instantiate the list
            List<float[]> output = new List<float[]>();

            // Create stream reader object
            input = File.OpenText(fileName);

            //Split each 9 digit line into 3 matrices
            string line = input.ReadLine();

            while (line != null)
            {

                float[] temp = new float[3];
                float[] temp2 = new float[3];
                float[] temp3 = new float[3];
                string[] substringOfLines = line.Split(' ');

                //Get the first point
                temp[0] = float.Parse(substringOfLines[0]);
                temp[1] = float.Parse(substringOfLines[1]);
                temp[2] = float.Parse(substringOfLines[2]);

                //Add to output
                output.Add(temp);

                //Get the second point
                temp2[0] = float.Parse(substringOfLines[3]);
                temp2[1] = float.Parse(substringOfLines[4]);
                temp2[2] = float.Parse(substringOfLines[5]);

                //Add to output
                output.Add(temp2);

                //Get the third point
                temp3[0] = float.Parse(substringOfLines[6]);
                temp3[1] = float.Parse(substringOfLines[7]);
                temp3[2] = float.Parse(substringOfLines[8]);

                //Add to output
                output.Add(temp3);

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

    #endregion

    #region Program3 Readers

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

    public static float[,] ReadInTriangleVertices(string fileName)
    {
        // Instantiate the streamreader
        StreamReader input = null;
        try
        {
            //Instantiate triangle vertices matrix
            float[,] vertices = new float[3, 3];

            // Create stream reader object
            input = File.OpenText(fileName);

            // Read and put each line into the string
            int i = 0; //Incrementer
            string line = input.ReadLine();
            while (line != null)
            {
                string[] substringOfLines = line.Split(' ');
                vertices[i, 0] = float.Parse(substringOfLines[0]);
                vertices[i, 1] = float.Parse(substringOfLines[1]);
                vertices[i, 2] = float.Parse(substringOfLines[2]);
                line = input.ReadLine();
                i++;
            }

            //Called if there is not a third line of values
            if (i == 2)
            {
                vertices[i, 0] = 1;
                vertices[i, 1] = 1;
                vertices[i, 2] = 1;
            }

            //Add A and B to the list of matrices, output
            return vertices;
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

    #endregion
}