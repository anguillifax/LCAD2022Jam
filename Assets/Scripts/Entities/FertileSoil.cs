using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FertileSoil : MonoBehaviour
{
	public GameObject prefabFlower;
	public Transform spawnPos;
	public bool isGrown;
	public GameObject blocker;
	public Transform meshSelect;

	// ------------------------------
	
	private void Awake() {
		foreach (Transform child in meshSelect) {
			child.gameObject.SetActive(false);
		}
		meshSelect.GetChild(UnityEngine.Random.Range(0, meshSelect.childCount)).gameObject.SetActive(true);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.GetIfExists<Player>(out var player)) {
			if (player.water.current > 0 && blocker == null) {
				isGrown = true;
				blocker = Instantiate(prefabFlower, spawnPos.transform.position, prefabFlower.transform.rotation);
				blocker.GetComponent<Flower>().owner = this;
				meshSelect.gameObject.SetActive(false);
			}
		}
	}
}
