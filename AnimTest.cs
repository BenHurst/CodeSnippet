using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest : PathfinderRedux {



    protected override void Update()
    {
        if (transform.position.x > moveToSite.x)
        {
            thisAnim.Play("Idle");
            thisAnim.SetFloat("blendTree", 3);
            GetComponentInChildren<SpriteRenderer>().flipX = false;

        }
        else if (transform.position.x < moveToSite.x)
        {
            thisAnim.SetFloat("blendTree", 3);
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.y > moveToSite.y)
        {
            thisAnim.SetFloat("blendTree", 1);
        }
        else if (transform.position.y < moveToSite.y)
        {
            thisAnim.SetFloat("blendTree", 2);
        }
        else if (Vector2.Distance(transform.position, moveToSite) >= 0.5f)
        {
            thisAnim.SetFloat("blendTree", 0);
        }
        
        //Debug.Log(moveToSite + "ChildClass");

    }

    // Use this for initialization
}
