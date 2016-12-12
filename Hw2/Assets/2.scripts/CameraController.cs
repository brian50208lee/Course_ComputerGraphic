using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Camera[] cameraArray;
	public Camera globalCamera;
	private int currentCameraIdx;


	// Use this for initialization
	void Start () {
		currentCameraIdx = 0;
		for (int i = 0; i < cameraArray.Length; i++) {
			cameraArray [i].enabled = i == currentCameraIdx? true: false;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("CameraSwitch")) {
			nextCamera ();
		}
	}

	void nextCamera(){
		int nextCameraIdx = currentCameraIdx + 1 >= cameraArray.Length ? 0 : currentCameraIdx + 1;
		cameraArray [currentCameraIdx].enabled = false;
		cameraArray [nextCameraIdx].enabled = true;
		currentCameraIdx = nextCameraIdx;
	}

	public void changeToGlobalCamera(){
		for (int i = 0; i < cameraArray.Length; i++) {
			cameraArray [i].enabled = false;
		}
		globalCamera.enabled = true;
	}
}
