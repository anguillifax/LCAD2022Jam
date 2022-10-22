using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public enum TrackMode {
		Player, ReflectAuto, ReflectManual,
	}

	[Header("Targets")]
	public Transform targetPlayer;
	public Transform targetLevelOrigin;

	[Header("Rigs")]
	public Transform rigRoot;
	public Transform rigOffset;

	[Header("General")]
	public TrackMode mode;
	public float rootSmoothDamp = 0.3f;
	public float rotateAutoSpeed = 5f;
	public float rotateSpeed = 20f;
	public float rotateResetSmoothDamp = 0.3f;

	private Vector3 rigRefVel;
	private Vector3 rigRefAng;

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
		rigRoot.eulerAngles = Vector3.SmoothDamp(rigRoot.eulerAngles, Vector3.zero, ref rigRefAng, rotateResetSmoothDamp);
	}

	private void UpdateReflectAuto() {

	}

	private void UpdateReflectManual() {

	}

	// ------------------------------

	public void TrackPlayer() {
		mode = TrackMode.Player;
	}

	public void TrackWorld() {
		mode = TrackMode.ReflectAuto;
	}
}
