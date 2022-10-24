using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreCounter : MonoBehaviour {
	public TextMeshProUGUI text;

	private void OnEnable() {
		text.text = GameManager.Instance.highscore.ToString();
	}
}
