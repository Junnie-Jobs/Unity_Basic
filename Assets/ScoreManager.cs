using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	static ScoreManager _instance = null;
	int _bestScore = 0; 
	int _myScore = 0;

	public static ScoreManager Instance() {
		return _instance; 
	}
	

	// Use this for initialization
	void Start () {
		if( _instance == null ) 
			_instance = this;
		else
			Destroy( gameObject );
	}

	public int bestScore {
		get {
			return _bestScore; 
		}
	}

	public int myScore {
		get {
			return _myScore;
		}
		set {
			_myScore = value;
			if (_myScore > _bestScore) {
				_bestScore = _myScore;
				SaveBestScore ();
			}
		}
	}

	void SaveBestScore()
	{
		PlayerPrefs.SetInt( "Best Score", _bestScore ); 
	}

	void LoadBestScore() {
		_bestScore = PlayerPrefs.GetInt( "Best Score", 0 ); 
	}
	
	// Update is called once per frame
//	void Update () {
//	
//	}
}
