using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfinderRedux : MonoBehaviour {

    float waitTime, startWaitTime = 1;
    int randomSpot;
    float playerDist, trackRange = 10, attackRange = 1;
    public float speed;


    Vector2 moveToSite, vanishPos;
    public GameObject player;

    bool sightBreak, lost = true;


    //patrol between specific locations added by dev
    public Transform[] moveSpots;

    //patrol zone based on min/max x,y locations
        //public Transform moveZone;




    void Start () {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
        player = GameObject.FindWithTag("Player");
        //patrol zone based on min/max x,y locations
        //moveZone.Position = new Vector2(Random.range(minX, maxX), Random.Range(minY, maxY));
    }


    void Update () {

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
                }
            }
        }
        
        //Return to patrol if player is not in range of the enemy
        else if (playerDist > trackRange && lost == true)
        {
            moveToSite = moveSpots[randomSpot].position;
        }
        
	}


    //if the player exits the range of the enemy, set the VanishPos to 
    void OnTriggerExit2D(Collider2D col)
    {
        print(col.tag);
        if (col.tag == "Player")
        {
            vanishPos = player.transform.position;
            sightBreak = true;
        }
    }


    void waitTimer()
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
