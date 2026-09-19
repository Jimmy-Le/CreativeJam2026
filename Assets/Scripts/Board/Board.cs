using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeReference] private int initialLevel = 0;

    private Tile[,] board;

    private void Start()
    {
        GenerateBoard(levels[initialLevel]);
    }

    private void GenerateBoard(Level level)
    {
        board = new Tile[level.BoardSize, level.BoardSize];

        for (int i = 0; i < level.BoardSize; i++)
        {
            for (int j = 0; j < level.BoardSize; j++)
            {
                board[i, j] = level.levelTilesToGenerate[i + (j * level.BoardSize)];
            }
        }
    }
}
