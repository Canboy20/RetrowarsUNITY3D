using UnityEngine;
using System.Collections;

public class MushroomPowerUp : BasePowerUp {

	// Use this for initialization
	public override void Start () {

		base.Start ();
	
	}
	
	// Update is called once per frame
	public override void Update () {

		base.Update ();
	
	}

	public void OnTriggerEnter2D(Collider2D other){

		if(GetComponent <Rigidbody2D>()){

			Destroy (gameObject);


		}else{

			if(other.GetComponent <PlayerController>()){

				Debug.Log (other.name + " wants to eat " + this.name);
				//other.GetComponent <PlayerController>().hasMushroom = true;
				Destroy (gameObject);

			}


		}


	}


}
