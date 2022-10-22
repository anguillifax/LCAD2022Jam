using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelStatDebug : MonoBehaviour {
	public KeyCode trigger = KeyCode.Alpha0;

	public TextMeshProUGUI text;

	// ------------------------------

	private void Update() {
		if (Input.GetKeyDown(trigger)) {
			Repaint();
		}
	}

	private void Start() {
		Repaint();
	}

	private void Repaint() {
		var output = new List<string>();

		output.Add($"Timer: {GameManager.Instance.gameTimer}\n");
		output.Add($"Flowers: {FindObjectsOfType<Flower>().Length}\n");

		text.text = string.Concat(output);
	}
}
