using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static int highscore;
	public static GameManager Instance { get; private set; }

	public bool debugMode;

	public float gameTimer = 60 * 2;
	public float remainingTime;

	public GameObject prefabFlowerCounter;

	// ------------------------------

	private void Awake() {
		Instance = this;
		highscore = 0;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Equals)) {
			Instantiate(prefabFlowerCounter);
		}
	}
}
