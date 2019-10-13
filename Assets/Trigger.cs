using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour {
	public UnityEvent onEnterEvent;
	public UnityEvent onStayEvent;
	public UnityEvent onExitEvent;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			onEnterEvent.Invoke();
		}
	}

	private void OnTriggerStay2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			onStayEvent.Invoke();
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			onExitEvent.Invoke();
		}
	}
}
