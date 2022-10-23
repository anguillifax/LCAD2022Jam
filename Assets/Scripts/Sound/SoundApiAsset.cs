using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LCAD Jam/Create Sound API Asset", order = 500)]
public class SoundApiAsset : ScriptableObject {
	[Serializable]
	public struct LifetimeSoundConfig {
		public AudioClip[] start;
		public AudioClip loop;
		public AudioClip[] stop;
	}

	[Header("Global")]
	public float globalFadeDuration = 0.5f;
	public AnimationCurve globalFadeVolumeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	[Header("Player")]
	public LifetimeSoundConfig playerFire;
	public LifetimeSoundConfig playerWater;
	public LifetimeSoundConfig playerPaint;
	public AudioClip[] playerGroundNormalStep;
	public AudioClip[] playerGroundSoilStep;
	public AudioClip playerHardCollide;
	public float playerDistancePerStep = 0.6f;
	public AnimationCurve playerStepSpeedToPitch = new AnimationCurve(new Keyframe(0, 1), new Keyframe(8, 1));
	public AnimationCurve playerForceToVolume = new AnimationCurve(new Keyframe(0, 0), new Keyframe(60, 1));

	[Header("Trash")]
	public LifetimeSoundConfig trashFire;
	public AudioClip trashContactHit;
	public AudioClip trashContactTraverse;

	[Header("Flower")]
	public AudioClip flowerGrow;
	public LifetimeSoundConfig flowerFire;
	public AudioClip flowerContact;

	[Header("Level")]
	public AudioClip levelTimerBegin;
	public AudioClip levelTimerHalfway;
	public AudioClip levelTimerLastSeconds;
	public AudioClip levelTimerEnd;

	[Header("Menu")]
	public AudioClip menuClick;
	public AudioClip menuOpen;
	public AudioClip menuClose;
}
