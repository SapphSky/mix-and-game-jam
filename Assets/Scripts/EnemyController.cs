using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D enemy;
    private Rigidbody2D vacuum;


    [Header("Random movement")]
    public float maxRandRangeX = 0;
    public float minRandRangeX = 0;
    public float maxRandRangeY = 0;
    public float minRandRangeY = 0;
    public float movePause = 0;
    public float randMoveTimer = 0;


    [Header("Health")]
    public float health;
    [Header("Movement")]
    public float movementSpeed;
    [Header("Attack")]
    public float attackSpeed;
    public bool playerDetect = false;

    

    private void Update()
    {
        if (!playerDetect)
        {
            RandomMovement();
        }
       
    }

    private void RandomMovement()
    {
        Vector2 movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        Vector2 movementPerSecond = movementDirection * movementSpeed;
        //create random Vector2
        Vector2 moveRandom = new Vector2(Random.Range(minRandRangeX,maxRandRangeX),Random.Range(minRandRangeY,maxRandRangeY));
        //set the timer
        randMoveTimer= randMoveTimer - 1 * Time.deltaTime;
        //move the enemy when timer is 0 then reset timer
        if (randMoveTimer <= 0)
        {
            randMoveTimer = movePause;

            enemy.velocity = moveRandom;
        }
    }
    //when player is in radius
    private void OnTriggerStay2D(Collider2D other)
    {
        playerDetect = true;

        if (other.gameObject.tag == "Player")
        {
            Vector2 playerPos = new Vector2(other.transform.position.x - transform.position.x, other.transform.position.y - transform.position.y);

            enemy.velocity = playerPos;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        playerDetect = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag =="Player" && Input.GetButton("Fire1"))
        {
            Destroy(gameObject);
        }
    }
}
