using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class CharacterController : MonoBehaviour {
		public struct Inputs {
			public string horizontal;
			public string vertical;
			public string jump;

			public string vacuum;
			public string blower;
		}
		public Inputs inputs;

		public Transform aimTransform;

		private Camera cam;

		private void Start() {
			cam = Camera.main;
		}

		private void Update() {
			Vector3 point = new Vector3();
			Event currentEvent = Event.current;
			Vector2 mousePos = new Vector2();

			mousePos.x = currentEvent.mousePosition.x;
			mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

			point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

			aimTransform.LookAt(point, Vector3.up);
		}
	}
}