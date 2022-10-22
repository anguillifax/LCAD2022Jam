using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlowerCounter : MonoBehaviour {
	public TextMeshProUGUI text;
	public int count;

	// ------------------------------

	public void Start() {
		count = FindObjectsOfType<Flower>().Length;
		text.text = count.ToString();
	}
}
