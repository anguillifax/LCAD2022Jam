using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static int highscore = 0;
	public static GameManager Instance { get; private set; }
	public static LevelSession Session { get; private set; }

	public bool debugMode;

	// ------------------------------

	private void Awake() {
		if (Instance != null) {
			Debug.LogWarning("Game Manager already in existence", this);
		}
		Instance = this;
	}

	private void Update() {
	}

	public void CreateSession(GameObject prefab) {
		var go = Instantiate(prefab, transform);
		Session = go.GetComponent<LevelSession>();
	}
}
