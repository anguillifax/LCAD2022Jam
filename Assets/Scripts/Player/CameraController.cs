using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public enum TrackMode {
		Player, ReflectAuto, ReflectManual,
	}

	[Header("Targets")]
	public Transform targetPlayer;
	public Transform targetLevelOrigin;

	[Header("Rigs")]
	public Transform rigRoot;
	public Transform rigOffset;
	public Transform rigZoom;

	[Header("Config")]
	public float rootSmoothDamp = 0.3f;
	public float rootMoveDamp = 0.8f;
	public float rotateAutoSpeed = 5f;
	public float rotateSpeed = 20f;
	public float rotateSpeedTargetDamp = 0.3f;
	public float turntableResetDamp = 0.3f;
	public float farDistance = -10;
	public float farSmoothDamp = 0.3f;
	public float idleTimerMax = 0.2f;

	[Header("Live")]
	public TrackMode mode;
	public float curRotateSpeed;
	public float idleTimer;

	private Vector3 rigRefVel;
	private float refTurntable;
	private float refZoom;
	private float refRotate;

	// ------------------------------

	private float TurntableAngle {
		get => rigRoot.eulerAngles.y;
		set {
			var b = rigRoot.eulerAngles;
			b.y = value;
			rigRoot.eulerAngles = b;
		}
	}

	private float ZoomOffset {
		get => rigZoom.localPosition.z;
		set {
			var b = rigZoom.localPosition;
			b.z = value;
			rigZoom.localPosition = b;
		}
	}

	private float InputRotate => Input.GetAxisRaw("Horizontal");

	// ------------------------------

	private void Awake() {
	}

	private void Start() {
		TrackPlayer();
	}

	private void LateUpdate() {
		switch (mode) {
			case TrackMode.Player: UpdatePlayer(); break;
			case TrackMode.ReflectAuto: UpdateReflectAuto(); break;
			case TrackMode.ReflectManual: UpdateReflectManual(); break;
		}
	}

	private void UpdatePlayer() {
		rigRoot.position = Vector3.SmoothDamp(rigRoot.position, targetPlayer.position, ref rigRefVel, rootSmoothDamp);
		TurntableAngle = Mathf.SmoothDamp(TurntableAngle, 0, ref refTurntable, turntableResetDamp);

		curRotateSpeed = 0;
		idleTimer = 0;
		ZoomOffset = Mathf.SmoothDamp(ZoomOffset, 0, ref refZoom, farSmoothDamp);
	}

	private void UpdateReflectAuto() {
		rigRoot.position = Vector3.SmoothDamp(rigRoot.position, targetLevelOrigin.position, ref rigRefVel, rootMoveDamp);

		curRotateSpeed = Mathf.SmoothDamp(curRotateSpeed, rotateAutoSpeed, ref refRotate, rotateSpeedTargetDamp);
		TurntableAngle += curRotateSpeed * Time.deltaTime;

		ZoomOffset = Mathf.SmoothDamp(ZoomOffset, farDistance, ref refZoom, farSmoothDamp);

		if (InputRotate == 0) {
			idleTimer += Time.deltaTime;
		}
		if (idleTimer >= idleTimerMax && InputRotate != 0) {
			mode = TrackMode.ReflectManual;
		}
	}

	private void UpdateReflectManual() {
		rigRoot.position = Vector3.SmoothDamp(rigRoot.position, targetLevelOrigin.position, ref rigRefVel, rootMoveDamp);

		float speedTarget = Input.GetAxisRaw("Horizontal") * rotateSpeed;
		curRotateSpeed = Mathf.SmoothDamp(curRotateSpeed, speedTarget, ref refRotate, rotateSpeedTargetDamp);
		TurntableAngle += curRotateSpeed * Time.deltaTime;

		idleTimer = 0;
		ZoomOffset = Mathf.SmoothDamp(ZoomOffset, farDistance, ref refZoom, farSmoothDamp);
	}

	// ------------------------------

	public void TrackPlayer() {
		mode = TrackMode.Player;
	}

	public void TrackWorld() {
		mode = TrackMode.ReflectAuto;
	}
}
