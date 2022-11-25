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
    public static void OutputTxtFile(List<float[]> thingToPrint, string fileName)
    {
        float[] temp;
        int i = 0;
        int j = 1;
        FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);
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


    #region Program4 Readers

    public static List<float[]> ReadInForProjectionProblem(string fileName)
    {
        //Instantiate A and B matrices
        float[] point = new float[3];
        float[] normal = new float[3];
        float[] direction = new float[3];

        // Instantiate the streamreader
        StreamReader input = null;
        try
        {
            // Instantiate the list
            List<float[]> output = new List<float[]>();

            // Create stream reader object
            input = File.OpenText(fileName);

            // Read in the first line
            int i = 0; //Incrementer
            string line = input.ReadLine();

            if (line != null)
            {
                //Split up the first line and make the point, normal, and direction
                string[] firstLineSubstring = line.Split(' ');
                point[0] = float.Parse(firstLineSubstring[0]);
                point[1] = float.Parse(firstLineSubstring[1]);
                point[2] = float.Parse(firstLineSubstring[2]);
                normal[0] = float.Parse(firstLineSubstring[3]);
                normal[1] = float.Parse(firstLineSubstring[4]);
                normal[2] = float.Parse(firstLineSubstring[5]);
                direction[0] = float.Parse(firstLineSubstring[6]);
                direction[1] = float.Parse(firstLineSubstring[7]);
                direction[2] = float.Parse(firstLineSubstring[8]);

                //Add the point, normal, and direction to the list
                output.Add(point);
                output.Add(normal);
                output.Add(direction);
            }

            while (line != null)
            {
                float[] temp = new float[3];
                string[] substringOfLines = line.Split(' ');

                //Get the first point
                temp[0] = float.Parse(substringOfLines[0]);
                temp[1] = float.Parse(substringOfLines[1]);
                temp[2] = float.Parse(substringOfLines[2]);
                
                //Add to output
                output.Add(temp);

                //Get the second point
                temp[0] = float.Parse(substringOfLines[3]);
                temp[1] = float.Parse(substringOfLines[4]);
                temp[2] = float.Parse(substringOfLines[5]);

                //Add to output
                output.Add(temp);

                //Get the third point
                temp[0] = float.Parse(substringOfLines[6]);
                temp[1] = float.Parse(substringOfLines[7]);
                temp[2] = float.Parse(substringOfLines[8]);

                //Add to output
                output.Add(temp);

                line = input.ReadLine();
                i++;
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