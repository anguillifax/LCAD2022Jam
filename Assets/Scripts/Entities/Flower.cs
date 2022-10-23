using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {
	[Header("Common")]
	public FertileSoil owner;
	public float destroyDelay = 1;
	public Transform meshSelect;

	[Header("Sound")]
	public SoundApiAsset soundAsset;
	public LifetimeSound fire;
	public AudioSource oneshot;
	public bool muteStartSound;

	private Animator anim;
	private Burnable burnable;

	// ------------------------------

	private void Awake() {
		anim = GetComponent<Animator>();

		burnable = GetComponent<Burnable>();
		burnable.burned.AddListener(Kill);

		fire.Initialize(soundAsset, soundAsset.flowerFire);
		fire.BindCallback(burnable.ignited, burnable.burned);

		foreach (Transform child in meshSelect) {
			child.gameObject.SetActive(false);
		}
		meshSelect.GetChild(UnityEngine.Random.Range(0, meshSelect.childCount)).gameObject.SetActive(true);
	}

	private void Start() {
		if (!muteStartSound) {
			oneshot.PlayOneShot(soundAsset.flowerGrow);
		}
	}

	private void Update() {
		fire.Update();
	}

	private void Kill() {
		if (owner) {
			owner.blocker = null;
			owner.meshSelect.gameObject.SetActive(true);
		}
		anim.SetTrigger("Burn");
		
		Destroy(gameObject, destroyDelay);
		Destroy(this); // Prevent counting
	}

	private void OnTriggerEnter(Collider c) {
		if (c.GetComponent<Player>()) {
			oneshot.PlayOneShot(soundAsset.flowerContact);
		}
	}
}
