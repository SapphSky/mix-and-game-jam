using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class Vacuum : MonoBehaviour {
		public bool active;

		private void Update() {
			if (active) {
				Debug.DrawRay(transform.position, transform.forward, Color.white);
			}
		}
	}
}