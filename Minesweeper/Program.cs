namespace Minesweeper
{
    using System;
    using System.Collections.Generic;

    public class Minesweeper
    {
        private const int Maks = 35;

        private static void Main(string[] args)
        {
            string command = string.Empty;
            char[,] playingField = CreatingPlayingField();
            char[,] bombsPlanted = PlantingTheBombs();
            int pointsCounter = 0;
            bool isBomb = false;
            List<Player> playersScore = new List<Player>(6);
            int row = 0;
            int column = 0;
            bool flag = true;
            bool flag2 = false;

            do
            {
                if (flag)
                {
                    Console.WriteLine(
                        "Hajde da igraem na “Mini4KI”. Probvaj si kasmeta da otkriesh poleteta bez mini4ki."
                        + " Komanda 'top' pokazva klasiraneto, 'restart' po4va nova igra, 'exit' izliza i hajde 4ao!");
                    BoardFieldFramework(playingField);
                    flag = false;
                }

                Console.Write("Choose row and colum : ");

                string readLine = Console.ReadLine();
                if (readLine != null)
                {
                    command = readLine.Trim();
                }

                if (command.Length >= 3)
                {
                    if (int.TryParse(command[0].ToString(), out row) && int.TryParse(command[2].ToString(), out column)
                        && row <= playingField.GetLength(0) && column <= playingField.GetLength(1))
                    {
                        command = "turn";
                    }
                }

                switch (command)
                {
                    case "top":
                        RankList(playersScore);
                        break;
                    case "restart":
                        playingField = CreatingPlayingField();
                        bombsPlanted = PlantingTheBombs();
                        BoardFieldFramework(playingField);
                        break;
                    case "exit":
                        Console.WriteLine("Bye, Bye, Bye!");
                        Console.WriteLine("Made in Bulgaria - Uauahahahahaha!");
                        Console.WriteLine("AREEEEEEeeeeeee.");
                        Environment.Exit(0);
                        break;
                    case "turn":
                        if (bombsPlanted[row, column] != '*')
                        {
                            if (bombsPlanted[row, column] == '-')
                            {
                                YourTurn(playingField, bombsPlanted, row, column);
                                pointsCounter++;
                            }

                            if (Maks == pointsCounter)
                            {
                                flag2 = true;
                            }
                            else
                            {
                                BoardFieldFramework(playingField);
                            }
                        }
                        else
                        {
                            isBomb = true;
                            Console.WriteLine("AAAAAAAAAAAAAAA");
                            Console.WriteLine("Daj si imeto, batka: ");
                        }
                        break;
                    default:
                        Console.WriteLine("\nGreshka! nevalidna Komanda\n");
                        break;
                }

                if (isBomb)
                {
                    BoardFieldFramework(bombsPlanted);
                    string nickName = Console.ReadLine();
                    Console.Write("\nHrrrrrr! You died like a hero with {0} points. " + " Nickname: {1} ", pointsCounter, nickName);
                    Player playerResult = new Player(nickName, pointsCounter);
                    if (playersScore.Count < 5)
                    {
                        playersScore.Add(playerResult);
                    }
                    else
                    {
                        for (int i = 0; i < playersScore.Count; i++)
                        {
                            if (playersScore[i].Points < playerResult.Points)
                            {
                                playersScore.Insert(i, playerResult);
                                playersScore.RemoveAt(playersScore.Count - 1);
                                break;
                            }
                        }
                    }

                    playersScore.Sort((Player r1, Player r2) => r2.Name.CompareTo(r1.Name));
                    playersScore.Sort((Player r1, Player r2) => r2.Points.CompareTo(r1.Points));
                    RankList(playersScore);

                    playingField = CreatingPlayingField();
                    bombsPlanted = PlantingTheBombs();
                    pointsCounter = 0;
                    isBomb = false;
                    flag = true;
                }

                if (flag2)
                {
                    Console.WriteLine("\nBRAVOOOS! Otvri 35 kletki bez kapka kryv.");
                    BoardFieldFramework(bombsPlanted);
                    string nickName = Console.ReadLine();
                    Player score = new Player(nickName, pointsCounter);
                    playersScore.Add(score);
                    RankList(playersScore);
                    playingField = CreatingPlayingField();
                    bombsPlanted = PlantingTheBombs();
                    pointsCounter = 0;
                    flag2 = false;
                    flag = true;
                }
            }

            while (command != "exit");
            {
                Console.Read();
            }

        }

        private static void RankList(List<Player> scores)
        {
            Console.WriteLine("\nPoints:" + scores.Count);
            if (scores.Count > 0)
            {
                for (int i = 0; i < scores.Count; i++)
                {
                    Console.WriteLine("{0}. {1} --> {2} kutii", i + 1, scores[i].Name, scores[i].Points);
                }

                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Empty Rank List!\n");
            }
        }

        private static void YourTurn(char[,] field, char[,] bombs, int row, int colum)
        {
            char howManyBombs = DestroyedCells(bombs, row, colum);
            bombs[row, colum] = howManyBombs;
            field[row, colum] = howManyBombs;
        }

        private static void BoardFieldFramework(char[,] board)
        {
            int rowsLenght = board.GetLength(0);
            int columsLenght = board.GetLength(1);
            Console.WriteLine("\n    0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");
            for (int i = 0; i < rowsLenght; i++)
            {
                Console.Write("{0} | ", i);
                for (int j = 0; j < columsLenght; j++)
                {
                    Console.Write($"{board[i, j]} ");
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.WriteLine("   ---------------------\n");
        }

        private static char[,] CreatingPlayingField()
        {
            int boardRows = 5;
            int boardColumns = 10;
            char[,] board = new char[boardRows, boardColumns];
            for (int i = 0; i < boardRows; i++)
            {
                for (int j = 0; j < boardColumns; j++)
                {
                    board[i, j] = '?';
                }
            }

            return board;
        }

        private static char[,] PlantingTheBombs()
        {
            int rows = 5;
            int colums = 10;
            char[,] playingField = new char[rows, colums];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < colums; j++)
                {
                    playingField[i, j] = '-';
                }
            }

            List<int> listOfBombs = new List<int>();
            while (listOfBombs.Count < 15)
            {
                Random random = new Random();
                int key = random.Next(50);
                if (!listOfBombs.Contains(key))
                {
                    listOfBombs.Add(key);
                }
            }

            foreach (var i2 in listOfBombs)
            {
                int col = i2 / colums;
                int row = i2 % colums;
                if (row == 0 && i2 != 0)
                {
                    col--;
                    row = colums;
                }
                else
                {
                    row++;
                }

                playingField[col, row - 1] = '*';
            }

            return playingField;
        }

        private static void Calculations(char[,] playingField)
        {
            int row = playingField.GetLength(0);
            int column = playingField.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    if (playingField[i, j] != '*')
                    {
                        char damage = DestroyedCells(playingField, i, j);
                        playingField[i, j] = damage;
                    }
                }
            }
        }

        private static char DestroyedCells(char[,] matrix, int row, int column)
        {
            int counter = 0;
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            if (row - 1 >= 0)
            {
                if (matrix[row - 1, column] == '*')
                {
                    counter++;
                }
            }

            if (row + 1 < rows)
            {
                if (matrix[row + 1, column] == '*')
                {
                    counter++;
                }
            }

            if (column - 1 >= 0)
            {
                if (matrix[row, column - 1] == '*')
                {
                    counter++;
                }
            }

            if (column + 1 < columns)
            {
                if (matrix[row, column + 1] == '*')
                {
                    counter++;
                }
            }

            if ((row - 1 >= 0) && (column - 1 >= 0))
            {
                if (matrix[row - 1, column - 1] == '*')
                {
                    counter++;
                }
            }

            if ((row - 1 >= 0) && (column + 1 < columns))
            {
                if (matrix[row - 1, column + 1] == '*')
                {
                    counter++;
                }
            }

            if ((row + 1 < rows) && (column - 1 >= 0))
            {
                if (matrix[row + 1, column - 1] == '*')
                {
                    counter++;
                }
            }

            if ((row + 1 < rows) && (column + 1 < columns))
            {
                if (matrix[row + 1, column + 1] == '*')
                {
                    counter++;
                }
            }

            return char.Parse(counter.ToString());
        }
    }
}