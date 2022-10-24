using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSpawner : MonoBehaviour {
	[Serializable]
	public class MaterialEffects {
		public GameObject prefabAdd;
		public GameObject prefabTrail;
		public GameObject prefabRemove;
	}

	[Header("Common")]
	public Vector3 offset;
	public float paintCastDistance = 50;
	public LayerMask paintCastMask;

	[Header("Material Blocks")]
	public MaterialEffects paint;
	public MaterialEffects fire;
	public MaterialEffects water;
	public GameObject[] paintBlobOptions;
	public ParticleSystem fireParticles;

	[Header("Force Field")]
	public Material forcefieldPaint;
	public Material forcefieldWater;
	public Material forcefieldFire;
	public MeshRenderer forcefieldRenderer;

	private Player player;

	// ------------------------------

	private void Awake() {
		player = GetComponent<Player>();

		void Bind(MaterialEffects effects, Player.DecayStat stat) {
			stat.added.AddListener(() => SpawnIfExists(effects.prefabAdd));
			stat.changed.AddListener(_ => SpawnIfExists(effects.prefabTrail));
			stat.removed.AddListener(() => SpawnIfExists(effects.prefabRemove));
		}
		Bind(paint, player.paint);
		Bind(fire, player.fire);
		Bind(water, player.water);

		player.fire.added.AddListener(fireParticles.Play);
		player.fire.removed.AddListener(fireParticles.Stop);
		fireParticles.Stop();

		player.paint.changed.AddListener(_ => SpawnPaint());
	}

	private void Update() {
		bool hasPaint = player.paint.current > 0;
		bool hasFire = player.fire.current > 0;
		bool hasWater = player.water.current > 0;

		if (!hasPaint && !hasFire && !hasWater) {
			forcefieldRenderer.enabled = false;
		} else {
			forcefieldRenderer.enabled = true;
			if (hasFire) {
				forcefieldRenderer.material = forcefieldFire;
			} else if (hasWater) {
				forcefieldRenderer.material = forcefieldWater;
			} else if (hasPaint) {
				forcefieldRenderer.material = forcefieldPaint;
			}
		}
	}

	private void SpawnIfExists(GameObject prefab) {
		if (prefab) {
			Instantiate(prefab, transform.position + offset, prefab.transform.rotation);
		}
	}

	private void SpawnPaint() {
		var prefab = paintBlobOptions[UnityEngine.Random.Range(0, paintBlobOptions.Length)];
		if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, paintCastDistance, paintCastMask, QueryTriggerInteraction.Ignore)) {
			Instantiate(prefab, hit.point, Quaternion.Euler(-90, UnityEngine.Random.Range(0, 360), 0));
		}
	}
}
