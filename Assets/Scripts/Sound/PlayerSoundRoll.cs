using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundRoll : MonoBehaviour {
	public AnimationCurve speedToPitch;
	public AudioSource rollSource;
	private Player player;
	private Rigidbody body;

	// ------------------------------

	private void Awake() {
		player = GetComponentInParent<Player>();
		body = GetComponentInParent<Rigidbody>();
	}

	private void Update() {
		rollSource.pitch = speedToPitch.Evaluate(body.velocity.magnitude);
	}
}
