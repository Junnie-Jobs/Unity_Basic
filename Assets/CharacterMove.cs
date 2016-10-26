using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

	CharacterController characterController = null;
	public Transform cameraTransform;
	public float moveSpeed = 10.0f;
	public float jumpSpeed = 10.0f;
	public float gravity = -20.0f;
	float yVelocity = 0.0f;

	PlayerState playerState = null;


	// Use this for initialization
	void Start () {
		characterController = GetComponent<CharacterController>();
		playerState = GetComponent< PlayerState >();
	}

	// Update is called once per frame
	void Update () {

		if( playerState.isDead ) return;

		float x = Input.GetAxis( "Horizontal" );
		float z = Input.GetAxis( "Vertical" );
		Vector3 moveDirection = new Vector3( x, 0, z );
		moveDirection = cameraTransform.TransformDirection( moveDirection );
		moveDirection *= moveSpeed;

		if( Input.GetButtonDown("Jump") ) {
			yVelocity = jumpSpeed;
		}
		if( characterController.isGrounded == true )
			yVelocity = 0.0f;

		yVelocity += (gravity * Time.deltaTime);
		moveDirection.y = yVelocity;

		characterController.Move( moveDirection * Time.deltaTime );
	}
}
