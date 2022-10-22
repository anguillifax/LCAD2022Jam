using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Burnable : MonoBehaviour {
	[Header("Config")]
	public float maxBurn = 0.5f;
	public float propagateRadius = 2;
	public UnityEvent ignited;
	public UnityEvent burned;
	public LayerMask mask = int.MaxValue;

	[Header("Live")]
	public float current;

	// ------------------------------

	private void OnEnable() {
		current = -1;
	}

	private void FixedUpdate() {
		if (current >= 0) {
			current += Time.fixedDeltaTime;
		}
		if (current > maxBurn) {
			burned.Invoke();
			Propagate();
			enabled = false;
		}
	}

	public void BeginBurn() {
		if (!enabled || current >= 0) {
			return;
		}
		current = 0;
		ignited.Invoke();
	}

	private void Propagate() {
		var hits = Physics.OverlapSphere(transform.position, propagateRadius, mask, QueryTriggerInteraction.Collide);
		foreach (Collider c in hits) {
			if (c.gameObject == gameObject) {
				continue;
			}
			if (c.GetIfExists<Burnable>(out var b)) {
				b.BeginBurn();
			}
		}
	}

	private void OnTriggerStay(Collider other) {
		if (other.GetIfExists<Player>(out var player)) {
			if (player.fire.current > 0) {
				BeginBurn();
			}
		}
	}

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, propagateRadius);
	}
}
