using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	Vector3 tempVec = new Vector3();
	public Car car;
	public float offset;
	public Animator animator;
    public float smoothSpeed;
    private float YOffset;

    void Start()
    {
        YOffset = transform.position.y;
    }
    

	void LateUpdate () {
		tempVec = new Vector3 (car.transform.position.x, YOffset, car.transform.position.z-offset);
        Vector3 smoothVec = Vector3.Lerp(transform.position, tempVec, smoothSpeed);
        transform.position = smoothVec;
    }
}
