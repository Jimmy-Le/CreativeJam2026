using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;
    [SerializeField] public Transform catBody;
    [SerializeField] public Animator animator;

    // Cache Trigger ID For performance apparently
    private static readonly int MoveSideHash = Animator.StringToHash("MoveSide");
    private static readonly int MoveUpHas = Animator.StringToHash("MoveUp");
    private static readonly int MoveDownHash = Animator.StringToHash("MoveDown");
    private static readonly int ExplodeHash = Animator.StringToHash("onExplode");
    private static readonly int UnExplodeHash = Animator.StringToHash("onReverse");


    public Vector2Int catPosition;
    private InputAction cat_move;
    // TODO: ANimation speed
    //private float speed = 10f;
    private Board board;

    void Awake()
    {
        cat_move = inputActions.FindAction("Move");
        board = FindAnyObjectByType<Board>();
    }


    void Update()
    {
        if (cat_move.WasPressedThisFrame())
        {
            if (board == null) return;
            Vector2 direction = cat_move.ReadValue<Vector2>();

            Vector2 newCatPosition = board.CatMove(ref catPosition, direction);
            if (newCatPosition == -Vector2.one) return;

            // TODO: animate
            animator.SetTrigger(MoveSideHash);
            catBody.position = newCatPosition;
        }
    }

    // TODO, Convert this into Explode()
    // Activate abilities of adjacent blocks
    public void OnCollisionEnter2D(Collision2D collision)
    {
        BlockBase block = collision.gameObject.GetComponent<BlockBase>();

        if (block != null)
        {
            block.ability.Activate();
        }
    }

    public void Explode()
    {

    }
}
