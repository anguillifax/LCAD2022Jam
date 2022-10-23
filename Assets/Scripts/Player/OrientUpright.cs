using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientUpright : MonoBehaviour {
	private void LateUpdate() {
		transform.rotation = Quaternion.identity;
	}
}
