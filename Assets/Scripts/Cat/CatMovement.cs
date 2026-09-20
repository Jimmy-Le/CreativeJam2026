using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    [SerializeField] public InputSystem_Actions inputActions;
    [SerializeField] public Transform catBody;
    [SerializeField] public Animator animator;
    [SerializeField] private VoidEvent explodeCatEvent;
    [SerializeField] private VoidEvent skipBoostEvent;
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
    //// TODO: ANimation speed
    ////private float speed = 10f;
    private Board board;
    private Vector2Int startPosition;
    private Vector2 startWorldPosition;
    private bool isBoosted = false;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        board = FindAnyObjectByType<Board>();
    }

    void Start()
    {
        startPosition = catPosition;
        startWorldPosition = catBody.position;
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

    private void MoveCat(InputAction.CallbackContext context)
    {
        if (board == null) return;
        Vector2 direction = context.ReadValue<Vector2>();
        Debug.Log("a" + isBoosted);
        Vector2 newCatPosition = board.CatMove(ref catPosition, direction, ref isBoosted);
        if (newCatPosition == -Vector2.one) return;

        SpriteRenderer spriteRenderer = GetComponentInParent<SpriteRenderer>();

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

        catBody.position = newCatPosition;

        currentStep++;
        GameUIScript.Instance.DisplayStepsLeft();
        if (currentStep >= stepCounter)
            ExplodeEvent(Unit.Default);
    }

    private void ExplodeEvent(Unit data)
    {
        inputActions.Player.Disable();
        animator.SetTrigger(ExplodeHash);       // THis animation calls the RespawnCat() Function at the end of its animation frame
    }



    public void Explode()
    {
        TriggerAdjacentTiles();
        RespawnCat();  
    }

    public void RespawnCat()
    {
        board.CatCleanUp(catPosition, startPosition);
        catPosition = startPosition;
        catBody.position = startWorldPosition;
        currentStep = 0;
        GameUIScript.Instance.DisplayStepsLeft();
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
