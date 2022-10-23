using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelSession : MonoBehaviour {
	[Header("Timer")]
	public bool enableTimer = true;
	public float gameTimerMax = 60 * 2;
	public float gameTimer;

	[Header("Cinematic")]
	public PlayableDirector endCutscene;

	// ------------------------------

	public bool Active { get; private set; } = true;

	// ------------------------------

	private void Start() {
		gameTimer = gameTimerMax;
		endCutscene = GameObject.FindWithTag("EndCutscene").GetComponent<PlayableDirector>();
	}

	private void Update() {
		if (enableTimer && Active) {
			gameTimer -= Time.deltaTime;
			if (gameTimer <= 0) {
				Active = false;
				StartCoroutine(OnSessionEnded());
			}
		}
	}

	private IEnumerator OnSessionEnded() {
		Debug.Log("LEVEL SESSION ENDED");
		FindObjectOfType<Player>().SetIdle();

		yield return new WaitForSeconds(2f);

		FindObjectOfType<CameraController>().TrackWorld();

		yield return new WaitForSeconds(2f);

		endCutscene.Play();
	}
}
