using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerDetected : MonoBehaviour {


	//public GameObject spawnableObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		GameManager.Instance.SubstractHealth (other.gameObject);


	}
}
