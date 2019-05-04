using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class portalScript : MonoBehaviour {

	public Material[] mats;

	const int inside = (int) CompareFunction.NotEqual;
	const int outside = (int) CompareFunction.Equal;

	public Transform device;
	bool wasInside; // default to false
	bool areInside;

	// Use this for initialization
	void Start () {
		setMaterials (false);
	}// end Start

	void setMaterials(bool fullRender) {
		var stencilTest = fullRender ? inside : outside;
		setMaterialMasks ((int)stencilTest);
	}

	bool getIsInside() {
		Vector3 pos = transform.InverseTransformPoint (device.position);
		return pos.z >= 0? true: false;
	}

	void OnTriggerStay(Collider other) {
		if (other.transform != device)
			return;
		bool isInFront = getIsInside ();
		if ((isInFront && !wasInside) || (wasInside && !isInFront)) {
			areInside = !areInside;
			setMaterials (areInside);
		}
		wasInside = isInFront;
	
	}// end onCollide

	void OnTriggerEnter(Collider other){
		if (other.transform != device)
			return;
		wasInside = getIsInside ();
	}

	// setMaterialMasks
	// helper function for onCollide
	private void setMaterialMasks(int mask) {
		foreach (var mat in mats) {
			mat.SetInt ("_StencilTest", mask);
		}
	}// end setMaterialMasks


	void onDestroy() {
		setMaterials (true);
	}

	// Update is called once per frame
	void Update () {
		
	}// end Update
}
