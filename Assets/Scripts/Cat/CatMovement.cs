using PrimeTween;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    [SerializeField] public InputSystem_Actions inputActions;
    [SerializeField] public Transform catBody;
    [SerializeField] public Animator animator;
    [SerializeField] private VoidEvent explodeCatEvent;
    [SerializeField] private VoidEvent skipBoostEvent;
    [SerializeField] private float moveAnimationDuration = 0.35f;
    public int stepCounter = 0;
    public int currentStep = 0;

    // Cache Trigger ID For performance apparently
    private static readonly int MoveSideHash = Animator.StringToHash("MoveSide");
    private static readonly int MoveUpHash = Animator.StringToHash("MoveUp");
    private static readonly int MoveDownHash = Animator.StringToHash("MoveDown");
    private static readonly int ExplodeHash = Animator.StringToHash("onExplode");
    private static readonly int UnExplodeHash = Animator.StringToHash("onReverse");

    private static readonly int MoveLeftHash = Animator.StringToHash("MoveSideLeft");

    public Vector2Int catPosition;
    private Board board;
    private Vector2Int startPosition;
    private Vector2 startWorldPosition;
    private bool isBoosted = false;
    private bool isMoving = false;

    private List<Vector2> movementSteps = new();

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        board = FindAnyObjectByType<Board>();
    }

    void Start()
    {
        startPosition = catPosition;
        startWorldPosition = catBody.position;
        movementSteps.Add(startWorldPosition);
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += MoveCat;
        inputActions.Player.Explode.performed += Explode;
        explodeCatEvent.OnEventRaised += ExplodeEvent;
        skipBoostEvent.OnEventRaised += EnableBoost;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= MoveCat;
        inputActions.Player.Explode.performed -= Explode;
        explodeCatEvent.OnEventRaised -= ExplodeEvent;
        skipBoostEvent.OnEventRaised -= EnableBoost;
    }

    private void EnableBoost(Unit data)
    {
        isBoosted = true;
        Debug.Log(isBoosted);
    }

    private async void MoveCat(InputAction.CallbackContext context)
    {
        if (board == null || isMoving) return;
        Vector2 direction = context.ReadValue<Vector2>();
        Vector2 newCatPosition = board.CatMove(ref catPosition, direction, ref isBoosted);
        if (newCatPosition == new Vector2(1000, 1000)) return;

        Debug.Log($"why {newCatPosition}, {Vector2.negativeInfinity}");
        movementSteps.Add(newCatPosition);
        SpriteRenderer spriteRenderer = GetComponentInParent<SpriteRenderer>();
        isMoving = true;
        if (direction.x < 0)
        {
            //spriteRenderer.flipX = true;
            animator.SetTrigger(MoveLeftHash);
        }
        else if (direction.x > 0)
        {
            //spriteRenderer.flipX = false;
            animator.SetTrigger(MoveSideHash);
        }
        else if (direction.y < 0)
        {
            animator.SetTrigger(MoveUpHash);
        }
        else if (direction.y > 0)
        {
            animator.SetTrigger(MoveDownHash);
        }
        await Tween.Position(catBody, startValue: catBody.position, endValue: newCatPosition, duration: moveAnimationDuration, ease: Ease.Linear).OnComplete(() =>
        {
            animator.Play("CatIdle");
        });
        isMoving = false;

        board.TriggerBoardAtPos(catPosition);
        
        currentStep++;
        GameUIScript.Instance?.DisplayStepsLeft();
        if (currentStep >= stepCounter)
            ExplodeEvent(Unit.Default);
    }

    private void ExplodeEvent(Unit data)
    {
        inputActions.Player.Disable();
        TriggerAdjacentTiles();
        animator.SetTrigger(ExplodeHash);       // THis animation calls the RespawnCat() Function at the end of its animation frame
    }



    public async void Explode()
    {
        animator.Play("CatIdle");
        for (int i = movementSteps.Count - 1; i >= 0; i--) 
        {
            await Tween.Position(catBody, startValue: catBody.position, endValue: movementSteps[i], duration: moveAnimationDuration * 0.5f, ease: Ease.Linear);
        }
        movementSteps.Clear();
        movementSteps.Add(startWorldPosition);
        RespawnCat();  
    }

    public void RespawnCat()
    {
        board.CatCleanUp(catPosition, startPosition);
        catPosition = startPosition;
        catBody.position = startWorldPosition;
        currentStep = 0;
        GameUIScript.Instance?.DisplayStepsLeft();
        inputActions.Player.Enable();
    }


    public void TriggerAdjacentTiles()
    {
        board.TriggerTile(catPosition.x + 1, catPosition.y, new Vector2Int(1, 0));
        board.TriggerTile(catPosition.x - 1, catPosition.y, new Vector2Int(-1, 0));
        board.TriggerTile(catPosition.x, catPosition.y + 1, new Vector2Int(0, -1));
        board.TriggerTile(catPosition.x, catPosition.y - 1, new Vector2Int(0, 1));
    }

    

    public void Explode(InputAction.CallbackContext context)
    {
        ExplodeEvent(Unit.Default);
    }
}
