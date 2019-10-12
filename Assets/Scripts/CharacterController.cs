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

		[SerializeField] private Rigidbody2D body;
		[SerializeField] private Rigidbody2D aimBody;

		private Camera cam;
		private Vector2 mousePos;

		private void Awake() {
			body = GetComponent<Rigidbody2D>();
			cam = Camera.main;
		}

		private void Update() {
			// Aim
			mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

			// Move
			body.AddForce(Vector2.right * Input.GetAxis(inputs.horizontal), ForceMode2D.Impulse);

			// Jump
			if (Input.GetButtonDown(inputs.jump)) {
				body.AddForce(Vector2.up * 5.0f, ForceMode2D.Impulse);
			}

			// Vacuum input
			aimBody.GetComponent<Vacuum>().active = Input.GetButton(inputs.vacuum);
		}

		private void FixedUpdate() {
			Vector2 lookDir = mousePos - body.position;
			float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
			aimBody.rotation = angle;
		}
	}
}
