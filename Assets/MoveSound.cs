using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSound : MonoBehaviour
{
  bool didItPlay = false;
  public AK.Wwise.Event MovingSound = null;
      
  
      void Update () 
      {
          if(GetComponent<Rigidbody2D>().velocity.magnitude >= 0.1 && !didItPlay)
          {
              MovingSound.Post(gameObject);
              didItPlay = true;
          }
          else
          {
             // MovingSound.Stop(gameObject);
             // didItPlay = false;
          }


      }
}
//!MovingSound.Post