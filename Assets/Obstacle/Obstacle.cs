using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	public CameraController cam;
    private GameObject curCamera;
	private string temp="";
	private GameObject car;

	// Use this for initialization
    void Start()
    {
        curCamera = GameObject.FindWithTag("MainCamera");
        car = GameObject.FindWithTag("Player");
    }
    
	void OnTriggerEnter(Collider col){
		if(col.gameObject.name!=temp){
			Main.lives--;
			if (Main.lives == 0) {
				this.gameObject.GetComponent<BoxCollider> ().isTrigger = false;
                this.gameObject.AddComponent (typeof(Rigidbody));
                this.gameObject.GetComponent<Rigidbody> ().AddForce (0, 5, 6, ForceMode.Impulse);
			} else if (Main.lives != 0 && Car.gameover == false) {
                Physics.IgnoreCollision(car.GetComponent<Collider>(), GetComponent<Collider>());
                car.transform.Find("Shower").GetComponent<ParticleSystem>().Play();
                this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                this.gameObject.AddComponent(typeof(Rigidbody));
                this.gameObject.GetComponent<Rigidbody>().AddForce(0, 10, 16, ForceMode.Impulse);
                curCamera.GetComponent<Animator> ().Play ("CameraAnimation");
            }
			temp = col.gameObject.name;}
	}
}
