using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class Vacuum : MonoBehaviour {
		public bool active;
		public bool hasObjectHeld;
		public Rigidbody2D heldObject;

		[Tooltip("Element 0: Vacuum Start\n" +
			"Element 1: Vacuum Loop\n" +
			"Element 2: Vacuum Stop")]
		public AudioClip[] audioClips = new AudioClip[3];

		private AudioSource asource;
		private Rigidbody2D body;
		private bool playing;

		private void Awake() {
			asource = GetComponent<AudioSource>();
			body = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			if (active) {
				Primary();

				if (!playing) {
					StartCoroutine(PlaySound());
					playing = true;
				}
			}
			else {
				if (playing) {
					StartCoroutine(StopSound());
					playing = false;
				}
			}
		}

		private IEnumerator PlaySound() {
			asource.clip = audioClips[0];
			asource.loop = false;
			asource.Play();

			yield return new WaitForSeconds(asource.clip.length);

			asource.clip = audioClips[1];
			asource.loop = true;
			asource.Play();
		}

		private IEnumerator StopSound() {
			asource.clip = audioClips[2];
			asource.loop = false;
			asource.Play();
			yield return null;
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