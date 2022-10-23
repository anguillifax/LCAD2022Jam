using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLoadScene : MonoBehaviour {
	public string scene;

	private void Start() {
		SceneManager.LoadScene(scene);
	}
}
