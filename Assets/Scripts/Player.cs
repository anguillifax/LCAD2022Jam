using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum State {
		Idle, Playing,
	}

	[Header("Main")]
	public State state;
	public bool debugMode;

	public float moveVel = 8;
	public float moveAccel = 30;

	[Header("Stats")]
	public float paintAmount;
	public float paintDecay = 3;

	public float fireAmount;
	public float fireDecay = 1;

	public float waterAmount;
	public float waterDecay = 0.3f;

	private Rigidbody body;
	private Vector3 targetVelocity;

	// ------------------------------

	private Vector2 InputClamped => Vector2.ClampMagnitude(
		new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1);

	// ------------------------------

	private void Awake() {
		body = GetComponent<Rigidbody>();
	}

	private void Update() {
		if (debugMode && Input.GetKeyDown(KeyCode.Backslash)) {
			transform.position = new Vector3(0, 2, 0);
			body.velocity = Vector3.zero;
			body.angularVelocity = Vector3.zero;
		}
	}

	private void FixedUpdate() {
		targetVelocity = moveVel * new Vector3(InputClamped.x, 0, InputClamped.y);
		body.AddForce(targetVelocity, ForceMode.Impulse);
	}
}
