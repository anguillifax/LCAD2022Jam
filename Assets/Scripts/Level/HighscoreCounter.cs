using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreCounter : MonoBehaviour {
	public TextMeshProUGUI text;

	private void Awake() {
		text.text = GameManager.highscore.ToString();
	}
}
