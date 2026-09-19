using System;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    [SerializeField] public InputSystem_Actions inputActions;
    [SerializeField] public Transform catBody;
    public Vector2Int catPosition;
    private InputAction cat_move;
    // TODO: ANimation speed
    //private float speed = 10f;
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
        Vector2 direction = cat_move.ReadValue<Vector2>();

        Vector2 newCatPosition = board.CatMove(ref catPosition, direction);
        if (newCatPosition == -Vector2.one) return;

        // TODO: animate
        catBody.position = newCatPosition;
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

    public void Explode(InputAction.CallbackContext context)
    {
        board.TriggerTile(catPosition.x + 1, catPosition.y);
        board.TriggerTile(catPosition.x - 1, catPosition.y);
        board.TriggerTile(catPosition.x, catPosition.y + 1);
        board.TriggerTile(catPosition.x, catPosition.y - 1);
    }
}
