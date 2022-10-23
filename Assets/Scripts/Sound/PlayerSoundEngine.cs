using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundEngine : MonoBehaviour {
	[Header("Config")]
	public SoundApiAsset asset;
	public LifetimeSound fire;
	public LifetimeSound water;
	public LifetimeSound paint;
	public ShuffleBox stepNormal;
	public ShuffleBox stepSoil;
	public AudioSource stepSource;
	public AudioSource hardCollide;
	public AudioSource[] speedPitchSource;
	public LayerMask soilMask;
	public float soilCastDistance = 2;

	[Header("Live")]
	public float travelDistance = 0;

	private Player player;
	private Rigidbody body;

	// ------------------------------

	private void Awake() {
		player = GetComponent<Player>();
		body = GetComponent<Rigidbody>();

		paint.Initialize(asset, asset.playerPaint);
		fire.Initialize(asset, asset.playerFire);
		water.Initialize(asset, asset.playerWater);

		static void Bind(Player.DecayStat stat, LifetimeSound sound) => sound.BindCallback(stat.added, stat.removed);
		Bind(player.paint, paint);
		Bind(player.fire, fire);
		Bind(player.water, water);

		stepNormal.clips = asset.playerGroundNormalStep;
		stepSoil.clips = asset.playerGroundSoilStep;
		hardCollide.clip = asset.playerHardCollide;
	}

	private void Update() {
		fire.Update();
		water.Update();
		paint.Update();
	}

	private void FixedUpdate() {
		float speedPitch = asset.playerStepSpeedToPitch.Evaluate(body.velocity.magnitude);
		foreach (var source in speedPitchSource) {
			source.pitch = speedPitch;
		}

		travelDistance += body.velocity.magnitude * Time.fixedDeltaTime;
		if (travelDistance > asset.playerDistancePerStep) {
			travelDistance -= asset.playerDistancePerStep;
			if (Physics.Raycast(transform.position, Vector3.down, soilCastDistance, soilMask, QueryTriggerInteraction.Collide)) {
				stepSoil.Play(stepSource);
			} else {
				stepNormal.Play(stepSource);
			}
		}
	}

	private void OnCollisionEnter(Collision c) {
		hardCollide.volume = asset.playerForceToVolume.Evaluate(c.impulse.magnitude);
		//Debug.Log("Impulse: " + c.impulse.magnitude);
		hardCollide.Play();
	}
}
