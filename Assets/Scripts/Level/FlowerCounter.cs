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
		text.text = "0";
	}

	public void Update() {
		if (lastProgress != progress) {
			text.text = Mathf.RoundToInt(mapProgress.Evaluate(progress) * count).ToString();
			lastProgress = progress;
		}
	}
}
