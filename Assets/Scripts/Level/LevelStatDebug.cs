using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelStatDebug : MonoBehaviour {
	public KeyCode trigger = KeyCode.Alpha0;

	public TextMeshProUGUI text;
	public float periodicTimerMax = 2;
	public float periodicTimer = 0;

	// ------------------------------

	private void Update() {
		if (Input.GetKeyDown(trigger)) {
			Repaint();
		}

		periodicTimer += Time.deltaTime;
		if (periodicTimer > periodicTimerMax) {
			periodicTimer %= periodicTimerMax;
			Repaint();
		}
	}

	private void Start() {
		Repaint();
	}

	private void Repaint() {
		var output = new List<string>();

		if (GameManager.Session) {
			output.Add("[Session Active]\n");

			if (GameManager.Session.enableTimer) {
				output.Add($"Timer: {GameManager.Session.gameTimer}\n");
			} else {
				output.Add("Timer: <disabled>\n");
			}

			output.Add($"Flowers: {FindObjectsOfType<Flower>().Length}\n");
		}

		text.text = string.Concat(output);
	}
}
