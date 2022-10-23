using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShuffleBox {
	public AudioClip[] clips;

	public void Play(AudioSource target) {
		if (clips.Length == 0) {
			return;
		}

		target.PlayOneShot(clips[UnityEngine.Random.Range(0, clips.Length)]);
	}
}
