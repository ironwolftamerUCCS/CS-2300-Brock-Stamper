using System;
using System.Collections.Generic;

namespace Program4_CS2300
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool keepTesting = true;

            while (keepTesting)
            {
                //Ask for and pass in the file name
                Console.Write("File name: ");
                string fileName = Console.ReadLine();

                //Ask which test to run on the file
                Console.WriteLine("Which test would you like to run? (1 (projection), 2 (distance and intersection), or 3 (Google PageRank))");
                int test = int.Parse(Console.ReadLine());
                switch (test)
                {
                    case 1:
                        List<float[]> projectionProblemData = FileProcessor.ReadInForProjectionProblem(fileName);
                        FileProcessor.OutputTxtFile(projectionProblemData, "ppd.txt");
                        MatrixManipulator.ParallelProjection(projectionProblemData);
                        MatrixManipulator.PerspectiveProjection(projectionProblemData);
                        break;

                    case 2:
                        List<float[]> lineToPlaneDistanceData = FileProcessor.ReadInForProjectionProblem(fileName);
                        FileProcessor.OutputTxtFile(lineToPlaneDistanceData, "lypdd.txt");
                        MatrixManipulator.DistanceToPlane(lineToPlaneDistanceData);
                        break;

                    case 3:
                        Console.WriteLine("Under construction");
                        break;

                    default: Console.WriteLine("Invalid answer"); break;
                }

                Console.WriteLine("Keep testing? (1 = yes, 0 = no)");
                int answer = int.Parse(Console.ReadLine());
                if (answer == 0) { keepTesting = false; }
            }
        }
    }
}
