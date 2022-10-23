using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAction : MonoBehaviour {
	public string sceneName;

	public void LoadSelected() {
		Load(sceneName);
	}

	public void Load(string scene) {
		SceneManager.LoadScene(scene);
	}
}
