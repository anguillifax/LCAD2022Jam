using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintSpawner : MonoBehaviour {
	public GameObject prefab;
	public Transform target;
	public GameObject spawned;
	public float delayMax = 3;
	public float delay;

	private void Update() {
		if (spawned == null) {
			delay += Time.deltaTime;
			if (delay > delayMax) {
				spawned = Instantiate(prefab, target.transform.position, Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
				delay = 0;
			}
		}
	}
}
