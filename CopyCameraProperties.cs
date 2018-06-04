using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Jason Jerome 2018

	License: MIT

	Usage: 
	1) Attach to a camera game object.
	2) Select origin camera to copy from.
	3) Select target camera to copy to.
	4) Select copy mode.
	Optional : With some copy modes, you can adjust the ratio of copying with the Reduction Factor float variable.
 */

public class CopyCameraProperties : MonoBehaviour {

	[Header("Primary References")]
	public GameObject origin;
	public GameObject target;
	public enum Options {
		CopyTransform,
		CopyFOV,
		CopyTransformAndFOV,
		CopyTransformReduced,
		CopyTransformAndFOVReduced
	};
	public Options copyMode;

	[Header("Extra Options")]
	[RangeAttribute(0f,1f)]
	public float reductionFactor;


	// Internal cache variables.
	private Vector3 originalPos;
	private Camera originCam;
	private Camera targetCam;

	

	// Use this for initialization
	private void Start () {
		Init();
		CameraLoop();
	}
	
	// Update is called once per frame
	private void Update () {
		CameraLoop();
	}
	 
	// Initialize and cache camera/object data.
	private void Init() {
		originalPos = this.transform.position;
		if (copyMode == Options.CopyFOV || copyMode == Options.CopyTransformAndFOV || copyMode == Options.CopyTransformAndFOVReduced) {
			originCam = origin.GetComponent<Camera>();
			targetCam = target.GetComponent<Camera>();
		}
	}

	public void CameraLoop() {
		switch(copyMode) {
			// Copies camera FOV only.
			case Options.CopyFOV:
				targetCam.fieldOfView = originCam.fieldOfView;
				break;
			// Copies camera Position only. (1-to-1 copy of camera transform)
			case Options.CopyTransform:
				target.transform.position = origin.transform.position;
				break;
			// Copies camera Position with a property to reduce the copy amount. (reductionFactor of 1f will be a 1-to-1 copy of camera transform)
			case Options.CopyTransformReduced:
				target.transform.position = new Vector3(origin.transform.position.x * reductionFactor, origin.transform.position.y, origin.transform.position.z);
				break;
			// Copies camera FOV and Position. (1-to-1 copy of camera transform)
			case Options.CopyTransformAndFOV:
				targetCam.fieldOfView = originCam.fieldOfView;
				target.transform.position = origin.transform.position;
				break;
			// Copies camera FOV and Position with a property to reduce the copy amount. (reductionFactor of 1f will be a 1-to-1 copy of camera transform)
			case Options.CopyTransformAndFOVReduced:
				targetCam.fieldOfView = originCam.fieldOfView;
				target.transform.position = new Vector3(origin.transform.position.x * reductionFactor, origin.transform.position.y, origin.transform.position.z);
				break;

			// Defaults to camera transform copying only.
			default:
				target.transform.position = origin.transform.position;
				break;
		}
	}

}
