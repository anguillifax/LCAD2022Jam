using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifetimeSound : MonoBehaviour {
	[Header("Input Files")]
	public ShuffleBox start;
	public AudioSource loop;
	public ShuffleBox end;

	[Header("Curve Maps")]
	[Tooltip("How long it takes to fade in and out in seconds.")]
	public float fadeDuration = 0.5f;
	[Tooltip("Shared curve for fade-in and fade-out volume.")]
	public AnimationCurve volumeFade = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
	[Tooltip("Maps speed of the objects in m/s to pitch. A quick pace is ~8 m/s.")]
	public AnimationCurve speedToPitch = new AnimationCurve(new Keyframe(0, 1), new Keyframe(8, 1));

	[Header("Associated Rigidbody")]
	public Rigidbody body;

	private float volumeTarget;
	private float curVolume;

	private void OnEnable() {
		curVolume = 0;
		loop.volume = 0;
	}

	public void Begin() {
		start.Play();
		volumeTarget = 1;
	}

	private void Update() {
		curVolume = Mathf.MoveTowards(curVolume, volumeTarget, 1f / fadeDuration * Time.deltaTime);
		loop.volume = volumeFade.Evaluate(curVolume);
		loop.pitch = speedToPitch.Evaluate(body.velocity.magnitude);
	}

	public void End() {
		end.Play();
		volumeTarget = 0;
	}
}
