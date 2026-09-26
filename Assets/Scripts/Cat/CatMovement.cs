using PrimeTween;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    #region Animation Hashes
    private static readonly int MoveSideHash = Animator.StringToHash("MoveSide");
    private static readonly int MoveUpHash = Animator.StringToHash("MoveUp");
    private static readonly int MoveDownHash = Animator.StringToHash("MoveDown");
    private static readonly int ExplodeHash = Animator.StringToHash("onExplode");
    private static readonly int UnExplodeHash = Animator.StringToHash("onReverse");
    private static readonly int MoveLeftHash = Animator.StringToHash("MoveSideLeft");
    #endregion Animation Hashes

    #region Editor Fields
    [Header("Events")]
    [SerializeField] private IntEvent updateStepsEvent;
    [SerializeField] private VoidEvent explodeCatEvent;
    [SerializeField] private VoidEvent skipBoostEvent;
    [SerializeField] private VoidEvent levelCompleteEvent;

    [Header("UI")]
    [SerializeField] private Transform catBody;

    [Header("Animation")]
    [SerializeField] public Animator animator;
    [SerializeField] private float moveAnimationDuration = 0.35f;
    #endregion Editor Fields

    #region Backing Fields
    /// <summary>
    /// The cat's world position.
    /// </summary>
    public Vector2 CatWorldPosition
    {
        get => catBody.position;
        set => catBody.position = value;
    }
    private Vector2 _startWorldPosition;

    /// <summary>
    /// The cat's index in the Grid.
    /// </summary>
    public Vector2Int catGridPosition;
    private Vector2Int _startGridPosition;

    /// <summary>
    /// The max steps allowed before an explosion.
    /// </summary>
    public int maxCatSteps = 0;
    private int _currentCatStep = 0;

    private Board _board;

    private bool _isBoosted = false;
    private bool _canExplode = true;

    private List<Vector2> _movementSteps = new();
    private bool _isMoving = false;
    #endregion Backing Fields

    #region Lifecycle Methods
    private void Awake()
    {
        _board = FindAnyObjectByType<Board>();
    }

    private void Start()
    {
        _startGridPosition = catGridPosition;
        _startWorldPosition = CatWorldPosition;
        _movementSteps.Add(_startWorldPosition);
        updateStepsEvent.Raise(maxCatSteps - _currentCatStep);
    }

    private void OnEnable()
    {
        _board.InputActions.Player.Enable();
        _board.InputActions.Player.Move.performed += MoveCat;
        _board.InputActions.Player.Explode.performed += INSExplode;
        explodeCatEvent.OnEventRaised += ExplodeEvent;
        skipBoostEvent.OnEventRaised += EnableBoost;
        levelCompleteEvent.OnEventRaised += DisableExplosion;
    }

    private void OnDisable()
    {
        _board.InputActions.Player.Disable();
        _board.InputActions.Player.Move.performed -= MoveCat;
        _board.InputActions.Player.Explode.performed -= INSExplode;
        explodeCatEvent.OnEventRaised -= ExplodeEvent;
        skipBoostEvent.OnEventRaised -= EnableBoost;
        levelCompleteEvent.OnEventRaised -= DisableExplosion;
    }
    #endregion Lifecycle Methods

    #region Event Methods
    /// <summary>
    /// Triggers the tile component for in each direction.
    /// </summary>
    public void TriggerAdjacentTiles()
    {
        _board.TriggerTileComponent(catGridPosition.x + 1, catGridPosition.y, new Vector2Int(1, 0));
        _board.TriggerTileComponent(catGridPosition.x - 1, catGridPosition.y, new Vector2Int(-1, 0));
        _board.TriggerTileComponent(catGridPosition.x, catGridPosition.y + 1, new Vector2Int(0, -1));
        _board.TriggerTileComponent(catGridPosition.x, catGridPosition.y - 1, new Vector2Int(0, 1));
    }

    /// <summary>
    /// Called when a skip boost is activated via event.
    /// </summary>
    /// <param name="data">Empty.</param>
    private void EnableBoost(Unit data)
    {
        _isBoosted = true;
    }

    private void DisableExplosion(Unit data)
    {
        _canExplode = false;
    }

    /// <summary>
    /// Called when a explode is activated.
    /// </summary>
    /// <param name="data">Empty.</param>
    private void ExplodeEvent(Unit data)
    {
        _board.InputActions.Player.Disable();
        TriggerAdjacentTiles();
        SoundManager.PlaySound(SoundManager.SoundType.Break);

        // This animation calls the Explode() Function at the end of its animation frame.
        animator.SetTrigger(ExplodeHash);
    }
    #endregion Event Methods

    #region Input Methods
    /// <summary>
    /// Moves the cat when movement options are used.
    /// </summary>
    private void MoveCat(InputAction.CallbackContext context)
    {
        // If is animating back or board is empty.
        if (_board == null || _isMoving) return;

        // Get the movement direction and attempt to move the cat.
        Vector2 direction = context.ReadValue<Vector2>();
        Vector2 newCatGridPosition = _board.CatMove(ref catGridPosition, direction, ref _isBoosted);
        
        // If the movement action was unsuccessful, quit.
        if (newCatGridPosition == new Vector2(1000, 1000))
        {
            SoundManager.PlaySound(SoundManager.SoundType.Error, 0.4f);
            return;
        }
        
        // If it was successful, start by adding the position to the history
        _movementSteps.Add(newCatGridPosition);

        // Stop cat from spam moving and animates movement.
        _isMoving = true;

        if (direction.x < 0)
            animator.SetTrigger(MoveLeftHash);
        else if (direction.x > 0)
            animator.SetTrigger(MoveSideHash);
        else if (direction.y < 0)
            animator.SetTrigger(MoveUpHash);
        else if (direction.y > 0)
            animator.SetTrigger(MoveDownHash);

        // Play walking audio
        SoundManager.PlaySound(SoundManager.SoundType.Walk, 0.25f);

        Tween.Position(catBody, startValue: CatWorldPosition, endValue: newCatGridPosition, duration: moveAnimationDuration, ease: Ease.Linear).OnComplete(() =>
        {
            // Reset the state
            animator.Play("CatIdle");
            _isMoving = false;

            // Trigger events.
            _board.TriggerTile(catGridPosition);

            // Update steps;
            ++_currentCatStep;
            updateStepsEvent.Raise(maxCatSteps - _currentCatStep);

            if (_currentCatStep >= maxCatSteps && _canExplode)
            {
                CatMovement[] cats = FindObjectsByType<CatMovement>(FindObjectsSortMode.None);

                foreach (CatMovement cat in cats)
                    cat.ExplodeEvent(Unit.Default);
            }
        });
    }

    /// <summary>
    /// Explode the cat when INS is pressed.
    /// </summary>
    private void INSExplode(InputAction.CallbackContext context)
    {
        ExplodeEvent(Unit.Default);
    }
    #endregion Input Methods

    #region End Events
    /// <summary>
    /// Plays the rewind sequence.
    /// </summary>
    public async void Explode()
    {
        animator.Play("CatIdle");
        for (int i = _movementSteps.Count - 1; i >= 0; i--) 
        {
            SoundManager.PlaySound(SoundManager.SoundType.Reverse,0.2f);
            await Tween.Position(catBody, startValue: CatWorldPosition, endValue: _movementSteps[i], duration: moveAnimationDuration * 0.5f, ease: Ease.Linear);
        }
        _movementSteps.Clear();
        _movementSteps.Add(_startWorldPosition);
        RespawnCat();  
    }

    /// <summary>
    /// Resets the cat to it's init state.
    /// </summary>
    public void RespawnCat()
    {
        _board.CatCleanUp(catGridPosition, _startGridPosition);
        catGridPosition = _startGridPosition;
        CatWorldPosition = _startWorldPosition;
        _isBoosted = false;
        _currentCatStep = 0;
        updateStepsEvent.Raise(maxCatSteps - _currentCatStep);
        SoundManager.PlaySound(SoundManager.SoundType.Meow);
        _board.InputActions.Player.Enable();
    }
    #endregion End Events
}
