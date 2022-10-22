using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBlob : MonoBehaviour
{
	public float amount = 100;

	// ------------------------------

	public void BeginDestroy() {
		Destroy(gameObject);
	}
}
