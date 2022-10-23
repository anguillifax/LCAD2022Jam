using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashProp : MonoBehaviour {
	public bool isSolid;
	public SoundApiAsset soundAsset;
	public AudioSource oneshot;
	public LifetimeSound fire;

	// ------------------------------

	private void Awake() {
		fire.Initialize(soundAsset, soundAsset.trashFire);
		var burnable = GetComponent<Burnable>();
		fire.BindCallback(burnable.ignited, burnable.burned);
	}

	private void Update() {
		fire.Update();
	}

	private void OnTriggerEnter(Collider c) {
		if (!isSolid && c.GetComponent<Player>()) {
			oneshot.PlayOneShot(soundAsset.trashContactTraverse);
		}
	}

	private void OnCollisionEnter(Collision c) {
		if (isSolid && c.gameObject.GetComponent<Player>()) {
			oneshot.PlayOneShot(soundAsset.trashContactHit);
		}
	}
}
