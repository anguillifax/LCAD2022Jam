using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilObstruction : MonoBehaviour
{
	public FertileSoil soil;

	private void Awake() {
		GetComponent<Burnable>().burned.AddListener(ReleaseBlockage);
	}

	private void ReleaseBlockage() {
		if (soil) {
			soil.blocker = null;
		}
	}
}
