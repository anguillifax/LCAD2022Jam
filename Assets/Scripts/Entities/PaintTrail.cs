using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTrail : MonoBehaviour {
	private void OnTriggerStay(Collider other) {
		if (other.GetIfExists<Player>(out var player)) {
			if (player.water.current > 0) {
				BeginDestroy();
			}
		}
	}

	public void BeginDestroy() {
		Destroy(gameObject);
	}
}
