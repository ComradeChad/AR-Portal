using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class passPortal : MonoBehaviour {

	public Material[] mats;

	const int inside = (int) CompareFunction.NotEqual;
	const int outside = (int) CompareFunction.Equal;

	// Use this for initialization
	void Start () {
		setMaterialMasks(outside);
	}// end Start

	void OnTriggerStay(Collider other) {
		if (other.name != "Main Camera")
			return;

		if (transform.position.z > other.transform.position.z)
			setMaterialMasks(outside);
		else
			setMaterialMasks(inside);
	
	}// end onCollide

	// setMaterialMasks
	// helper function for onCollide
	private void setMaterialMasks(int mask) {
		foreach (var mat in mats) {
			mat.SetInt ("_StencilTest", mask);
		}
	}// end setMaterialMasks


	void onDestroy() {
		setMaterialMasks (inside);
	}

	// Update is called once per frame
	void Update () {
		
	}// end Update
}
