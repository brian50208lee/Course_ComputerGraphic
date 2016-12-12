using UnityEngine;
using System.Collections;

public class MyHP : MonoBehaviour {
	
	public GameObject playerTank;
	public GameObject hpBar;
	public GameObject cameraController;

	private float HP = 100f;



	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision collision) {//碰撞發生時呼叫
		if (collision.gameObject.tag == "Terrain") {
			return;
		}

		//碰撞後產生爆炸
		if (collision.gameObject.tag == "EnemyBullet") {
			HP -= 10;
		}
		Vector3 scale = hpBar.transform.localScale;
		hpBar.transform.localScale = new Vector3(scale.x, HP/100.0f*0.5f, scale.z);

		if(HP <= 0){
			CameraController controller = cameraController.GetComponent<CameraController> ();
			controller.changeToGlobalCamera ();
			Destroy(playerTank);
		}
	}
}
