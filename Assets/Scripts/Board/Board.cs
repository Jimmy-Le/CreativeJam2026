using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeReference] private int initialLevel = 0;
    public float spacing = 2f;
    private Vector2 initialPosition;

    private Tile[,] board;

    private void Start()
    {
        GenerateBoard(levels[initialLevel]);
    }

    private void GenerateBoard(Level level)
    {
        float tileScale = level.levelTilesToGenerate[0].transform.localScale.x;
        float boardArea = tileScale * level.BoardSize + spacing * (level.BoardSize - 1);

        initialPosition = new Vector2(this.transform.position.x + boardArea / 4, this.transform.position.y + boardArea / 4);

        Debug.Log($"{tileScale}, {boardArea}, {initialPosition}");

        board = new Tile[level.BoardSize, level.BoardSize];

        for (int i = 0; i < level.BoardSize; i++)
        {
            for (int j = 0; j < level.BoardSize; j++)
            {
                board[i, j] = level.levelTilesToGenerate[i + (j * level.BoardSize)];

                if (board[i, j] == null) continue;

                Vector2 tilePosition = new Vector2(i * spacing - initialPosition.x, -j * spacing + initialPosition.y);
                Instantiate(level.levelTilesToGenerate[i + (j * level.BoardSize)], tilePosition, Quaternion.identity, this.transform);
            }
        }
    }
}
