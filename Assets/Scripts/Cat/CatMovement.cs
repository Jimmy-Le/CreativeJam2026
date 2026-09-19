using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    [SerializeField] public InputActionAsset inputActions;
    [SerializeField] public Transform catBody;
    public Vector2 catPosition;
    private InputAction cat_move;
    private Vector2 direction;
    private float speed = 10f;

    void Awake()
    {
        cat_move = inputActions.FindAction("Move");
    }

    void Update()
    {
        if(cat_move.WasPressedThisFrame())    // TODO, Move by tile
        {
            direction = cat_move.ReadValue<Vector2>();

                catBody.Translate( direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0, Space.World);

                

        }
    }

    // TODO, Convert this into Explode()
    // Activate abilities of adjacent blocks
    public void OnCollisionEnter2D(Collision2D collision)
    {
        BlockBase block = collision.gameObject.GetComponent<BlockBase>();

        if(block != null)
        {
            block.ability.Activate();
        }
    }

    
}
