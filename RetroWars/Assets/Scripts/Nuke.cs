using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour {

	//Public Variables
	public GameObject explosion;
	public int hitPoint;

	// Private Variables
	private PlayerController playerCtrl;
	private Animator anim;


	void Start () {

		anim = GetComponent <Animator> ();

	}

	IEnumerator Kill(GameObject damagedPlayer){

		GameObject explosionObj = Instantiate (explosion,new Vector3(transform.position.x , transform.position.y  , transform.position.z),Quaternion.identity) as GameObject;

		explosionObj.GetComponent <CircleCollider2D> ().offset = new Vector2 (damagedPlayer.transform.localScale.x * -1, 0f);
		explosionObj.GetComponent <AreaEffector2D>().forceAngle = (damagedPlayer.transform.localScale.x > 0) ? 170 : 10;

		yield return new WaitForSeconds (0.3f);
		Destroy (gameObject);

	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.GetComponent <PlayerController>() && other.gameObject.tag != "Collectibles"){
			
			//GameManager.Instance.SubstractHealth (other.gameObject);


			anim.SetTrigger ("Explode");
			GameManager.Instance.NukeSound.Play ();
			Destroy (gameObject.GetComponent <Rigidbody2D>());
			gameObject.GetComponent <BoxCollider2D>().enabled =false;

			if(other.tag == "Player1"){

				if(GameManager.Instance.POneScore > 0){

					GameManager.Instance.POneScore = (GameManager.Instance.POneScore - hitPoint > 0) ? GameManager.Instance.POneScore - hitPoint : GameManager.Instance.POneScore = 0;
				
				}

				if(GameManager.Instance.pOneHealthCount > 1 ){
					
					GameManager.Instance.pOneHealthCount--;

				}else if(GameManager.Instance.pOneHealthCount == 1 ){
					
					GameManager.Instance.ResetPlayerOne ();
				}


			}else if(other.tag == "Player2"){

				if(GameManager.Instance.PTwoScore > 0){

					GameManager.Instance.PTwoScore = (GameManager.Instance.PTwoScore - hitPoint > 0) ? GameManager.Instance.PTwoScore - hitPoint : GameManager.Instance.PTwoScore = 0;

				}

				if(GameManager.Instance.pTwoHealthCount > 1 ){

					GameManager.Instance.pTwoHealthCount--;

				}else if(GameManager.Instance.pTwoHealthCount == 1 ){

					GameManager.Instance.ResetPlayerTwo ();
				}

			}
			StartCoroutine (Kill (other.gameObject));

		}
	}
}
