using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Program2_CS2300
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Prompt for file being tested
            string inputFile = Console.ReadLine();

            // Gets the input for the game
            List<string> inputFileAsList = FileReader.FileInputToList(inputFile);

            if (inputFileAsList != null)
            {
                // Gets the line that configures the game board and gameplay
                string gameConfiguration = inputFileAsList[0];

                // Set dimensions of board and K
                string[] gameConfigSubstrings = gameConfiguration.Split(' ');
                int boardSize = int.Parse(gameConfigSubstrings[0]);
                int turnsToRemember = int.Parse(gameConfigSubstrings[1]);

                // Generate the board
                Board board = new Board(boardSize);

                board.PlayFile(inputFileAsList);

                board.PrintBoard();
            }
        }
    }
}
