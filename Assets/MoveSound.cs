using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSound : MonoBehaviour
{
  bool didItPlay = false;
  public AK.Wwise.Event MovingSound = null;
  public AK.Wwise.Event StopMovingSound = null;
  public AK.Wwise.RTPC Velocity = null;
      
  
      void Update () 
      {
          if(GetComponent<Rigidbody2D>().velocity.magnitude >= 0.1 && !didItPlay)
          {
              MovingSound.Post(gameObject);
              didItPlay = true;
              Velocity.SetValue(gameObject, GetComponent<Rigidbody2D>().velocity.magnitude);
          }
           else if (GetComponent<Rigidbody2D>().velocity.magnitude == 0 && didItPlay)
          {
            StopMovingSound.Post(gameObject);
            didItPlay = false;

          }


      }
}
//!MovingSound.Post