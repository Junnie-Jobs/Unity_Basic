using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	enum ENEMYSTATE {
		NONE = -1, IDLE = 0, MOVE, ATTACK, DAMAGE, DEAD
	}

	public AudioClip clip;

	public GameObject explosionParticle = null;
	public GameObject deadObject = null;

	int healthPoint = 5;
	public int score = 10;

	ENEMYSTATE enemyState = 0;
	float stateTime = 0.0f;
	public float idleStateMaxTime = 2.0f;
	Animation anim;
	Transform target = null;
	CharacterController characterController = null;

	DeadSpider deadSpider = null;

	public float moveSpeed = 5.0f;
	public float rotationSpeed = 10.0f;
	public float attackRange = 6.5f;
	public float attackStateMaxTime = 2.0f;


	PlayerState playerState = null;

	void Start(){
		target = GameObject.Find ("Player").transform;
		characterController = GetComponent<CharacterController>();

		playerState = target.GetComponent<PlayerState>();
	}
		
	void Awake(){
		anim = GetComponent<Animation> ();
		InitSpider ();
	}

	void OnEnable() {
		InitSpider(); 
	}

	void InitSpider(){

		healthPoint = 5;
		enemyState = ENEMYSTATE.IDLE;
		anim.Play("idle");
	}

	void OnCollisionEnter( Collision collision ) {
		Debug.Log( collision.gameObject.name );
//		int layerIndex = collision.gameObject.layer;

		if( enemyState == ENEMYSTATE.NONE || enemyState == ENEMYSTATE.DEAD ) 
			return;

		//How to detect collision : Layer
//		if( LayerMask.LayerToName( layerIndex ) != "Bomb" )
//			return;

		//How to detect collision : Tag 
//		if( collision.gameObject.tag != "Bomb" ) 
//			return;

		if( collision.gameObject.name.Contains( "Ball" ) == false )
			return;


		enemyState = ENEMYSTATE.DAMAGE;
	}

	IEnumerator DeadProcess() {
		anim[ "death" ].speed = 0.5f; 
		anim.Play( "death" );
		while( anim.isPlaying ) {
			yield return new WaitForEndOfFrame(); 
		}

		yield return new WaitForSeconds(1.0f);

		GameObject explosionObj = Instantiate( explosionParticle ) as GameObject; 
		Vector3 explosionObjPos = transform.position;
		explosionObjPos.y = 0.6f;
		explosionObj.transform.position = explosionObjPos;

		yield return new WaitForSeconds(0.5f);
		GameObject deadObj = Instantiate(deadObject) as GameObject; 
		Vector3 deadObjPos = transform.position;
		deadObjPos.y = 0.6f;
		deadObj.transform.position = deadObjPos;
		float rotationY = Random.Range( -180.0f, 180.0f); 
		deadObj.transform.eulerAngles = new Vector3(0.0f, rotationY, 0.0f);

		//Destroy( gameObject );
		gameObject.SetActive( false );
	}

	void Update(){
		
		switch (enemyState) {
		case ENEMYSTATE.IDLE:
			{
				stateTime += Time.deltaTime;
				if (stateTime > idleStateMaxTime) {
					stateTime = 0.0f;
					enemyState = ENEMYSTATE.MOVE;
				}
			}
			break;
		case ENEMYSTATE.MOVE:
			{
				anim.Play( "walk" );
				float distance = (target.position - transform.position).magnitude;
				if( distance < attackRange ){
					enemyState = ENEMYSTATE.ATTACK;
					stateTime = attackStateMaxTime;
				}else{
					Vector3 dir = target.position - transform.position;
					dir.y = 0.0f;
					dir.Normalize();
					characterController.SimpleMove( dir * moveSpeed );
					transform.rotation = Quaternion.Lerp( transform.rotation, Quaternion.LookRotation( dir ), rotationSpeed * Time.deltaTime );
				}
			}
			break;
		case ENEMYSTATE.ATTACK:
			{
				stateTime += Time.deltaTime;
				float distance = (target.position - transform.position).magnitude;

				if( stateTime > attackStateMaxTime ) {
					stateTime = 0.0f;
					anim.Play( "attack_Melee" );
					anim.PlayQueued( "idle", QueueMode.CompleteOthers );
					playerState.DamageByEnemy();
				}

				if( distance > attackRange )
				{
					enemyState = ENEMYSTATE.IDLE;
				}
			}
			break;
		case ENEMYSTATE.DAMAGE:
			{
				healthPoint -= 1;
				Debug.Log( healthPoint );
		
				anim["damage"].speed = 0.5f;
				anim.Play( "damage" );

				anim.PlayQueued( "idle", QueueMode.CompleteOthers );

				stateTime = 0.0f;
				enemyState = ENEMYSTATE.IDLE;

				AudioManager.instance.PlaySfx( clip );

				if( healthPoint <= 0 )
					enemyState = ENEMYSTATE.DEAD;
			}
			break;
		case ENEMYSTATE.DEAD:
			{
				//Destroy( gameObject );
				StartCoroutine( "DeadProcess" ); 
				enemyState = ENEMYSTATE.NONE;

				ScoreManager.Instance().myScore += score;
			}
			break;
			


		}
	}



}
