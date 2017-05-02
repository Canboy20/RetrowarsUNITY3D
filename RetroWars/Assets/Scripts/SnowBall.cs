using UnityEngine;
using System.Collections;

public class SnowBall : MonoBehaviour {

	//Public Variables
	public float ballSpeed;
	public GameObject snowBallEffect;
	public int hitPoint;
	public float knockBackMagnitude = 700f;

	//Private Variables
	private Rigidbody2D snowBallRB;

	void Start () {
		snowBallRB = GetComponent <Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		snowBallRB.velocity = new Vector2 (ballSpeed * transform.localScale.x, 0);
	}

	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject.GetComponent <PlayerController>() && other.tag != "Collectibles"){


			int knockBackDirection = (snowBallRB.velocity.x > 0) ? 1 : -1;
			knockBackDirection = snowBallRB.velocity.x == 0 ? 0 : knockBackDirection;
			other.GetComponent <Rigidbody2D>().AddForce (new Vector2(knockBackMagnitude * knockBackDirection, 0.0f));
			//other.GetComponent <Rigidbody2D>().AddForce (new Vector2((other.GetComponent <Transform>().position.x * 250.0f),0.0f));

			if(other.tag == "Player1"){

				GameManager.Instance.PTwoHitCount++;
				Debug.Log ("Player 2 Hit Count : " + GameManager.Instance.PTwoHitCount);

				if(GameManager.Instance.POneScore > 0){
					
					GameManager.Instance.POneScore -= hitPoint;

				}

				if(GameManager.Instance.POneDamagedCount < 9){

					GameManager.Instance.POneDamagedCount++;

				}else if(GameManager.Instance.POneDamagedCount >= 9){
					
					GameManager.Instance.POneDamagedCount = 0;

					GameManager.Instance.SubstractHealth (other.gameObject);
				}


			}else if(other.tag == "Player2"){

				GameManager.Instance.POneHitCount++;
				Debug.Log ("Player 1 Hit Count : " + GameManager.Instance.POneHitCount);

				if(GameManager.Instance.PTwoScore > 0){

					GameManager.Instance.PTwoScore -= hitPoint;
					
				}

				if(GameManager.Instance.PTwoDamagedCount < 9){

					GameManager.Instance.PTwoDamagedCount++;

				}else if(GameManager.Instance.PTwoDamagedCount >= 9){

					GameManager.Instance.PTwoDamagedCount = 0;

					GameManager.Instance.SubstractHealth (other.gameObject);
				}


			}

			Instantiate (snowBallEffect,transform.position,transform.rotation);
			Destroy (gameObject);
		
		}else if (other.tag != "Collectibles" /*|| other.tag  != "Player1Sheild" || other.tag != "Player2Sheild"*/){

			Instantiate (snowBallEffect,transform.position,transform.rotation);
			Destroy (gameObject);
			
		}


	}
}
