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
	public SoundApiAsset soundAsset;
	public AudioSource timerSound;

	[Header("Cinematic")]
	public PlayableDirector endCutscene;

	private bool flagHalfway;
	private bool flagFiveSec;

	// ------------------------------

	public bool Active { get; private set; } = true;

	// ------------------------------

	private void Start() {
		gameTimer = gameTimerMax;
		endCutscene = GameObject.FindWithTag("EndCutscene").GetComponent<PlayableDirector>();

		timerSound.PlayOneShot(soundAsset.levelTimerBegin);
	}

	private void OnDestroy() {
		GameManager.Instance.EndSession();
	}

	private void Update() {
		if (enableTimer && Active) {
			gameTimer -= Time.deltaTime;

			if (!flagHalfway && gameTimer <= 0.5f * gameTimerMax && 0.5f * gameTimerMax > 5) {
				NotifyHalfway();
				flagHalfway = true;
			}

			if (!flagFiveSec && gameTimer <= 5) {
				NotifyFiveSec();
				flagFiveSec = true;
			}

			if (gameTimer <= 0) {
				Active = false;
				StartCoroutine(OnSessionEnded());
			}
		}
	}

	private void NotifyHalfway() {
		timerSound.PlayOneShot(soundAsset.levelTimerHalfway);
	}

	private void NotifyFiveSec() {
		timerSound.PlayOneShot(soundAsset.levelTimerLastSeconds);
	}

	private IEnumerator OnSessionEnded() {
		timerSound.PlayOneShot(soundAsset.levelTimerEnd);
		Debug.Log("LEVEL SESSION ENDED");
		FindObjectOfType<Player>().SetIdle();

		yield return new WaitForSeconds(2f);

		FindObjectOfType<CameraController>().TrackWorld();

		yield return new WaitForSeconds(2f);

		endCutscene.Play();
	}
}
