using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private VoidEvent boardGenerated;

    [SerializeField] private List<Level> levels;
    [SerializeReference] private int initialLevel = 0;
    public float spacing = 2f;
    private Vector2 initialPosition;

    private Tile[,] board;
    private int boardSize;

    private void Start()
    {
        GenerateBoard(levels[initialLevel]);
    }

    private void GenerateBoard(Level level)
    {
        boardSize = level.BoardSize;

        float tileScale = level.levelTilesToGenerate[0].tile.transform.localScale.x;
        float boardArea = tileScale * boardSize + spacing * (boardSize - 1);
        initialPosition = new Vector2(this.transform.position.x + boardArea / 4, this.transform.position.y + boardArea / 4);

        board = new Tile[boardSize, boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Tile tile = level.levelTilesToGenerate[i + (j * boardSize)].tile;
                if (tile == null) continue;

                tile.tilePosition = GetTilePosition(i, j);

                Tile tileObject = Instantiate(tile, tile.tilePosition, Quaternion.identity, this.transform);

                if (level.levelTilesToGenerate[i + (j * boardSize)].tileComponent != null)
                {
                    CatMovement catMovement = level.levelTilesToGenerate[i + (j * boardSize)].tileComponent.GetComponent<CatMovement>();
                    if (catMovement != null)
                        catMovement.catPosition = new Vector2Int(i, j);

                    tileObject.tileComponent = Instantiate(level.levelTilesToGenerate[i + (j * boardSize)].tileComponent, tile.tilePosition, Quaternion.identity, tileObject.transform);
                }

                board[i, j] = tileObject;
            }
        }

        boardGenerated.Raise(Unit.Default);
    }

    private Vector2 GetTilePosition(int x, int y)
    {
        return new Vector2(x * spacing - initialPosition.x, -y * spacing + initialPosition.y);
    }

    public Vector2 CatMove(ref Vector2Int catPosition, Vector2 moveDirection)
    {
        Vector2Int newCatPosition = catPosition + new Vector2Int(Mathf.FloorToInt(moveDirection.x), -Mathf.FloorToInt(moveDirection.y));

        // if x or y are -1 or x or y are boardSize + 1
        if (newCatPosition.x <= -1 ||
            newCatPosition.y <= -1 ||
            newCatPosition.x >= boardSize ||
            newCatPosition.y >= boardSize ||
            board[newCatPosition.x, newCatPosition.y].tileComponent != null)
            return -Vector2.one;

        GameObject cat = board[catPosition.x, catPosition.y].tileComponent;
        board[newCatPosition.x, newCatPosition.y].tileComponent = cat;
        board[catPosition.x, catPosition.y].tileComponent = null;
        cat.transform.SetParent(board[newCatPosition.x, newCatPosition.y].transform);

        catPosition = newCatPosition;
        return GetTilePosition(newCatPosition.x, newCatPosition.y);
    }

    public void TriggerTile(int x, int y)
    {
       // if (board[x, y].tileComponent as BlockBase != null) {
    }
}
