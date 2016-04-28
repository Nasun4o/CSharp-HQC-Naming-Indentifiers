namespace MatrixMultiplicator
{
    using System;

    public class MatrixMultipliction
    {
        static void Main(string[] args)
        {
            var firstMatrix = new double[,] { { 1, 3 }, { 5, 7 } };
            var secondMatrix = new double[,] { { 4, 2 }, { 1, 5 } };
            var sumOfMatrix = MatrixSumCalculator(firstMatrix, secondMatrix);

            for (int row = 0; row < sumOfMatrix.GetLength(0); row++)
            {
                for (int col = 0; col < sumOfMatrix.GetLength(1); col++)
                {
                    Console.Write(sumOfMatrix[row, col] + " ");
                }
                Console.WriteLine();
            }

        }

       private static double[,] MatrixSumCalculator(double[,] firstMatrix, double[,] secondMatrix)
        {
            if (firstMatrix.GetLength(1) != secondMatrix.GetLength(0))
            {
                throw new Exception("Error!");
            }

            var colsLenght = firstMatrix.GetLength(1);
            var result = new double[firstMatrix.GetLength(0), secondMatrix.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
            {
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    for (int k = 0; k < colsLenght; k++)
                    {
                        result[row, col] += firstMatrix[row, k] * secondMatrix[k, col];
                    }
                }
            }
            return result;
        }
    }
}