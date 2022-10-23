using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class LifetimeSound {
	public SoundApiAsset asset;
	public ShuffleBox start;
	public ShuffleBox stop;
	public AudioSource oneShot;
	public AudioSource loop;

	private float volumeTarget;
	private float curVolume;

	public void Initialize(SoundApiAsset asset, SoundApiAsset.LifetimeSoundConfig config) {
		this.asset = asset;
		start.clips = config.start;
		stop.clips = config.stop;
		loop.clip = config.loop;
		loop.volume = 0;
		curVolume = 0;
		loop.loop = true;
		loop.Play();
	}

	public void BindCallback(UnityEvent startEvent, UnityEvent stopEvent) {
		startEvent.AddListener(Begin);
		stopEvent.AddListener(End);
	}

	public void Begin() {
		start.Play(oneShot);
		volumeTarget = 1;
		Debug.Log("begin");
	}

	public void Update() {
		curVolume = Mathf.MoveTowards(curVolume, volumeTarget, 1f / asset.globalFadeDuration * Time.deltaTime);
		loop.volume = asset.globalFadeVolumeCurve.Evaluate(curVolume);
	}

	public void End() {
		stop.Play(oneShot);
		volumeTarget = 0;
		Debug.Log("end");
	}
}
