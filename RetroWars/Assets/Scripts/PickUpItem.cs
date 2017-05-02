using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour {


	//Public Variables
	public bool hasCollected;
	public PlayerController.PowerUpTypes powerUpType;

	//Private Variables
	private PlayerController playerCtrl;


	void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.GetComponent <PlayerController>()){
			playerCtrl = other.GetComponent <PlayerController> ();

			Debug.Log ("Press " + playerCtrl.powerUpButton + " to grab");

			GameObject grabIconObject = GameManager.Instance.grabIcon as GameObject;
			grabIconObject.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 0.75f, 0f);

			if(playerCtrl.hasPowerUP == true){

				grabIconObject.GetComponent <SpriteRenderer>().sprite = Resources.Load<Sprite>("unGrabIcon");

			}else{

				grabIconObject.GetComponent <SpriteRenderer>().sprite = Resources.Load<Sprite>("grabicon");
			}

			grabIconObject.SetActive (true);

		}
	}

	void OnTriggerExit2D(Collider2D other){

		if(other.gameObject.GetComponent <PlayerController>()){

			GameManager.Instance.grabIcon.SetActive (false);

		}
	}

	void OnTriggerStay2D(Collider2D other){


		if(other.gameObject.GetComponent <PlayerController>()){


			if(playerCtrl.hasPowerUP == false){



				if(Input.GetButtonUp (playerCtrl.controllerType + "_" + playerCtrl.powerUpButton)){
					
					hasCollected = true;
					GameManager.Instance.powerUpSound.Play ();
					GameManager.Instance.grabIcon.SetActive (false);
					playerCtrl.hasPowerUP = true;
					playerCtrl.pType = powerUpType;

					if(playerCtrl.tag == "Player1"){

						GameManager.Instance.POnePowerUpCount++;
						Debug.Log ("Player 1 Powerup Count : " + GameManager.Instance.POnePowerUpCount);
						
					}else if(playerCtrl.tag == "Player2"){

						GameManager.Instance.PTwoPowerUpCount++;
						Debug.Log ("Player 2 Powerup Count : " + GameManager.Instance.PTwoPowerUpCount);
					}

					Destroy (gameObject);
					
				}

			}
		}

	}
		
}
