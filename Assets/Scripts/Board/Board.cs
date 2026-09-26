using PrimeTween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Board : MonoBehaviour
{
    #region Editor Fields
    [Header("Levels")]
    [SerializeField] public List<Level> levels;
    [SerializeReference] private int initialLevel = 0;
    [SerializeField] private VoidEvent LevelCompleteEvent;
    
    [Header("UI")]
    [SerializeField] private bool isBoardOnTitleScreen = false;
    #endregion Editor Fields

    #region Backing Fields
    private InputSystem_Actions _inputActions;
    /// <summary>
    /// The InputSystem_Actions instance for handling player input. 
    /// This is initialized in the Awake method and provides access to the player's input actions.
    /// </summary>
    public InputSystem_Actions InputActions => _inputActions;

    /// <summary>
    /// The current level index that the player is on.
    /// </summary>
    public int currentLevel = 0;

    // This is the top left square in the grid's position.
    private Vector2 _initialPosition;

    // Tracks the state of each tile in the board.
    private Tile[,] _board;
    private int _boardWidth;
    #endregion Backing Fields

    #region Lifecycle Methods
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }

    private void Start()
    {
        LoadLevel(initialLevel);
    }

    private void OnEnable()
    {
       
        LevelCompleteEvent.OnEventRaised += NextLevel;
    }

    private void OnDisable()
    {
        LevelCompleteEvent.OnEventRaised -= NextLevel;
    }
    #endregion Lifecycle Methods

    #region Level Generation Methods
    /// <summary>
    /// Updates the current level to provided level and generates that level's board.
    /// </summary>
    /// <param name="levelIndex">The level to generate.</param>
    public void GenerateLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        GenerateBoard(levels[levelIndex]);
    }

    /// <summary>
    /// Called when a level is completed via event.
    /// </summary>
    /// <param name="data">Empty.</param>
    private void NextLevel(Unit data)
    {
        GameObject cat = GameObject.FindWithTag("Cat");
        ++currentLevel;
        
        Debug.Log(cat.GetComponent<CatMovement>().CatWorldPosition + " 1");
        IrisTransition.Instance.IrisClose(cat != null ? cat.GetComponent<CatMovement>().CatWorldPosition : Vector3.zero, () =>
        {
            LoadLevel(currentLevel);
        });
    }

    /// <summary>
    /// Handles UI setup calling and board generation for the level.
    /// </summary>
    /// <param name="levelIndex">The level to generate.</param>
    private void LoadLevel(int levelIndex)
    {
        // No loading screen.
        if (isBoardOnTitleScreen)
        {
            GenerateLevel(levelIndex);
            return;
        }

        // Loading screen.
        if (currentLevel < levels.Count)
        {
            GameUIScript.Instance.LoadLevel(currentLevel);
            GameObject cat = GameObject.FindWithTag("Cat");
            Debug.Log(cat.GetComponent<CatMovement>().CatWorldPosition + " 2");
            Debug.Log(cat != null ? cat.GetComponent<CatMovement>().CatWorldPosition : Vector3.zero + " 2.5");
            IrisTransition.Instance.IrisOpen(cat != null ? cat.GetComponent<CatMovement>().CatWorldPosition : Vector3.zero, () =>
            {
                //GameUIScript.Instance.RestartLevel();
            });
        }
    }

    /// <summary>
    /// Generates the board for a level.
    /// </summary>
    /// <param name="level">The level to generate a board for.</param>
    private void GenerateBoard(Level level)
    {
        Debug.Log("GENERATED!");
        // Clean up old trash.
        for (int i = gameObject.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }

        // New board width.
        _boardWidth = level.BoardWidth;

        // Position setup.
        float boardArea = level.levelTilesToGenerate[0].tile.transform.localScale.x * _boardWidth;
        _initialPosition = new Vector2(this.transform.position.x + boardArea / 2f - 0.5f, this.transform.position.y + boardArea / 2f - 0.5f);

        // Board init.
        _board = new Tile[_boardWidth, _boardWidth];

        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardWidth; j++)
            {
                // Gets next tile.
                Tile tile = level.levelTilesToGenerate[i + (j * _boardWidth)].tile.GetComponent<Tile>();
                if (tile == null)
                {
                    Debug.Log($"Tile {i + (j * _boardWidth)} is empty");
                    continue;
                }
                
                // Updates tile stats.
                tile.tileIndex = new Vector2Int(i, j);
                tile.tilePosition = GetTilePosition(i, j);

                // Generates new tile visually.
                Tile tileObject = Instantiate(tile, tile.tilePosition, Quaternion.identity, this.transform);

                // If tile has a component.
                if (level.levelTilesToGenerate[i + (j * _boardWidth)].tileComponent != null)
                {
                    // If is a Cat, setup cat.
                    CatMovement catMovement = level.levelTilesToGenerate[i + (j * _boardWidth)].tileComponent.GetComponent<CatMovement>();
                    if (catMovement != null)
                    {
                        catMovement.catGridPosition = new Vector2Int(i, j);
                        catMovement.maxCatSteps = level.stepsAllowed;
                        Debug.Log(catMovement.CatWorldPosition + " 3");
                    }

                    // Spawn the component.
                    tileObject.tileComponent = Instantiate(level.levelTilesToGenerate[i + (j * _boardWidth)].tileComponent, tile.tilePosition, Quaternion.identity, tileObject.transform);
                }

                // Adds the tile to the board.
                _board[i, j] = tileObject;
            }
        }

    }

    /// <summary>
    /// Gets the world position of a tile based on it's index.
    /// </summary>
    /// <param name="x">The x index in the board 2d array.</param>
    /// <param name="y">The y index in the board 2d array.</param>
    /// <returns>The world position of the tile.</returns>
    private Vector2 GetTilePosition(int x, int y)
    {
        return new Vector2(x - _initialPosition.x, -y + _initialPosition.y);
    }
    #endregion Level Generation Methods

    #region Movement Methods
    /// <summary>
    /// Attempts to move the cat from one tile to another.
    /// </summary>
    /// <param name="catGridPosition">The cat's current grid position. Updates via ref after successful move.</param>
    /// <param name="moveDirection">The direction to attempt to move the cat.</param>
    /// <param name="isBoosted">Whether the cat has boosted movement.</param>
    /// <returns>The new cat world position.</returns>
    public Vector2 CatMove(ref Vector2Int catGridPosition, Vector2 moveDirection, ref bool isBoosted)
    {
        // Set new cat grid position based on whether the cat is boosted.
        Vector2Int newCatGridPosition = isBoosted ? 
            catGridPosition + new Vector2Int(Mathf.FloorToInt(moveDirection.x) * 2, -Mathf.FloorToInt(moveDirection.y) * 2) : 
            catGridPosition + new Vector2Int(Mathf.FloorToInt(moveDirection.x), -Mathf.FloorToInt(moveDirection.y));

        isBoosted = false;

        // Checks if the cat can move the distance.
        if (newCatGridPosition.x <= -1 ||
            newCatGridPosition.y <= -1 ||
            newCatGridPosition.x >= _boardWidth ||
            newCatGridPosition.y >= _boardWidth ||
            _board[newCatGridPosition.x, newCatGridPosition.y].tileComponent != null)
            return new Vector2(1000, 1000);

        // Performs all the required actions to move the cat.
        GameObject cat = _board[catGridPosition.x, catGridPosition.y].tileComponent;
        _board[newCatGridPosition.x, newCatGridPosition.y].tileComponent = cat;
        _board[catGridPosition.x, catGridPosition.y].tileComponent = null;
        cat.transform.SetParent(_board[newCatGridPosition.x, newCatGridPosition.y].transform);

        // Updates the cat's grid position and returns it's world position.
        catGridPosition = newCatGridPosition;
        return GetTilePosition(newCatGridPosition.x, newCatGridPosition.y);
    }

    /// <summary>
    /// Attempts to move the block from one tile to another.
    /// </summary>
    /// <param name="blockGridPosition">The block's current grid position.</param>
    /// <param name="moveDirection">The direction to attempt to move the block.</param>
    public void MoveBlock(Vector2Int blockGridPosition, Vector2 moveDirection)
    {
        // Gets the new block grid position.
        Vector2Int newBlockGridPosition = blockGridPosition + new Vector2Int(Mathf.FloorToInt(moveDirection.x), -Mathf.FloorToInt(moveDirection.y));

        // Checks if the block can move the distance.
        if (newBlockGridPosition.x <= -1 ||
            newBlockGridPosition.y <= -1 ||
            newBlockGridPosition.x >= _boardWidth ||
            newBlockGridPosition.y >= _boardWidth ||
            _board[newBlockGridPosition.x, newBlockGridPosition.y].tileComponent != null ||
            _board[newBlockGridPosition.x, newBlockGridPosition.y].gameObject.CompareTag("MoveBlockBan"))
                return;

        // Performs all the required actions to move the block.
        PerformPositionSwap(blockGridPosition, newBlockGridPosition);

        // Triggers the new tile's step event.
        _board[newBlockGridPosition.x, newBlockGridPosition.y].OnStep();
    }

    /// <summary>
    /// Teleports cat from one teleportation tile to another.
    /// </summary>
    /// <param name="initialTile">The first teleportation tile.</param>
    /// <param name="destinationTile">The destination teleportation tile.</param>
    public void Teleport(TeleportTile initialTile, TeleportTile destinationTile)
    {
        // Gets the item to tp.
        GameObject tpItem = initialTile.tileComponent;
     
        // Checks if it's a cat.
        CatMovement catMovement = tpItem.GetComponent<CatMovement>();   
        if (catMovement == null) return;

        // Performs all the required actions to move the cat.
        PerformPositionSwap(initialTile.tileIndex, destinationTile.tileIndex);

        catMovement.catGridPosition = new Vector2Int(destinationTile.tileIndex.x, destinationTile.tileIndex.y);
        catMovement.CatWorldPosition = destinationTile.tilePosition;
    }

    /// <summary>
    /// Swaps the tile component from 1 grid position to another.
    /// </summary>
    /// <param name="gridPosition">The starting position.</param>
    /// <param name="newGridPosition">The ending position.</param>
    private void PerformPositionSwap(Vector2Int gridPosition, Vector2Int newGridPosition)
    {
        GameObject item = _board[gridPosition.x, gridPosition.y].tileComponent;
        _board[newGridPosition.x, newGridPosition.y].tileComponent = item;
        _board[gridPosition.x, gridPosition.y].tileComponent = null;
        item.transform.SetParent(_board[newGridPosition.x, newGridPosition.y].transform);
        item.transform.position = GetTilePosition(newGridPosition.x, newGridPosition.y);
    }
    #endregion Movement Methods

    #region Trigger Methods
    /// <summary>
    /// Triggers the tile's event at a given grid position.
    /// </summary>
    /// <param name="tileGridPosition"></param>
    /// <returns>Whether the tile is a door tile.</returns>
    public bool TriggerTile(Vector2Int tileGridPosition)
    {
        _board[tileGridPosition.x, tileGridPosition.y].OnStep();

        return _board[tileGridPosition.x, tileGridPosition.y].tag == "Door";
    }

    /// <summary>
    /// Triggers the tile's component's event at a given grid position.
    /// </summary>
    /// <param name="x">The x index in the board 2d array.</param>
    /// <param name="y">The y index in the board 2d array.</param>
    /// <param name="direction">The direction which the trigger is coming from.</param>
    public void TriggerTileComponent(int x, int y, Vector2Int direction)
    {
        // Checks for bounds or a trigger component to trigger.
        if (x <= -1 || y <= -1 || x >= _boardWidth || y >= _boardWidth || _board[x, y].tileComponent == null)
            return;

        BlockBase blockBase = _board[x, y].tileComponent.GetComponent<BlockBase>();
        if (blockBase != null)
        {
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "blockBase", blockBase },
                { "board", this },
                { "blockPosition", new Vector2Int(x, y) },
                { "direction", direction }
            };

            blockBase.ability.Activate(data);
        }
    }
    #endregion Trigger Methods

    public void CatCleanUp(Vector2Int oldPosition, Vector2Int startPosition)
    {
        GameObject cat = _board[oldPosition.x, oldPosition.y].tileComponent;
        _board[oldPosition.x, oldPosition.y].tileComponent = null;
        _board[startPosition.x, startPosition.y].tileComponent = cat;
    }
}
