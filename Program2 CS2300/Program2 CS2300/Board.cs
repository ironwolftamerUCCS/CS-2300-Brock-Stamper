using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;

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
            int index = 0;

            // Check what the absolute value of the slope is
            if (line.AbsoluteValueOfSlope == -1)
            {
                // Since the line is undefined, just plot the corresponding row points
                // Find which rows to change
                float target = line.EndRow - line.StartRow;

                // Change each necessary row
                while (index <= target)
                {
                    FlipCell(index + 1, line.StartColumn - 1, playerSymbol);
                    index++;
                }
            }
            else if (line.AbsoluteValueOfSlope == 0)
            {
                // Since the line is a straigh line, just plot the corresponding column points
                float target = line.EndColumn - line.StartColumn;

                // Change each necessary column
                while (index <= target)
                {
                    FlipCell(line.StartRow - 1, index + 1, playerSymbol);
                    index++;
                }
            }
            else if (line.AbsoluteValueOfSlope < 1)
            {
                // Check the points above and below the line to plot

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
                        FlipCell(currentRow - 1, currentColumn - 1, playerSymbol);

                        currentRow++;
                        currentColumn++;
                    }
                }
                else
                {
                    while (currentRow >= line.EndRow && currentColumn <= line.EndColumn)
                    {
                        FlipCell(currentRow - 1, currentColumn - 1, playerSymbol);

                        currentRow--;
                        currentColumn++;
                    }
                }
            }
            else if (line.AbsoluteValueOfSlope > 1)
            {
                // Check the points to the right and left of the line
            } 
        }
    }

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

    #endregion
}
