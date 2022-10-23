using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStartTrigger : MonoBehaviour {
	public GameObject sessionPrefab;

	public void TriggerStart() {
		GameManager.Instance.CreateSession(sessionPrefab);
		Destroy(gameObject);
	}

	public void OnTriggerEnter(Collider collider) {
		if (collider.GetIfExists<Player>(out var _)) {
			TriggerStart();
		}
	}
}
