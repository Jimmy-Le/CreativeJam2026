using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplodeAbility", menuName = "BlockAbilities/ExplodeAbility")]
public class ExplodeAbility : BlockAbility
{
    public Animator animator;
    public BlockBase blockBase;

    public override void Activate(Dictionary<string, object> options)
    {
        // play poof animation
        
        blockBase = (BlockBase)options["blockBase"];
        blockBase.GetComponentInParent<SpriteRenderer>().enabled = false;

        animator = blockBase.GetComponentInParent<Animator>();

        if(animator == null)
        {
            animator = blockBase.GetComponentInChildren<Animator>();
        }
        
        

        animator.Play("Poof");
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        //// when animation is done.
        Destroy(blockBase.transform.gameObject);
    }

    public void Destruction()
    {
        Destroy(blockBase.transform.gameObject);
    }


}
