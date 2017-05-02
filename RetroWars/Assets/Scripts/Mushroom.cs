using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour {

	//Public Variables
	public int hitPoint;
	public CircleCollider2D effectArea;
	[HideInInspector]
	public PlayerController playerCtrl;
	public GameObject poisonEffectPrefab;

	//Private Variables
	private PlayerController enemyCtrl;
	private Rigidbody2D theRB;


	// Use this for initialization
	void Start () {

		theRB = GetComponent <Rigidbody2D> ();
		
	}
	
	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.GetComponent <PlayerController>() && other.gameObject.GetComponent <PlayerController>() != playerCtrl && other.gameObject.tag != "Collectibles"){

			Debug.Log ("Zehirlen Amk");
			enemyCtrl = other.gameObject.GetComponent <PlayerController> ();
			enemyCtrl.infected = true;
			GameObject tempPart = Instantiate (poisonEffectPrefab,new Vector3 (enemyCtrl.transform.position.x , enemyCtrl.transform.position.y + 1f , enemyCtrl.transform.position.z - 0.1f), Quaternion.identity) as GameObject;
			tempPart.GetComponent <Renderer>().sortingLayerName = "VeryCloseObjects";
			gameObject.GetComponent <SpriteRenderer>().enabled = false ;
			if(other.tag == "Player1"){

				if(GameManager.Instance.POneScore > 0){

					GameManager.Instance.POneScore = (GameManager.Instance.POneScore - hitPoint > 0) ? GameManager.Instance.POneScore - hitPoint : GameManager.Instance.POneScore = 0;

				}

			}else if(other.tag == "Player2"){

				if(GameManager.Instance.PTwoScore > 0){

					GameManager.Instance.PTwoScore = (GameManager.Instance.PTwoScore - hitPoint > 0) ? GameManager.Instance.PTwoScore - hitPoint : GameManager.Instance.PTwoScore = 0;

				}

			}

			StartCoroutine (KillSelf ());
		}
	}


	IEnumerator KillSelf(){

		yield return new WaitForSeconds (3f);
		Destroy (gameObject);
	}
}
