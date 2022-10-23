using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectCallback : MonoBehaviour {
	[Header("Common")]
	public GameObject rootUI;
	public bool canOpen;

	[Header("Scenes")]
	public string sceneTutorial;
	public string sceneTimeAttack;
	public string sceneZenGarden;

	[Header("Sounds")]
	public SoundApiAsset asset;
	public AudioSource oneshot;

	// ------------------------------

	private void Awake() {
		rootUI.SetActive(false);
	}

	private void Update() {
		if (canOpen && Input.GetButtonDown("Cancel")) {
			if (rootUI.activeSelf) {
				oneshot.PlayOneShot(asset.menuClose);
				rootUI.SetActive(false);
			} else {
				oneshot.PlayOneShot(asset.menuOpen);
				rootUI.SetActive(true);
			}
		}
	}

	private void Load(string scene) {
		SceneManager.LoadScene(scene);
	}

	// ------------------------------

	public void LoadTutorial() {
		Load(sceneTutorial);
	}

	public void LoadTimeAttack() {
		Load(sceneTimeAttack);
	}

	public void LoadZenGarden() {
		Load(sceneZenGarden);
	}

}
