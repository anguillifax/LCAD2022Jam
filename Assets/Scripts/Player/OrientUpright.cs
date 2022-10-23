using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientUpright : MonoBehaviour {
	public AnimationCurve angleMap;
	public Rigidbody body;

	private void LateUpdate() {
		Vector3 v = body.velocity;
		v.y = 0;
		Vector3 dir = v.sqrMagnitude > 0 ? v.normalized : Vector3.forward;
		Vector3 axis = Vector3.Cross(Vector3.up, dir);
		Debug.DrawRay(transform.position + Vector3.up, dir, Color.yellow);
		Debug.DrawRay(transform.position + Vector3.up, axis, Color.red);

		transform.rotation = Quaternion.AngleAxis(angleMap.Evaluate(v.magnitude), axis);
	}
}
