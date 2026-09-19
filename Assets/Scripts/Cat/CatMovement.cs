using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    [SerializeField] public InputSystem_Actions inputActions;
    [SerializeField] public Transform catBody;
    [SerializeField] public Animator animator;

    // Cache Trigger ID For performance apparently
    private static readonly int MoveSideHash = Animator.StringToHash("MoveSide");
    private static readonly int MoveUpHas = Animator.StringToHash("MoveUp");
    private static readonly int MoveDownHash = Animator.StringToHash("MoveDown");
    private static readonly int ExplodeHash = Animator.StringToHash("onExplode");
    private static readonly int UnExplodeHash = Animator.StringToHash("onReverse");


    public Vector2Int catPosition;
    //// TODO: ANimation speed
    ////private float speed = 10f;
    private Board board;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        board = FindAnyObjectByType<Board>();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += MoveCat;
        inputActions.Player.Explode.performed += Explode;
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= MoveCat;
        inputActions.Player.Explode.performed -= Explode;
    }

    private void MoveCat(InputAction.CallbackContext context)
    {
        if (board == null) return;
        Vector2 direction = context.ReadValue<Vector2>();

        Vector2 newCatPosition = board.CatMove(ref catPosition, direction);
        if (newCatPosition == -Vector2.one) return;

        // TODO: animate
        catBody.position = newCatPosition;
    }

    public void Explode(InputAction.CallbackContext context)
    {
        board.TriggerTile(catPosition.x + 1, catPosition.y);
        board.TriggerTile(catPosition.x - 1, catPosition.y);
        board.TriggerTile(catPosition.x, catPosition.y + 1);
        board.TriggerTile(catPosition.x, catPosition.y - 1);
    }
}
