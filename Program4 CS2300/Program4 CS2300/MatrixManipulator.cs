using System;
using System.Collections.Generic;
using System.IO;
using static System.Formats.Asn1.AsnWriter;

public class MatrixManipulator
{
    #region Program4 Methods



    #endregion

    #region Program3 Methods

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
    /// Also solves for the distance to the line that spans the first 2 points (2D triangle) of the 3rd point
    /// or solves for the distance to the plane that bisects the first 2 points (3D triangle) of the 3rd point
    /// </summary>
    /// <param name="matrix">triangle's vertices</param>
    public static void SolveTriangleThings(float[,] matrix)
    {
        //Find the vectors that define the triangle
        float[] vector1 = new float[] { matrix[0, 0] - matrix[0, 1], matrix[1, 0] - matrix[1, 1], matrix[2, 0] - matrix[2, 1] }; //pt 1 to pt 2
        float[] vector2 = new float[] { matrix[0, 0] - matrix[0, 2], matrix[1, 0] - matrix[1, 2], matrix[2, 0] - matrix[2, 2] }; //pt 1 to pt 3

        //Find the cross product (also the normal to the plane)
        float[] crossProduct = new float[] { vector1[1] * vector2[2] - vector2[1] * vector1[2],
                                            vector1[2] * vector2[0] - vector2[2] * vector1[0],
                                            vector1[0] * vector2[1] - vector2[0] * vector1[1]};

        //Find the magnitude of the cross product and half it to get the area of the triangle
        float area = MathF.Sqrt(MathF.Pow(crossProduct[0], 2) + MathF.Pow(crossProduct[1], 2) + MathF.Pow(crossProduct[2], 2)) / 2;

        //Prints out the area
        Console.WriteLine("Area:");
        Console.WriteLine(SigDigRounder(area, 4));

        //Find the distance of the third point to the line if the triangle is 2D
        if (matrix[2, 0] == 1 && matrix[2, 1] == 1 && matrix[2, 2] == 1)
        {
            //Isolate the point on the line
            float[] point = new float[] { matrix[0, 0], matrix[1, 0] };

            //Make vector 1 into a 1d vector
            float[] vector1Transposed = new float[] { vector1[0], vector1[1] };

            //Find t (from equation t = v dot w divided by magnitude of v squared
            float t = (vector1Transposed[0] * vector2[0] + vector1Transposed[1] * vector2[1]) / (MathF.Pow(vector1Transposed[0], 2) + MathF.Pow(vector1Transposed[1], 2));

            //Find the foot of the point
            float[] foot = AddArrays(point, MulitplyScalar(vector1Transposed, t));

            //Find the vector that goes from the foot to the third point
            float[] distanceVector = new float[] { foot[0] - matrix[0, 2], foot[1] - matrix[1, 2] };

            //Find the magnitude of the vector (which will be the distance)
            float distance = MathF.Sqrt(MathF.Pow(distanceVector[0], 2) + MathF.Pow(distanceVector[1], 2));

            //Print the distance of the point to the line
            Console.WriteLine("Distance of 3rd point to line:");
            Console.WriteLine(SigDigRounder(distance, 4));
        }
        else
        {
            //Isolate point 1, point 2, and point 3
            float[] point1 = new float[] { matrix[0, 0], matrix[1, 0], matrix[2, 0] };
            float[] point2 = new float[] { matrix[0, 1], matrix[1, 1], matrix[2, 1] };
            float[] point3 = new float[] { matrix[0, 2], matrix[1, 2], matrix[2, 2] };

            //Find the midpoint of point 1 and point 2
            float[] midpoint = MulitplyScalar(AddArrays(point1, point2), 0.5f);

            //Find the vector from the midpoint to point 3
            float[] vector3 = new float[] { midpoint[0] - point3[0], midpoint[1] - point3[1], midpoint[2] - point3[2] };

            //Find the distance of point 3 to the plane defined by vector 1 and the midpoint
            //Uses the equation distance = |vector 1 (n) dot vector 3| / ||vector 1||
            float distance = MathF.Abs(vector1[0] * vector3[0] + vector1[1] * vector3[1] + vector1[2] * vector3[2]) /
                MathF.Sqrt(MathF.Pow(vector1[0], 2) + MathF.Pow(vector1[1], 2) + MathF.Pow(vector1[2], 2));

            //Print the distance of the point to the plane
            Console.WriteLine("Distance of the point to the plane that bisects the first 2 points:");
            Console.WriteLine(SigDigRounder(distance, 4));
        }
    }

    #endregion

    #region Matrix operations

    /// <summary>
    /// Multiplies a 1D vector by a scalar
    /// </summary>
    /// <param name="vector">Vector to be multiplied</param>
    /// <param name="scalar">scalar to mulptiply</param>
    /// <returns>new 1D vector</returns>
    public static float[] MulitplyScalar(float[] vector, float scalar)
    {
        float[] newArray = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            newArray[i] = vector[i] * scalar;
        }
        return newArray;
    }

    /// <summary>
    /// Adds 2 equal size 1D arrays together
    /// </summary>
    /// <param name="a">first array</param>
    /// <param name="b">second array</param>
    /// <returns>the combined array</returns>
    public static float[] AddArrays(float[] a, float[] b)
    {
        float[] newArray = new float[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            newArray[i] = a[i] + b[i];
        }
        return newArray;
    }

    /// <summary>
    /// Multiplies 2 2D matrices together. Matrix 1 columns must equal matrix 2 rows
    /// </summary>
    /// <param name="m1">1st matrix</param>
    /// <param name="m2">2nd matrix</param>
    /// <returns></returns>
    public static float[,] MatrixMultiplier2D(float[,] m1, float[,] m2)
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

    #endregion

    #region Other methods

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
    /// Rounds a number to a certain amount of signinificant digits
    /// </summary>
    /// <param name="number">number to round</param>
    /// <param name="numSigDig">number of significant digits</param>
    /// <returns>rounded number</returns>
    /// Taken from Stack Overflow from a "P Daddy"
    public static float SigDigRounder(float number, int numSigDig)
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
    public static float CanRoundToZero(float number)
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

    #endregion

}
