using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderRedux : MonoBehaviour {

    protected Animator thisAnim;


    protected float waitTime, startWaitTime = 1;
    protected int randomSpot;
    protected float playerDist, trackRange = 10, attackRange = 1;
    protected float speed = 10;


    protected Vector2 moveToSite, vanishPos;
    protected GameObject player;

    protected bool sightBreak, lost = true;
    

    //patrol between specific locations added by dev
    public Transform[] moveSpots;

    //patrol zone based on min/max x,y locations
    //public Transform moveZone;




    protected virtual void Start () {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        player = GameObject.FindWithTag("Player");

        thisAnim = gameObject.GetComponent<Animator>();

        //patrol zone based on min/max x,y locations
        //moveZone.Position = new Vector2(Random.range(minX, maxX), Random.Range(minY, maxY));
    }


    protected virtual void Update () {
        
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        
        transform.position = Vector2.MoveTowards(transform.position, moveToSite, speed * Time.deltaTime);

        playerDist = Vector2.Distance(transform.position, player.transform.position);

        //if enemy is within .5 units of distance from the randomly selected location, wait 1 second, then move to the next randomly selected location
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.5f)
        {
            waitTimer();
        }


        //Track the player if the player is in range of the enemy
        if (playerDist <= trackRange)
        {
            sightBreak = false;
            lost = false;
            moveToSite = player.transform.position;
        }

        //On exit of trigger zone, enemy moves to the last known location of the player
        else if (playerDist > trackRange && sightBreak == true)
        {
            moveToSite = vanishPos;
            if (Vector2.Distance(transform.position, vanishPos) < 0.5f)
            {
                waitTimer();
                if (waitTime <= 0)
                {
                    sightBreak = false;
                    lost = true;
                    waitTime = startWaitTime;
                }
            }
        }
        
        //Return to patrol if player is not in range of the enemy
        else if (playerDist > trackRange && lost == true)
        {
            moveToSite = moveSpots[randomSpot].position;
        }
        else if (playerDist <= attackRange)
        {

        }
        Debug.Log(moveToSite + "ParentClass");

	}


    //if the player exits the range of the enemy, set the VanishPos to 
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        print(col.tag);
        if (col.tag == "Player")
        {
            vanishPos = player.transform.position;
            sightBreak = true;
        }
    }


    protected virtual void waitTimer()
    {
        if (waitTime <= 0)
        {
            //patrol zone based on min/max x,y locations
            //moveZone.Position = new Vector2(Random.range(minX, maxX), Random.Range(minY, maxY));

            //patrol between specific locations added by dev
            randomSpot = Random.Range(0, moveSpots.Length);

            waitTime = startWaitTime;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
