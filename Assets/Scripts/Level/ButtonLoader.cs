using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoader : MonoBehaviour {
	public SoundApiAsset asset;
	private void Awake() {
		GetComponent<AudioSource>().clip = asset.menuClick;
	}
}
