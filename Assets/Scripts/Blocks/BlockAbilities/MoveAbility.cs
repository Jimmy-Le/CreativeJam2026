using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAbility", menuName = "BlockAbilities/MoveAbility")]
public class MoveAbility : BlockAbility
{
    public override void Activate(Dictionary<string, object> options)
    {
        Board board = (Board)options["board"];
        Vector2Int blockPosition = (Vector2Int)options["blockPosition"];
        Vector2Int direction = (Vector2Int)options["direction"];
        board.MoveBlock(blockPosition, direction);
    }
}
