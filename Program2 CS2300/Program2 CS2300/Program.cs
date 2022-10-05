using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;

namespace Program2_CS2300
{
    internal class Program
    {
        // Got help from Kaylee on this. About 10-20% (she gave me the idea to make a line class and helped with some of the logic)
        static void Main(string[] args)
        {
            // Prompt for file being tested
            Console.Write("Please enter the path for the file you would like to play: ");
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
                Board board = new Board(boardSize, turnsToRemember);

                board.PlayFile(inputFileAsList);
            }
        }
    }
}