using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientUprightConstant : MonoBehaviour {
	private void LateUpdate() {
		transform.rotation = Quaternion.identity;
	}
}
