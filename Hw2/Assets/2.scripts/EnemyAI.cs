using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public GameObject playerTank;
	public GameObject enemyTank;

	public GameObject hpBar;
	public GameObject firePoint;
	public Rigidbody bullet;

	private int enemySpeed = 8;
	private int rotateSpeed = 1;
	private float bulletSeed = 5;
	private float bulletCD = 2f;
	private float HP = 100f;

	private float cd;


	// Use this for initialization
	void Start () {
		cd = bulletCD;
	}

	// Update is called once per frame
	void Update () {
		cd = cd - Time.smoothDeltaTime <= 0? 0 :cd - Time.smoothDeltaTime ;

		Vector3 forward = enemyTank.transform.forward;
		Vector3 direction = (playerTank.transform.position - enemyTank.transform.position);
		float distance = direction.sqrMagnitude;

		Vector2 forward2D = new Vector2 (forward.x, forward.z).normalized;
		Vector2 direction2D = new Vector2 (direction.x, direction.z).normalized;

		float rotate = Vector3.Cross (forward2D, direction2D).z;
		float face = Vector3.Dot (forward2D, direction2D);

		//track
		if (rotate > 0.05) {
			enemyTank.transform.Rotate (0, rotateSpeed, 0);
		} else if (rotate < -0.05) {
			enemyTank.transform.Rotate (0, -1 * rotateSpeed, 0);
		} else if (distance < 10000) {
			if (cd != 0) {
				return;
			}

			Rigidbody shoot = (Rigidbody)Instantiate (bullet, firePoint.transform.position, firePoint.transform.rotation);
			shoot.velocity = direction;
			Physics.IgnoreCollision (firePoint.transform.root.GetComponent<Collider> (), shoot.GetComponent<Collider> ());
			cd = bulletCD;
		} else if (distance < 50000) {
			enemyTank.transform.Translate (0, 0, (-1) * enemySpeed * Time.smoothDeltaTime);
		}
	}

	void OnCollisionEnter (Collision collision) {//碰撞發生時呼叫
		if (collision.gameObject.tag == "Terrain") {
			return;
		}

		//碰撞後產生爆炸
		if (collision.gameObject.tag == "Laser") {
			HP -= 10;
		} else if (collision.gameObject.tag == "Sphere") {
			HP -= 30;
		}
		Vector3 scale = hpBar.transform.localScale;
		hpBar.transform.localScale = new Vector3(scale.x, HP/100.0f*0.5f, scale.z);

		if(HP <= 0){
			Destroy(enemyTank);
		}
	}
	
}
