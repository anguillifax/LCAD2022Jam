using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDroplet : MonoBehaviour
{
	public float waterValue = 5;
	public float lifeTime = 3;

	// ------------------------------

	private void Start() {
		Destroy(gameObject, lifeTime);
	}
}
