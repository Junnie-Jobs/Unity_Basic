using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public GameObject groundExplosionParticle;
	public GameObject explosionParticle;
	public AudioClip clip;

	void OnCollisionEnter( Collision collision ) {

		int collisionLayer = collision.gameObject.layer;
		AudioManager.instance.PlaySfx( clip );

		if( collisionLayer == LayerMask.NameToLayer("Ground") ) {
			GameObject particleObj = Instantiate(groundExplosionParticle) as GameObject;
			particleObj.transform.position = transform.position; 
		}
		else {
			GameObject particleObj = Instantiate(explosionParticle) as GameObject;
			particleObj.transform.position = transform.position; 
		}

		Destroy( gameObject ); 
	}
}
