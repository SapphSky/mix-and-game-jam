using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class Character : MonoBehaviour {
		[Header("Parameters")]
		public int CurrentHealth;
		public int MaxHealth;
		public bool canTakeDamage;

		// Events
		public delegate void OnHealed();
		public event OnHealed Healed;

		public delegate void OnDamage();
		public event OnDamage Damaged;

		public delegate void OnKilled();
		public event OnKilled Killed;

		private void Awake() {
			
		}

		private void Start() {
			
		}

		private void Update() {
			if (CurrentHealth <= 0) {
				// Game Over
			}
		}

		private void TakeDamage(int amount) {
			if (canTakeDamage) {
				CurrentHealth -= amount;
				Damaged();
				StartCoroutine(IFrames(2.0f));
			}
		}

		private void Heal(int amount) {
			CurrentHealth += amount;
			Healed();
		}

		private IEnumerator IFrames(float duration) {
			canTakeDamage = false;
			// Perform IFrame indication here?

			yield return new WaitForSeconds(duration);
			canTakeDamage = true;
		}
	}
}
