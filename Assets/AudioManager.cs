using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public AudioClip music = null;
	public AudioSource audioSource;
	public static AudioManager instance { get; private set; }

	// Use this for initialization
	void Start () {
		
		audioSource =GetComponent<AudioSource>(); 

		if( instance == null ) 
			instance = this;


		if( music != null )
		{
			audioSource.clip = music; 
			audioSource.loop = true; 
			audioSource.Play();
		}

	
	}

	public void PlaySfx( AudioClip clip ) {
		audioSource.PlayOneShot( clip ); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
