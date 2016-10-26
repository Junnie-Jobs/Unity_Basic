using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {

	public Transform cameraTransform;
	public GameObject fireObject;
	public float forwardPower = 20.0f;
	public float upPower = 5.0f;
	public Transform firePosition;
	PlayerState playerState = null;

	//	// Use this for initialization
		void Start () {
		playerState = GetComponent< PlayerState >();
		}

	// Update is called once per frame
	void Update () {

		if( playerState.isDead ) return;

		if( Input.GetButtonDown("Fire1") ) {

			GameObject obj = Instantiate( fireObject ) as GameObject;
			//			GameObject obj = GetComponent<GameObject>();

			obj.transform.position = firePosition.position;
			obj.GetComponent<Rigidbody>().velocity = cameraTransform.forward * forwardPower + Vector3.up * upPower;
		}
	}
}
