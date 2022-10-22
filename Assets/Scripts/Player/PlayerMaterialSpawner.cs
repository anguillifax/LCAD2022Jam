using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMaterialSpawner : MonoBehaviour
{
	[Serializable]
	public class MaterialEffects {
		public GameObject prefabAdd;
		public GameObject prefabTrail;
		public GameObject prefabRemove;
	}

	public Vector3 offset;

	public MaterialEffects paint;
	public MaterialEffects fire;
	public MaterialEffects water;

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
	}

	private void SpawnIfExists(GameObject prefab) {
		if (prefab) {
			Instantiate(prefab, transform.position + offset, prefab.transform.rotation);
		}
	}
}
