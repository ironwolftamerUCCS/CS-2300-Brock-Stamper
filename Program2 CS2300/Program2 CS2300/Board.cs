using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;

public class Board
{
    #region Fields

    // Game board
    private char[,] board;

    // # of turns to remember
    private int turnsToRemember;

    #endregion

    #region Properties

    /// <summary>
    /// # of turns to remember
    /// </summary>
    public int TurnsToRemember
    {
        set { turnsToRemember = value; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Construct a square board with the given dimension
    /// </summary>
    /// <param name="dimension">dimensions of the board</param>
    public Board (int dimension)
    {
        board = new char[dimension, dimension];

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
            PlayLine(player, startRow, startColumn, endRow, endColumn);

            index++;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Plays the line on the board (if possible)
    /// </summary>
    /// <param name="player">player who is playing the line</param>
    /// <param name="startRow">row line starts</param>
    /// <param name="startColumn">column line starts</param>
    /// <param name="endRow">row line ends</param>
    /// <param name="endColumn">column line ends</param>
    private void PlayLine(int player, float startRow, float startColumn, float endRow, float endColumn)
    {
        if (CheckLine(startRow, startColumn, endRow, endColumn))
        {
            //do shit
        }
    }

    /// <summary>
    /// Checks if the given line is valid
    /// </summary>
    /// <param name="startRow">row line starts</param>
    /// <param name="startColumn">column line starts</param>
    /// <param name="endRow">row line ends</param>
    /// <param name="endColumn">column line ends</param>
    /// <returns>true is line can be played, false if line cannot be played</returns>
    private bool CheckLine(float startRow, float startColumn, float endRow, float endColumn)
    {
        return true;
    }

    #endregion
}
