using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSound : MonoBehaviour {
	public AK.Wwise.Event MovingSound = null;

	private Rigidbody2D body;
	private bool isPlaying;

	private void Update() {
		if (body.velocity.magnitude > 0) {
			isPlaying = true;
			// Play sound
			MovingSound.Post(gameObject);
			// Adjust volume based on velocity
		}
		else {
			// MovingSound.Stop(gameObject);
			// didItPlay = false;
		}
	}
}
//!MovingSound.Post