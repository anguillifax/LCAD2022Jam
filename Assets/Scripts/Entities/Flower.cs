using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
	public FertileSoil owner;
	public float destroyDelay = 1;

	private Animator anim;

	// ------------------------------

	private void Awake() {
		anim = GetComponent<Animator>();

		GetComponent<Burnable>().burned.AddListener(Kill);
	}

	private void Kill() {
		if (owner) {
			owner.blocker = null;
		}
		anim.SetTrigger("Burn");
		Destroy(gameObject, destroyDelay);
		Destroy(this); // Prevent counting
	}
}
