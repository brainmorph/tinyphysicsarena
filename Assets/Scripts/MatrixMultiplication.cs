using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatrixMultiplication : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void Main(string[] args)
    {
        int[,] matrixA = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        int[,] matrixB = new int[,] { { 1 }, { 2 }, { 3 } };

        int[,] result = MultiplyMatrices(matrixA, matrixB);

        Console.WriteLine("Resulting matrix:");
        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine("{0} {1} {2}", result[i, 0], result[i, 1], result[i, 2]);
        }

        Console.ReadLine();
    }

    static int[,] MultiplyMatrices(int[,] matrixA, int[,] matrixB)
    {
        int[,] result = new int[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int sum = 0;
                for (int k = 0; k < 3; k++)
                {
                    sum += matrixA[i, k] * matrixB[k, j];
                }
                result[i, j] = sum;
            }
        }
        return result;
    }
}
