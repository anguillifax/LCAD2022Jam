using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {
	public enum State {
		Idle, Playing,
	}

	[Serializable]
	public class DecayStat {
		public float current;
		public float timeDecay;
		public float distanceDecay;
		public float reapplyThreshold = 8;
		public UnityEvent added;
		public UnityEvent<float> changed;
		public UnityEvent removed;

		public DecayStat(float timeDecay, float distanceDecay) {
			this.timeDecay = timeDecay;
			this.distanceDecay = distanceDecay;
		}

		public void Update(float deltaTime, float deltaDistance, float customDecay = 0) {
			if (current <= 0) {
				current = 0;
				return;
			}

			current -= timeDecay * deltaTime;
			current -= distanceDecay * deltaDistance;
			current -= customDecay;

			if (current <= 0) {
				current = 0;
				removed.Invoke();
			} else {
				changed.Invoke(current);
			}
		}

		public void Reset() {
			current = 0;
		}

		public void SetAmount(float amount) {
			float last = current;
			current = Mathf.Max(current, amount);
			if (current - last > reapplyThreshold) {
				added.Invoke();
			}
		}

		public void RemoveAll() {
			current = 0;
			removed.Invoke();
		}
	}

	// ------------------------------

	[Header("Main")]
	public State state;
	public bool debugMode;

	public float moveVel = 8;
	public float paintBurnRate = 8;

	[Header("Stats")]
	public DecayStat paint = new DecayStat(0, 3);
	public DecayStat fire = new DecayStat(0.5f, 2);
	public DecayStat water = new DecayStat(0.3f, 0.5f);

	private Rigidbody body;
	private Vector3 targetVelocity;

	// ------------------------------

	private Vector2 InputClamped => Vector2.ClampMagnitude(
		new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1);

	// ------------------------------

	private void Awake() {
		body = GetComponent<Rigidbody>();
	}

	private void Start() {
		paint.Reset();
		fire.Reset();
		water.Reset();
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

		float distance = body.velocity.magnitude * Time.fixedDeltaTime;
		if (fire.current > 0) {
			paint.Update(Time.fixedDeltaTime, distance, paintBurnRate * Time.fixedDeltaTime);
		} else {
			paint.Update(Time.fixedDeltaTime, distance);
		}
		fire.Update(Time.fixedDeltaTime, distance);
		water.Update(Time.fixedDeltaTime, distance);
	}

	private void OnTriggerStay(Collider other) {
		if (other.GetIfExists<FireSource>(out var fireSource)) {
			water.RemoveAll();
			fire.SetAmount(fireSource.amount);
		}
		if (other.GetIfExists<Pond>(out var pond)) {
			fire.RemoveAll();
			paint.RemoveAll();
			water.SetAmount(pond.amount);
		}
		if (other.GetIfExists<PaintBlob>(out var paintBlob)) {
			water.RemoveAll();
			paint.SetAmount(paintBlob.amount);
			paintBlob.BeginDestroy();
		}
	}
}

