using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartKey : MonoBehaviour
{
    public Animator anim;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            anim.SetBool("KeyObtained", true);
            Destroy(gameObject);
        }
    }

}
