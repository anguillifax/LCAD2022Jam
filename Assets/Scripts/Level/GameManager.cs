using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance { get; private set; }
	public static LevelSession Session { get; private set; }

	public int highscore;
	public bool debugMode;

	// ------------------------------

	private void Awake() {
		if (Instance != null) {
			Debug.LogWarning("Game Manager already in existence", this);
			Destroy(this);
		}
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Update() {
	}

	public void CreateSession(GameObject prefab) {
		var go = Instantiate(prefab);
		Session = go.GetComponent<LevelSession>();
	}

	public void EndSession() {
		Session = null;
	}
}
