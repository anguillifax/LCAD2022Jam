using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndDestroyCallback : MonoBehaviour
{
	public GameObject prefab;
	public float destroyDelay = 0;

	public void Invoke() {
		if (prefab) {
			Instantiate(prefab, transform.position, prefab.transform.rotation);
		}
		Destroy(gameObject, destroyDelay);
	}
}
