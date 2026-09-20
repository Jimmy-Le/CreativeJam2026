using PrimeTween;
using System.Collections.Generic;
using UnityEngine;
public class Board : MonoBehaviour
{
    [SerializeField] public List<Level> levels;
    [SerializeReference] public int initialLevel = 0;
    [SerializeField] private VoidEvent LevelCompleteEvent;
    public int currentLevel = 0;
    public float spacing = 2f;
    public Vector2 initialPosition;

    [SerializeField] public GameObject loadingScreen;
    public Tile[,] board;
    public int boardSize;

    private void Start()
    {
        GenerateBoard(levels[initialLevel]);
        currentLevel = initialLevel;
    }

    void OnEnable()
    {
        LevelCompleteEvent.OnEventRaised += NextLevel;
    }

    void OnDisable()
    {
        LevelCompleteEvent.OnEventRaised -= NextLevel;
    }

    public void NextLevel(Unit data)
    {
        currentLevel++;

        if (currentLevel < levels.Count)
        {
            loadingScreen.SetActive(true);
            GameUIScript.Instance.LoadLevel(currentLevel);
            SoundManager.instance.audioSource.Stop();
            Tween.Delay(duration: 1f, onComplete: () =>
            {
                GameUIScript.Instance.RestartLevel();
                GameUIScript.Instance.DisplayStepsLeft();
                loadingScreen.SetActive(false);
               
            });

            //GameUIScript.Instance.RestartLevel();
        }
            
        else
            Debug.Log("GameOver");


    }

    public void GenerateBoard(Level level)
    {

        for (int i = this.gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
        }

        boardSize = level.BoardSize;

        float tileScale = level.levelTilesToGenerate[0].tile.transform.localScale.x;
        float boardArea = tileScale * boardSize;
        initialPosition = new Vector2(this.transform.position.x + boardArea / 2f - 0.5f, this.transform.position.y + boardArea / 2f - 0.5f);
        Debug.Log(initialPosition);


        board = new Tile[boardSize, boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                Tile tile = level.levelTilesToGenerate[i + (j * boardSize)].tile.GetComponent<Tile>();
                if (tile == null) continue;

                tile.tileIndex = new Vector2Int(i, j);
                tile.tilePosition = GetTilePosition(i, j);

                Tile tileObject = Instantiate(tile, tile.tilePosition, Quaternion.identity, this.transform);

                if (level.levelTilesToGenerate[i + (j * boardSize)].tileComponent != null)
                {
                    CatMovement catMovement = level.levelTilesToGenerate[i + (j * boardSize)].tileComponent.GetComponent<CatMovement>();
                    if (catMovement != null)
                    {
                        catMovement.catPosition = new Vector2Int(i, j);
                        catMovement.stepCounter = level.stepsAllowed;
                    }

                    tileObject.tileComponent = Instantiate(level.levelTilesToGenerate[i + (j * boardSize)].tileComponent, tile.tilePosition, Quaternion.identity, tileObject.transform);
                }

                board[i, j] = tileObject;
            }
        }

    }

    public Vector2 GetTilePosition(int x, int y)
    {
        return new Vector2(x * spacing - initialPosition.x, -y * spacing + initialPosition.y);
    }

    public Vector2 CatMove(ref Vector2Int catPosition, Vector2 moveDirection, ref bool isBoosted)
    {
        Vector2Int newCatPosition;

        if (isBoosted)
        {
            newCatPosition = catPosition + new Vector2Int(Mathf.FloorToInt(moveDirection.x) * 2, -Mathf.FloorToInt(moveDirection.y) * 2);
            isBoosted = false;
        }
        else
            newCatPosition = catPosition + new Vector2Int(Mathf.FloorToInt(moveDirection.x), -Mathf.FloorToInt(moveDirection.y));

        // if x or y are -1 or x or y are boardSize + 1
        if (newCatPosition.x <= -1 ||
            newCatPosition.y <= -1 ||
            newCatPosition.x >= boardSize ||
            newCatPosition.y >= boardSize ||
            board[newCatPosition.x, newCatPosition.y].tileComponent != null)
            return new Vector2(1000, 1000);
        Debug.Log("herhehe");
        GameObject cat = board[catPosition.x, catPosition.y].tileComponent;
        board[newCatPosition.x, newCatPosition.y].tileComponent = cat;
        board[catPosition.x, catPosition.y].tileComponent = null;
        cat.transform.SetParent(board[newCatPosition.x, newCatPosition.y].transform);

        catPosition = newCatPosition;
        
        return GetTilePosition(newCatPosition.x, newCatPosition.y);
    }

    public void TriggerBoardAtPos(Vector2Int pos)
    {
        board[pos.x, pos.y].OnStep();
    }

    public void MoveBlock(Vector2Int blockPosition, Vector2 moveDirection)
    {
        Vector2Int newBlockPosition = blockPosition + new Vector2Int(Mathf.FloorToInt(moveDirection.x), -Mathf.FloorToInt(moveDirection.y));

        // if x or y are -1 or x or y are boardSize + 1
        if (newBlockPosition.x <= -1 ||
            newBlockPosition.y <= -1 ||
            newBlockPosition.x >= boardSize ||
            newBlockPosition.y >= boardSize ||
            board[newBlockPosition.x, newBlockPosition.y].tileComponent != null ||
            board[newBlockPosition.x, newBlockPosition.y].gameObject.CompareTag("MoveBlockBan"))
                return;

        GameObject block = board[blockPosition.x, blockPosition.y].tileComponent;
        board[newBlockPosition.x, newBlockPosition.y].tileComponent = block;
        board[blockPosition.x, blockPosition.y].tileComponent = null;
        block.transform.SetParent(board[newBlockPosition.x, newBlockPosition.y].transform);
        block.transform.position = GetTilePosition(newBlockPosition.x, newBlockPosition.y);

        board[newBlockPosition.x, newBlockPosition.y].OnStep();
    }

    public void Teleport(TeleportTile initialTile, TeleportTile destinationTile)
    {
        GameObject tpItem = initialTile.tileComponent;
        CatMovement catMovement = tpItem.GetComponent<CatMovement>();
        
        if (catMovement == null) return;

        board[destinationTile.tileIndex.x, destinationTile.tileIndex.y].tileComponent = tpItem;
        board[initialTile.tileIndex.x, initialTile.tileIndex.y].tileComponent = null;
        tpItem.transform.SetParent(board[destinationTile.tileIndex.x, destinationTile.tileIndex.y].transform);
        tpItem.transform.position = destinationTile.tilePosition;

        Debug.Log($"{destinationTile.tileIndex.x}, {destinationTile.tileIndex.y}");

        catMovement.catPosition = new Vector2Int(destinationTile.tileIndex.x, destinationTile.tileIndex.y);
        catMovement.catBody.position = destinationTile.tilePosition;
        Debug.Log($"{catMovement.catPosition}, {catMovement.catBody.position}");
    }

    public void TriggerTile(int x, int y, Vector2Int direction)
    {
        if (x <= -1 || y <= -1 || x >= boardSize || y >= boardSize || board[x, y].tileComponent == null)
            return;

        BlockBase blockBase = board[x, y].tileComponent.GetComponent<BlockBase>();
        if (blockBase != null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("blockBase", blockBase);
            data.Add("board", this);
            data.Add("blockPosition", new Vector2Int(x, y));
            data.Add("direction", direction);

            blockBase.ability.Activate(data);
        }
    }

    public void CatCleanUp(Vector2Int oldPosition, Vector2Int startPosition)
    {
        GameObject cat = board[oldPosition.x, oldPosition.y].tileComponent;
        //board[startPosition.x, startPosition.y].tileComponent = cat;          // Moved This Down
        board[oldPosition.x, oldPosition.y].tileComponent = null;
        board[startPosition.x, startPosition.y].tileComponent = cat;
    }

    public bool CheckIfDoor(Vector2Int playerPos)
    {
       if(board[playerPos.x, playerPos.y].GetComponent<DoorTile>() != null)
        {
            return true;
        }

       return false;
    }
}
