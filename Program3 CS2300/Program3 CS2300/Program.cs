using System;
using System.Collections.Generic;
using System.IO;

namespace Program3_CS2300
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Ask for and pass in the file name
            Console.Write("File name: ");
            string fileName = Console.ReadLine();

            //Ask which test to run on the file
            Console.WriteLine("Which test would you like to run? (1 (ax=b), 2(eigen things), or 3(triangle things))");
            int test = int.Parse(Console.ReadLine());
            switch (test)
            {
                case 1:
                    //Set A and B
                    List<float[,]> AAndB = FileProcessor.ReadInAAndB(fileName);
                    float[,] A = AAndB[0];
                    float[,] B = AAndB[1];
                    Console.WriteLine("------");
                    MatrixManipulator.SolveMatrixEquation(A, B, true);
                    break;

                case 2:
                    AAndB = FileProcessor.ReadInAAndB(fileName);
                    A = AAndB[0];
                    Console.WriteLine("------");
                    MatrixManipulator.SolveEigenThings(A);
                    break;
                case 3: break;

                default: Console.WriteLine("Invalid answer"); break;
            }
        }
    }
}
