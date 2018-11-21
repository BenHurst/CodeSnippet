using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mozzyNav : NavMesh {

    Animator mozzy;
    public GameObject thisMozzy;
    public float speed = 3;
    public int damage;

    protected override void Start()
    {
        base.Start();
        mozzy = thisMozzy.GetComponent<Animator>();
    }




    protected override void Update()
    {
        base.Update();

        //TEMP MOVE TO START WHEN SORTED
        GetComponent<NavMeshAgent>().speed = speed;


        if (playerDist < 1)
        {
            mozzy.SetBool("isAttacking", true);
        }
        else
        {
            mozzy.SetBool("isAttacking", false);
        }

        if (waitTime <= 0)
        {
            StartIdling();
        }
    }





    protected override void Patrol()
    {
        base.Patrol();
        
        //flip sprite anim based on position from location
        if (transform.position.x <= patrolArea.x )
        {
            mozzy.SetBool("isMoving", true);
            thisMozzy.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x >= patrolArea.x)
        {
            mozzy.SetBool("isMoving", true);
            thisMozzy.GetComponent<SpriteRenderer>().flipX = false;
        }
    }


    protected override void SetDestination()
    {
        base.SetDestination();

        //flip sprite anim based on position from player
        if (transform.position.x <= targetVector.x)
        {
            mozzy.SetBool("isMoving", true);
            thisMozzy.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (transform.position.x >= targetVector.x)
        {
            mozzy.SetBool("isMoving", true);
            thisMozzy.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    protected override void StartIdling()
    {
        mozzy.SetBool("isMoving", false);
        mozzy.SetBool("isAttacking", false);
    }

    void DealDamage()
    {
        GameManager.instance.TakeDamage(damage);
    }

}
