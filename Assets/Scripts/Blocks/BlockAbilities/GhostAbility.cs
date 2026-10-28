using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostAbility", menuName = "BlockAbilities/GhostAbility")]
public class GhostAbility : BlockAbility
{
    [Header("Settings")]
    [SerializeField] private float ghostOpacity = 0.25f;
    private BlockBase blockBase;
    public override void Activate(Dictionary<string, object> options)
    {
        blockBase = (BlockBase)options["blockBase"];
        SpriteRenderer[] sprites = blockBase.gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (blockBase.gameObject.CompareTag("CatMoveBan"))
        {
            blockBase.gameObject.tag = "Untagged";
            Color color = sprites[0].color;
            color.a = ghostOpacity;
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = color;
        }
        else
        {
            blockBase.gameObject.tag = "CatMoveBan";
            Color color = sprites[0].color;
            color.a = 1f;
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = color;
        }
    }
}
