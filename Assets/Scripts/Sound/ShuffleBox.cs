using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShuffleBox {
	public AudioSource outputSource;
	public AudioClip[] clips;

	public void Play() {
		if (clips.Length == 0) {
			return;
		}

		outputSource.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
		outputSource.Play();
	}
}
