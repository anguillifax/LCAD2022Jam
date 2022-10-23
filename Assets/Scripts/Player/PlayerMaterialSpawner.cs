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

		player.paint.changed.AddListener(_ => SpawnPaint());
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
