using System.Collections;
using System.Collections.Generic;
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
        animator = blockBase.GetComponent<Animator>();

        foreach(Transform child in blockBase.transform)
        {
            if(child.CompareTag("ForegroundSprite"))
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }    
        }

        blockBase.StartCoroutine(ExplodeAnim());
        //Destruction();

    }

    public void Destruction()
    {
        Destroy(blockBase.transform.gameObject);
    }

    private IEnumerator ExplodeAnim()
    {
        SoundManager.PlaySound(SoundManager.SoundType.GlassBreak);
        animator.Play("Poof");

        yield return null;

        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        float explosionLength = state.length / Mathf.Max(state.speedMultiplier, 0.01f);

        yield return new WaitForSeconds(explosionLength);
        //blockBase.Destruction();
    }


}
