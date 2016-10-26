using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public float sensitivity = 700.0f; 
	float rotationX;
	float rotationY;

	PlayerState playerState = null;

	// Use this for initialization
	void Start () {
		playerState = transform.parent.GetComponent< PlayerState >();
	}

	// Update is called once per frame
	void Update () {

		if( playerState.isDead ) return;

		float mouseMoveValueX = Input.GetAxis( "Mouse X" );
		float mouseMoveValueY = Input.GetAxis( "Mouse Y" );

		rotationY += mouseMoveValueX * sensitivity * Time.deltaTime; 
		rotationX += mouseMoveValueY * sensitivity * Time.deltaTime;

		rotationX %= 360; 
		rotationY %= 360;

		transform.eulerAngles = new Vector3( -rotationX, rotationY, 0.0f );
	}
}
