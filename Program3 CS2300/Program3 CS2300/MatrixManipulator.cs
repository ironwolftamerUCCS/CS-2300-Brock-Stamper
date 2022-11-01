using System;
using System.Collections.Generic;
using System.IO;
using static System.Formats.Asn1.AsnWriter;

public class MatrixManipulator
{
    /// <summary>
    /// Prints a 2D matrix to the console
    /// </summary>
    /// <param name="matrix">Matrix to print</param>
	public static void Print2DMatrix(float[,] matrix)
	{
        // Loop support
        int row = 0;
        int col = 0;

        // Print each cell in the matrix
        while (row < matrix.GetLength(0))
        {
            while (col < matrix.GetLength(1))
            {
                Console.Write(SigDigRounder(matrix[row, col], 4) + " ");
                col++;
            }
            Console.WriteLine();
            row++;
            col = 0;
        }
    }

    /// <summary>
    /// Solves the matrix equation Ax=B for x
    /// </summary>
    /// <param name="A">The 2x2 transformation matrix</param>
    /// <param name="B">The 2x1 result</param>
    /// <param name="print">Whether or not to print</param>
    public static float[,] SolveMatrixEquation(float[,] A, float[,] B, bool print = false)
    {
        //Declare variables, s is shear for A, X is solution
        float[,] shear = new float[2, 2];
        float[,] X = new float[2, 1];

        //Setting and applying shear for left side
        A[1, 0] = CanRoundToZero(A[1, 0]);
        if (A[1, 0] != 0 )
        {
            //Set shear
            shear[1, 0] = (A[1, 0]  / -A[0, 0]);
            shear[0, 0] = 1;
            shear[0, 1] = 0;
            shear[1, 1] = 1;

            //Multiply against A
            A = MatrixMultiplier2D(shear, A);
            B = MatrixMultiplier2D(shear, B);
        }

        //Check to see if previous shear turned the bottom rows completely to 0 or if the bottom row was already all 0's
        //(We already checked the bottom left spot so we only need to check the bottom right)
        A[1, 1] = CanRoundToZero(A[1, 1]);
        A[1, 0] = CanRoundToZero(A[1, 0]);
        A[0, 1] = CanRoundToZero(A[0, 1]);
        if (A[1, 1] == 0)
        {
            //If b2 = 0, then infinite solutions, if it doesn't, then no solutions
            B[1, 0] = CanRoundToZero(B[1, 0]);
            if (B[1, 0] != 0)
            {
                if (print) { Console.WriteLine("System Inconsistant"); }

                //Since there is no solution, returns null
                return null;
            }
            else
            {
                if (print) { Console.WriteLine("System Inconsistant"); }
                
                //Let x2 = 0, solve for x1
                X[1, 0] = 1;
                X[0, 0] = -A[0, 1] / A[0, 0];
                return X;
            }
        }
        //Setting and applying shear for right side
        else if (A[0, 1] != 0)
        {
            //Set shear
            shear[0, 1] = (-A[0, 1] / A[1, 1]);
            shear[0, 0] = 1;
            shear[1, 0] = 0;
            shear[1, 1] = 1;

            //Multiply against A
            A = MatrixMultiplier2D(shear, A);
            B = MatrixMultiplier2D(shear, B);
        }

        //Find x1 using b1 and a11
        X[0, 0] = B[0, 0] / A[0, 0];

        //Find x2 using b2 and a22
        X[1, 0] = B[1, 0] / A[1, 1];

        //Print and return solution
        if (print) 
        {
            Console.WriteLine("Solution:");
            Print2DMatrix(X); 
        }
        return X;
    }

    /// <summary>
    /// Multiplies 2 2D matrices together. Matrix 1 columns must equal matrix 2 rows
    /// </summary>
    /// <param name="m1">1st matrix</param>
    /// <param name="m2">2nd matrix</param>
    /// <returns></returns>
    public static float[,] MatrixMultiplier2D (float[,] m1, float[,] m2)
    {
        //Set output
        float[,] output = new float[m1.GetLength(0), m2.GetLength(1)];

        //Loop support
        List<float> rowNums = new List<float>();
        List<float> columnNums = new List<float>();
        int row1 = 0;
        int row2 = 0;
        int col1 = 0;
        int col2 = 0;

        //Multiply the matrices
        //Take each row
        while (row1 < m1.GetLength(0))
        {
            //Save each column value in a list
            while (col1 < m1.GetLength(1))
            {
                rowNums.Add(m1[row1, col1]);
                col1++;
            }
            //Take each column
            while (col2 < m2.GetLength(1))
            {
                //Save each row value in a list
                while (row2 < m2.GetLength(0))
                {
                    columnNums.Add(m2[row2, col2]);
                    row2++;
                }
                //Loop through both lists adding together each position's product ([1]*[1] + [2]*[2] +...)
                for (int i = 0; i < rowNums.Count; i++)
                {
                    output[row1, col2] += rowNums[i] * columnNums[i];
                }
                //Clear the column list to go back and save the next set of row values in the next column
                columnNums.Clear();
                row2 = 0;
                col2++;
            }
            //Clear the row list to go back and save the next set of column values in the next row
            rowNums.Clear();
            col1 = 0;
            col2 = 0;
            row1++;
        }

        return output;
    }

    /// <summary>
    /// Solves for a matrix's eigenvalues, eigenvectors, R, and computes R*(Lambda)*R(transposed)
    /// </summary>
    /// <param name="matrix">2x2 2D matrix</param>
    public static void SolveEigenThings(float[,] matrix)
    {
        //Calculation support
        float[,] eValues = new float[2, 1];

        //Check to make sure that the matrix has real eigenvalues
        //Set variables for the quadratic equation (a is always 1)
        float b = -matrix[0, 0] - matrix[1, 1];
        float c = (matrix[0, 0] * matrix[1, 1]) - (matrix[0, 1] * matrix[1, 0]);
        
        //Using the quadratic equation plug check the inside of the quadratic
        float discriminant = MathF.Pow(b, 2) - 4 * c;

        //If the inside of the quadratic equation is less than 0, there are no real eigen values
        if (discriminant < 0)
        {
            Console.WriteLine("No real eigenvalues");
            return;
        }

        //Calculate each eigen value, no sorting based on dominance
        eValues[0, 0] = (-b + MathF.Sqrt(discriminant)) / 2;
        eValues[1, 0] = (-b - MathF.Sqrt(discriminant)) / 2;

        //Create and print lambda
        float[,] lambda = new float[2, 2];
        lambda[0, 0] = eValues[0, 0];
        lambda[1, 1] = eValues[1, 0];
        Console.WriteLine("Lambda:");
        Print2DMatrix(lambda);



        //Solve Eigenvectors
        //Set new 2x2 matrices
        float[,] eigenManipulatedM1 = new float[2, 2];
        float[,] eigenManipulatedM2 = new float[2, 2];
        float[,] zeroVector = { {0}, {0} };

        //Subtract eigenvalue 1 from the top left and bottom right values of the input matrix, set rest to equivilant position in matrix
        eigenManipulatedM1[0, 0] = matrix[0, 0] - eValues[0, 0];
        eigenManipulatedM1[1, 1] = matrix[1, 1] - eValues[0, 0];
        eigenManipulatedM1[1, 0] = matrix[1, 0];
        eigenManipulatedM1[0, 1] = matrix[0, 1];

        //Subtract eigenvalue 2 from the top left and bottom right values of the input matrix, set rest to equivilant position in matrix
        eigenManipulatedM2[0, 0] = matrix[0, 0] - eValues[1, 0];
        eigenManipulatedM2[1, 1] = matrix[1, 1] - eValues[1, 0];
        eigenManipulatedM2[1, 0] = matrix[1, 0];
        eigenManipulatedM2[0, 1] = matrix[0, 1];

        //Pass in each eigenvector and the zero vector and print them
        float[,] eigenvector1 = SolveMatrixEquation(eigenManipulatedM1, zeroVector);
        float[,] eigenvector2 = SolveMatrixEquation(eigenManipulatedM2, zeroVector);

        //Create and print R
        float[,] R = new float[2, 2];
        R[0, 0] = eigenvector1[0, 0];
        R[1, 0] = eigenvector1[1, 0];
        R[0, 1] = eigenvector2[0, 0];
        R[1, 1] = eigenvector2[1, 0];
        Console.WriteLine("R:");
        Print2DMatrix(R);

        //Transpose R
        float[,] rTransposed = new float[2, 2];
        rTransposed[1, 0] = R[0, 1];
        rTransposed[0, 1] = R[1, 0];
        rTransposed[0, 0] = R[0, 0];
        rTransposed[1, 1] = R[1, 1];

        //Multiply R by lambda then by rTransposed the print the resulting eigendecomp
        float[,] rLambda = MatrixMultiplier2D(R, lambda);
        float[,] eigenDecomp = MatrixMultiplier2D(rLambda, rTransposed);
        Console.WriteLine("EigenDecomp:");
        Print2DMatrix(eigenDecomp);

        //Check if eigendecomp is equal to matrix
        //Set bool
        bool isSame = true;

        if (eigenDecomp[0, 0] != matrix[0, 0])
        {
            //Find the factor that eigendecomp may be mulitplied by
            float factor = matrix[0, 0] / eigenDecomp[0, 0];

            //Loop support
            int row = 0;
            int col = 0;

            //Loop through eigenDecomp and make sure each cell is equal to the same cell in matrix
            while (row < matrix.GetLength(0))
            {
                while (col < matrix.GetLength(1))
                {
                    if (MathF.Round(eigenDecomp[row, col] * factor) != matrix[row, col])
                    {
                        isSame = false;
                    }
                    col++;
                }
                row++;
                col = 0;
            }
        }

        //Print out whether is same is true or false
        if (isSame) { Console.WriteLine("Is same: " + 1);  }
        else { Console.WriteLine("Is same: " + 0);  }
    }

    /// <summary>
    /// Solves for the area of the triangle given by the points of the matrix
    /// Also solves for the distance to the line (2D triangle) of the 3rd column point
    /// or solves for the distance to the plane (3D triangle) of the 3rd column point
    /// </summary>
    /// <param name="matrix">triangle's vertices</param>
    public static void SolveTriangleThings(float[,] matrix)
    {
        //Find the vectors that define the triangle
        float[] vector1 = new float[] { matrix[0, 0] - matrix[0, 1], matrix[1, 0] - matrix[1, 1], matrix[2, 0] - matrix[2, 1] };
        float[] vector2 = new float[] { matrix[0, 1] - matrix[0, 2], matrix[1, 1] - matrix[1, 2], matrix[2, 1] - matrix[2, 2] };

        ////Find determinant of triangle
        //float determinant = matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1])
        //    - matrix[1, 0] * (matrix[0, 1] * matrix[2, 2] - matrix[0, 2] * matrix[2, 1])
        //    + matrix[2, 0] * (matrix[0, 1] * matrix[1, 2] - matrix[0, 2] * matrix[1, 1]);

        ////Calculate area of the triangle
        //float area = 0.5f * MathF.Abs(determinant);

        ////Prints out the area
        //Console.WriteLine("Area:");
        //Console.WriteLine(area);
    }

    /// <summary>
    /// Rounds a number to a certain amount of signinificant digits
    /// </summary>
    /// <param name="number">number to round</param>
    /// <param name="numSigDig">number of significant digits</param>
    /// <returns>rounded number</returns>
    /// Taken from Stack Overflow from a "P Daddy"
    public static float SigDigRounder (float number, int numSigDig)
    {
        if (number == 0) { return number; }

        float scale = MathF.Pow(10, MathF.Floor(MathF.Log10(MathF.Abs(number))) + 1);
        number = scale * MathF.Round(number / scale, numSigDig);
        return number;
    }

    /// <summary>
    /// Rounds a number to zero if it is sufficiently small enough
    /// </summary>
    /// <param name="number">number to check and round</param>
    /// <returns>zero or the initial number</returns>
    public static float CanRoundToZero (float number)
    {
        //Set tolerance
        float zeroTolerance = 0.000001f;

        //Check if number is within the tolerance
        if (number < zeroTolerance && number > -zeroTolerance)
        {
            //Set to 0 if so
            number = 0;
        }

        return number;
    }
}
