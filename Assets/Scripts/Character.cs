using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam {
	public class Character : MonoBehaviour {
		public int health;
		public int maxHealth;

		public delegate void OnKilled();
		public event OnKilled Killed;
	}
}
