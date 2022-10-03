using System;
using System.Collections.Generic;
using System.Security.Cryptography;

/// <summary>
/// A game board
/// </summary>
public class Board
{
    #region Fields

    // Game board
    private char[,] board;

    // # of turns to remember
    private int turnsToRemember;

    #endregion

    #region Constructor

    /// <summary>
    /// Construct a square board with the given dimension
    /// </summary>
    /// <param name="dimension">dimensions of the board</param>
    /// <param name="turnsToRemember"># of turns to remember</param>
    public Board (int dimension, int turnsToRemember)
    {
        // Set board field
        board = new char[dimension, dimension];

        // Fill in the board with '.'
        int row = 0;
        int col = 0;
        while (row < board.GetLength(0))
        {
            while (col < board.GetLength(1))
            {
                board[row, col] = '.';
                col++;
            }
            row++;
            col = 0;
        }

        // Set turnsToRemember field
        this.turnsToRemember = turnsToRemember;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Prints the board
    /// </summary>
    public void PrintBoard()
    {
        // Loop support
        int row = 0;
        int col = 0;

        // Print each cell in the board
        while (row < board.GetLength(0))
        {
            while (col < board.GetLength(1))
            {
                Console.Write(board[row, col]);
                col++;
            }
            Console.WriteLine();
            row++;
            col = 0;
        }
    }

    /// <summary>
    /// Plays the input file and prints the board after every successful play
    /// </summary>
    /// <param name="fileAsList"></param>
    public void PlayFile(List<string> fileAsList)
    {
        // Split the lines into P1 and P2 and play them accordingly
        int index = 1;
        while (index < fileAsList.Count)
        {
            // Keep track of what player is playing
            int player = 0;

            // Seperate the line at index into its coordinates (startRow, startColumn, endRow, endColumn)
            string lineBeingPlayed = fileAsList[index];
            string[] lineSubstrings = lineBeingPlayed.Split(' ');
            int startRow = int.Parse(lineSubstrings[0]);
            int startColumn = int.Parse(lineSubstrings[1]);
            int endRow = int.Parse(lineSubstrings[2]);
            int endColumn = int.Parse(lineSubstrings[3]);
            Line line = new Line(startRow, startColumn, endRow, endColumn);

            // Check if index is even or odd
            // If odd, then player one is playing
            if (index % 2 != 0)
            {
                player = 1;
            }
            // Else index is even, so player two is playing
            else
            {
                player = 2;
            }

            // Play the line
            PlayLine(player, line);

            PrintBoard();

            Console.WriteLine();

            index++;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Plays the line on the board (if possible)
    /// </summary>
    /// <param name="player">player who is playing the line</param>
    /// <param name="line">line to be played</param>
    private void PlayLine(int player, Line line)
    {
        // Set which player is playing and the corresponding symbol to use
        Char playerSymbol = '.';

        if (player == 1)
        {
            playerSymbol = 'X';
        }
        else if (player == 2)
        {
            playerSymbol = 'O';
        }

        // First check if the line is valid to play
        if (CheckLine(line))
        {
            // Check what the absolute value of the slope is
            if (line.AbsoluteValueOfSlope == -1)
            {
                // Since the line is undefined, just plot the corresponding row points
                // Find which rows to change
                float target = line.EndRow - line.StartRow;
                float currentRow = line.StartRow;

                // Change each necessary row
                while (currentRow <= target)
                {
                    FlipCell(currentRow, line.StartColumn, playerSymbol);
                    currentRow++;
                }
            }
            else if (line.AbsoluteValueOfSlope == 0)
            {
                // Since the line is a straigh line, just plot the corresponding column points
                float target = line.EndColumn - line.StartColumn;
                float currentColumn = line.StartColumn;

                // Change each necessary column
                while (currentColumn <= target)
                {
                    FlipCell(line.StartRow, currentColumn, playerSymbol);
                    currentColumn++;
                }
            }
            else if (line.AbsoluteValueOfSlope < 1)
            {
                // Check the points to the right and left of the line to plot
                // Set up for while loop
                float index;

                float rowPoint;
                float leftCellRow;
                float rightCellRow;
                float[] p = new float[2];

                float[] r1 = new float[2];
                float[] w1 = new float[2];
                float t1;
                float[] q1 = new float[2];
                float d1;

                float[] r2 = new float[2];
                float[] w2 = new float[2];
                float t2;
                float[] q2 = new float[2];
                float d2;

                // Check if line goes bottom to top or top to bottom
                if (line.StartColumn < line.EndColumn)
                {
                    index = line.StartColumn;

                    // Flip the appropriate top or bottom point (if they are an equal distance, use the top one)
                    while (index <= line.EndColumn)
                    {
                        // Find the row of the current column and set the potential cells to fill in (essentially finding r)
                        rowPoint = line.FindRowPoint(index);
                        leftCellRow = (float)Math.Floor(rowPoint);      //R1Row
                        rightCellRow = (float)Math.Ceiling(rowPoint);   //R2Row

                        // For finding some things below
                        p[0] = rowPoint;
                        p[1] = index;

                        // Find the distance of the top point's foot to the point on the line
                        // Find r
                        r1[0] = leftCellRow;
                        r1[1] = index;
                        // Find w (r - minus the point on the line (index, rowPoint))
                        w1 = SubtractArrays(r1, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t1 = (line.ParametricV[0] * w1[0] + line.ParametricV[1] * w1[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q1 = AddArrays(p, MulitplyScalar(line.ParametricV, t1));
                        // Find distance between p and q
                        d1 = (float)Math.Sqrt((Math.Pow(p[0] - q1[0], 2) + Math.Pow(p[1] - q1[1], 2)));

                        // Find the distance of the bottom point's foot to the point on the line
                        // Find r
                        r2[0] = rightCellRow;
                        r2[1] = index;
                        // Find w (r - minus the point on the line (rowPoint, index))
                        w2 = SubtractArrays(r2, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t2 = (line.ParametricV[0] * w2[0] + line.ParametricV[1] * w2[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q2 = AddArrays(p, MulitplyScalar(line.ParametricV, t2));
                        // Find distance between p and q
                        d2 = (float)Math.Sqrt((Math.Pow(q2[0] - p[0], 2) + Math.Pow(q2[1] - p[1], 2)));

                        // Compare the distances and fill the appropriate cell
                        if (d2 < d1)
                        {
                            FlipCell(r2[0], r2[1], playerSymbol);
                        }
                        else
                        {
                            FlipCell(r1[0], r1[1], playerSymbol);
                        }

                        index++;
                    }
                }
                else
                {
                    index = line.StartColumn;

                    // Flip the appropriate left or right point (if they are an equal distance, use the left one)
                    while (index >= line.EndColumn)
                    {
                        // Find the row of the current column and set the potential cells to fill in (essentially finding r)
                        rowPoint = line.FindRowPoint(index);
                        leftCellRow = (float)Math.Floor(rowPoint);      //R1Row
                        rightCellRow = (float)Math.Ceiling(rowPoint);   //R2Row

                        // For finding some things below
                        p[0] = rowPoint;
                        p[1] = index;

                        // Find the distance of the top point's foot to the point on the line
                        // Find r
                        r1[0] = leftCellRow;
                        r1[1] = index;
                        // Find w (r - minus the point on the line (index, rowPoint))
                        w1 = SubtractArrays(r1, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t1 = (line.ParametricV[0] * w1[0] + line.ParametricV[1] * w1[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q1 = AddArrays(p, MulitplyScalar(line.ParametricV, t1));
                        // Find distance between p and q
                        d1 = (float)Math.Sqrt((Math.Pow(p[0] - q1[0], 2) + Math.Pow(p[1] - q1[1], 2)));

                        // Find the distance of the bottom point's foot to the point on the line
                        // Find r
                        r2[0] = rightCellRow;
                        r2[1] = index;
                        // Find w (r - minus the point on the line (rowPoint, index))
                        w2 = SubtractArrays(r2, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t2 = (line.ParametricV[0] * w2[0] + line.ParametricV[1] * w2[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q2 = AddArrays(p, MulitplyScalar(line.ParametricV, t2));
                        // Find distance between p and q
                        d2 = (float)Math.Sqrt((Math.Pow(q2[0] - p[0], 2) + Math.Pow(q2[1] - p[1], 2)));

                        // Compare the distances and fill the appropriate cell
                        if (d2 < d1)
                        {
                            FlipCell(r2[0], r2[1], playerSymbol);
                        }
                        else
                        {
                            FlipCell(r1[0], r1[1], playerSymbol);
                        }

                        index--;
                    }
                }
            }
            else if (line.AbsoluteValueOfSlope == 1)
            {
                // Since the line will pass through each point we need to plot, just find said points
                // Find how many rows the line passes through then how many columns it passes through then flip each row, col
                float currentRow = line.StartRow;
                float currentColumn = line.StartColumn;

                // Check if the line goes top left to bottom left or top right to bottom right
                if (line.StartRow < line.EndRow)
                {
                    while (currentRow <= line.EndRow && currentColumn <= line.EndColumn)
                    {
                        FlipCell(currentRow, currentColumn, playerSymbol);

                        currentRow++;
                        currentColumn++;
                    }
                }
                else
                {
                    while (currentRow >= line.EndRow && currentColumn <= line.EndColumn)
                    {
                        FlipCell(currentRow, currentColumn, playerSymbol);

                        currentRow--;
                        currentColumn++;
                    }
                }
            }
            else if (line.AbsoluteValueOfSlope > 1)
            {
                // Check the points to the right and left of the line to plot
                // Set up for while loop
                float index;

                float columnPoint;
                float leftCellColumn;
                float rightCellColumn;
                float[] p = new float[2];

                float[] r1 = new float[2];
                float[] w1 = new float[2];
                float t1;
                float[] q1 = new float[2];
                float d1;

                float[] r2 = new float[2];
                float[] w2 = new float[2];
                float t2;
                float[] q2 = new float[2];
                float d2;

                // Check if line goes bottom to top or top to bottom
                if (line.StartRow < line.EndRow)
                {
                    index = line.StartRow;

                    // Flip the appropriate left or right point (if they are an equal distance, use the left one)
                    while (index <= line.EndRow)
                    {
                        // Find the column of the current row and set the potential cells to fill in (essentially finding r)
                        columnPoint = line.FindColumnPoint(index);
                        leftCellColumn = (float)Math.Floor(columnPoint);      //R1Col
                        rightCellColumn = (float)Math.Ceiling(columnPoint);   //R2Col

                        // For finding some things below
                        p[0] = index;
                        p[1] = columnPoint;

                        // Find the distance of the left point's foot to the point on the line
                        // Find r
                        r1[0] = index;
                        r1[1] = leftCellColumn;
                        // Find w (r - minus the point on the line (index, columnPoint))
                        w1 = SubtractArrays(r1, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t1 = (line.ParametricV[0] * w1[0] + line.ParametricV[1] * w1[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q1 = AddArrays(p, MulitplyScalar(line.ParametricV, t1));
                        // Find distance between p and q
                        d1 = (float)Math.Sqrt((Math.Pow(p[0] - q1[0], 2) + Math.Pow(p[1] - q1[1], 2)));

                        // Find the distance of the right point's foot to the point on the line
                        // Find r
                        r2[0] = index;
                        r2[1] = rightCellColumn;
                        // Find w (r - minus the point on the line (index, columnPoint))
                        w2 = SubtractArrays(r2, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t2 = (line.ParametricV[0] * w2[0] + line.ParametricV[1] * w2[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q2 = AddArrays(p, MulitplyScalar(line.ParametricV, t2));
                        // Find distance between p and q
                        d2 = (float)Math.Sqrt((Math.Pow(q2[0] - p[0], 2) + Math.Pow(q2[1] - p[1], 2)));

                        if (d2 < d1)
                        {
                            FlipCell(r2[0], r2[1], playerSymbol);
                        }
                        else
                        {
                            FlipCell(r1[0], r1[1], playerSymbol);
                        }

                        index++;
                    }
                }
                else
                {
                    index = line.StartRow;

                    // Flip the appropriate left or right point (if they are an equal distance, use the left one)
                    while (index >= line.EndRow)
                    {
                        // Find the column of the current row and set the potential cells to fill in (essentially finding r)
                        columnPoint = line.FindColumnPoint(index);
                        leftCellColumn = (float)Math.Floor(columnPoint);      //R1Col
                        rightCellColumn = (float)Math.Ceiling(columnPoint);   //R2Col

                        // For finding some things below
                        p[0] = index;
                        p[1] = columnPoint;

                        // Find the distance of the left point's foot to the point on the line
                        // Find r
                        r1[0] = index;
                        r1[1] = leftCellColumn;
                        // Find w (r - minus the point on the line (index, columnPoint))
                        w1 = SubtractArrays(r1, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t1 = (line.ParametricV[0] * w1[0] + line.ParametricV[1] * w1[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q1 = AddArrays(p, MulitplyScalar(line.ParametricV, t1));
                        // Find distance between p and q
                        d1 = (float)Math.Sqrt((Math.Pow(p[0] - q1[0], 2) + Math.Pow(p[1] - q1[1], 2)));

                        // Find the distance of the right point's foot to the point on the line
                        // Find r
                        r2[0] = index;
                        r2[1] = rightCellColumn;
                        // Find w (r - minus the point on the line (index, columnPoint))
                        w2 = SubtractArrays(r2, p);
                        // Find t using v dot w divided by the squared magnitude of v
                        t2 = (line.ParametricV[0] * w2[0] + line.ParametricV[1] * w2[1]) / (float)((Math.Pow(line.ParametricV[0], 2)) + (Math.Pow(line.ParametricV[1], 2)));
                        // Find q
                        q2 = AddArrays(p, MulitplyScalar(line.ParametricV, t2));
                        // Find distance between p and q
                        d2 = (float)Math.Sqrt((Math.Pow(q2[0] - p[0], 2) + Math.Pow(q2[1] - p[1], 2)));

                        if (d2 < d1)
                        {
                            FlipCell(r2[0], r2[1], playerSymbol);
                        }
                        else
                        {
                            FlipCell(r1[0], r1[1], playerSymbol);
                        }

                        index--;
                    }
                }
            } 
        }
    }

    /// <summary>
    /// Flips the cell of the provided coordinate
    /// </summary>
    /// <param name="row">row of cell</param>
    /// <param name="col">column of cell</param>
    /// <param name="newSymbol">symbol to place into cell</param>
    private void FlipCell(float row, float col, Char newSymbol)
    {
        // Change the provided cell to the new symbol
        board[(int)row, (int)col] = newSymbol;
    }

    /// <summary>
    /// Checks if the given line is valid
    /// </summary>
    /// <param name="line">line that needs to be checked</param>
    /// <returns>true is line can be played, false if line cannot be played</returns>
    private bool CheckLine(Line line)
    {
        return true;
    }

    // Below inspired by Chris on stack overflow

    /// <summary>
    /// Subtracts 2 equal size 1D arrays from each other
    /// </summary>
    /// <param name="a">first array</param>
    /// <param name="b">second array</param>
    /// <returns>the combined array</returns>
    private float[] SubtractArrays(float[] a, float[] b)
    {
        float[] newArray = new float[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            newArray[i] = a[i] - b[i];
        }
        return newArray;
    }

    /// <summary>
    /// Adds 2 equal size 1D arrays together
    /// </summary>
    /// <param name="a">first array</param>
    /// <param name="b">second array</param>
    /// <returns>the combined array</returns>
    private float[] AddArrays(float[] a, float[] b)
    {
        float[] newArray = new float[a.Length];
        for (int i = 0; i < a.Length; i++)
        {
            newArray[i] = a[i] + b[i];
        }
        return newArray;
    }

    private float[] MulitplyScalar(float[] vector, float scalar)
    {
        float[] newArray = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            newArray[i] = vector[i] * scalar;
        }
        return newArray;
    }

    #endregion
}
