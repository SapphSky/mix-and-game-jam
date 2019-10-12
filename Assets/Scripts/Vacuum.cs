using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class Vacuum : MonoBehaviour {
		public bool active;
		public bool hasObjectHeld;
		public Rigidbody2D heldObject;

		private Rigidbody2D body;

		private void Awake() {
			body = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			if (active) {
				Primary();
			}
		}

		private void Primary() {
			Debug.DrawLine(body.position, body.transform.right * 3.0f);
			RaycastHit2D[] hits = Physics2D.CapsuleCastAll(transform.position, new Vector2(0.5f, 3.0f), CapsuleDirection2D.Vertical, 0, Vector2.right);
			if (hits.Length > 0) {
				foreach (var item in hits) {
					if (item.transform.GetComponent<Rigidbody2D>()) {
						PullTowards(item.transform.GetComponent<Rigidbody2D>());
					}
				}
			}
		}

		private void PullTowards(Rigidbody2D obj) {
			var distToPoint = obj.position - body.position;
			obj.AddForce(-distToPoint, ForceMode2D.Force);
		}

		private void HoldObject(Rigidbody2D obj) {
			var joint = obj.gameObject.AddComponent<FixedJoint2D>();
			joint.connectedBody = GetComponent<Rigidbody2D>();
		}

		private void ReleaseObject(Rigidbody2D obj) {
			Destroy(obj.gameObject.GetComponent<FixedJoint2D>());
			obj.AddForce(transform.right * 5, ForceMode2D.Impulse);
		}
	}
}
