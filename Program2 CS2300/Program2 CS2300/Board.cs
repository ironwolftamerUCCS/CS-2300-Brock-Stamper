using System;
using System.Security.Cryptography.X509Certificates;

public class Board
{
    #region Fields

    int[,] board = new int[3, 3];

    #endregion

    #region Constructor

    // Construct a board with NxN dimensions

    #endregion

    #region Properties

    #endregion

    #region Private methods

    /// <summary>
    /// To check the line can be played
    /// </summary>
    private void CheckLine()
    {
        // Check if the line is perpendicular to one already played
        // Check if the line has start or end points no longer allowed
    }

    #endregion

    #region Public methods

    /// <summary>
    /// To play a line on the game board
    /// </summary>
    public void PlayLine()
    {
        // Checks the line
        // If valid, plays the line
        // If invalid, throws error message
    }

    #endregion
}
