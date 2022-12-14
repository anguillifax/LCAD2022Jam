using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlowerCounter : MonoBehaviour {
	public TextMeshProUGUI text;
	public int count;

	[Range(0, 1)]
	public float progress;
	private float lastProgress;
	public AnimationCurve mapProgress;

	// ------------------------------

	private void OnEnable() {
		count = FindObjectsOfType<Flower>().Length;
		GameManager.Instance.highscore = Mathf.Max(count, GameManager.Instance.highscore);
		text.text = "0";
	}

	public void Update() {
		if (lastProgress != progress) {
			if (progress <= 1) {
				text.text = Mathf.RoundToInt(mapProgress.Evaluate(progress) * (count - 1)).ToString();
			} else {
				text.text = count.ToString();
			}
			lastProgress = progress;
		}
	}
}
