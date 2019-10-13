using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class Vacuum : MonoBehaviour {
		public AK.Wwise.Event VacuumSoundStart = null;
		public AK.Wwise.Event VacuumSoundStop = null;
		public bool active;
		public bool hasObjectHeld;
		public Rigidbody2D heldObject;

		public LayerMask layerMask;

		//[Tooltip("Element 0: Vacuum Start\n" +
		//	"Element 1: Vacuum Loop\n" +
		//	"Element 2: Vacuum Stop")]
		//public AudioClip[] audioClips = new AudioClip[3];

		//private AudioSource asource;
		private Rigidbody2D body;
		private bool playing;

		private void Awake() {
			AkSoundEngine.RegisterGameObj(gameObject);
			//asource = GetComponent<AudioSource>();
			body = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			if (active && !hasObjectHeld) {
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
			//asource.clip = audioClips[0];
			//asource.loop = false;
			//asource.Play();

			yield return new WaitForSeconds(0);

			//asource.clip = audioClips[1];
			//asource.loop = true;
			//asource.Play();
			VacuumSoundStart.Post(gameObject);
		}

		private IEnumerator StopSound() {
			//asource.clip = audioClips[2];
			//asource.loop = false;
			//asource.Play();
			VacuumSoundStop.Post(gameObject);
			yield return null;
		}

		private void Primary() {
			Debug.DrawLine(body.position, body.transform.right * 3.0f);
			RaycastHit2D[] hits = Physics2D.RaycastAll(body.position, body.transform.right, 5.0f, layerMask);
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

			if (distToPoint.magnitude < 1) {
				HoldObject(obj);
				active = false;
			}
		}

		private void HoldObject(Rigidbody2D obj) {
			heldObject = obj;
			hasObjectHeld = true;

			var joint = obj.gameObject.AddComponent<FixedJoint2D>();
			joint.connectedBody = body;
		}

		public void ReleaseObject() {
			
			Destroy(heldObject.gameObject.GetComponent<FixedJoint2D>());
			heldObject.AddForce(transform.right * 20, ForceMode2D.Impulse);
			
			hasObjectHeld = false;
			heldObject = null;
		}
	}
}