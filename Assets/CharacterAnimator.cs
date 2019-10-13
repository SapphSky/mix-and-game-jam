using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {
	public Rigidbody2D player;
	public Rigidbody2D aim;
	private Animator animator;

	private void Awake() {
		animator = GetComponent<Animator>();
	}

	private void Update() {
		animator.SetFloat("Locomotion", Mathf.Abs(player.velocity.x));

		if (player.velocity.x < 0) {
			transform.rotation = Quaternion.Euler(Vector3.up * -90);
		}
		else if (player.velocity.x > 0) {
			transform.rotation = Quaternion.Euler(Vector3.up * 90);
		}

		// GET THIS DONE AAAA
		// animator.SetFloat("AimDirection", );
	}
}
