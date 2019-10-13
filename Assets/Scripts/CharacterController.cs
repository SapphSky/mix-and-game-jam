using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class CharacterController : MonoBehaviour {
		[System.Serializable]
		public struct Inputs {
			public string horizontal;
			public string vertical;
			public string jump;
			public string vacuum;
			public string blower;
		}
		[Tooltip("Assign input axes here.")]
		public Inputs inputs;

		public bool canMove;
		public bool canJump;
		public bool canUseVacuum;

		[SerializeField] private Vacuum vacuum;
		[SerializeField] private Rigidbody2D body;
		[SerializeField] private Rigidbody2D aim;
		private Camera cam;
		private Vector2 mousePos;

		private void Awake() {
			body = GetComponent<Rigidbody2D>();
			cam = Camera.main;
		}

		private void Update() {
			mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

			if (body.velocity.x >= 5) {
				body.velocity = new Vector2(4, body.velocity.y);
			}
			if (body.velocity.x <= -5) {
				body.velocity = new Vector2(-4, body.velocity.y);
			}

			if (canMove) {
				body.AddForce(Vector2.right * Input.GetAxis(inputs.horizontal), ForceMode2D.Impulse);
			}

			
			if (canJump && Input.GetButtonDown(inputs.jump)) {
				body.AddForce(Vector2.up * 10.0f, ForceMode2D.Impulse);
			}

			if (canUseVacuum) {
				vacuum.active = Input.GetButton(inputs.vacuum);
				if (Input.GetButtonDown(inputs.blower)) {
					vacuum.ReleaseObject();
				}
			}
		}

		private void FixedUpdate() {
			Vector2 lookDir = mousePos - aim.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			aim.rotation = angle;
		}
	}
}
