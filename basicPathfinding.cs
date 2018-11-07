using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicPathfinding : MonoBehaviour
{

    //Basic AI
    public GameObject player;
    float distFromTarget, range = 10;
    Vector2 SpawnPos;
    Vector2 VanishPos;
    bool sightBreak, lost = false;
    float timer = 0;


    float speed = 0, moveSpeed = .5f, Acceleration, maxSpeed = 5, speedUp = .5f;


    //Lerping
    Transform playerPos;
    float smoothSpeed = 0.125f;
    Vector3 offset;
    Vector2 speed0Lerp, speedLerp, speed1Lerp;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        SpawnPos = new Vector2(transform.position.x, transform.position.y);
        playerPos = player.transform;
        
    }

    // Update is called once per frame
    void Update()
    {

        print(timer);
        Acceleration = speedUp * timer;
        speed = moveSpeed + Acceleration;
        if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }
        

        //Setting up player and checking distance between player and AI
        if (player != null)
        {
            distFromTarget = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y));
        }

        //if the player is within range, begin tracking
        if (distFromTarget < range)
        {
            sightBreak = false;
            lost = false;
            TrackPlayer();
        }

        if (sightBreak == true)
        {
            Search();
        }

        if (lost == true)
        {
            Invoke("Home", 0.5f);
        }
        

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timer = timer + (1 * Time.deltaTime);
            sightBreak = false;
        }
    }

    //begin timer for multiplying speed
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timer = timer + (1 * Time.deltaTime);
            if (timer >= 10)
            {
                timer = 10;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            VanishPos = new Vector2(player.transform.position.x, player.transform.position.y);
            sightBreak = true;
            
        }
    }

    void TrackPlayer()
    {
        Vector2 tracking = new Vector2((transform.position.x - playerPos.position.x) * speed,   (transform.position.y - playerPos.position.y) * speed);
        GetComponent<Rigidbody2D>().velocity = -tracking;

    }

    void Search()
    {
        Vector2 searching = new Vector2((transform.position.x - VanishPos.x) * speed, (transform.position.y - VanishPos.y) * speed);
        GetComponent<Rigidbody2D>().velocity = -searching;

        if(transform.position.x <= VanishPos.x + 1 && transform.position.x >= VanishPos.x - 1 && transform.position.y <= VanishPos.y + 1 && transform.position.y >= VanishPos.y - 1)
        {
            sightBreak = false;
            lost = true;
        }
        
    }

    void Home()
    {
        Vector2 returning = new Vector2((transform.position.x - SpawnPos.x) * speed, (transform.position.y - SpawnPos.y) * speed);
        GetComponent<Rigidbody2D>().velocity = -returning;
        timer = 0;
    }
}

