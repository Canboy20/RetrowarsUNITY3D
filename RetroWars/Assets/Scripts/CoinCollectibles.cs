using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectibles : MonoBehaviour {

	// Use this for initialization
	public int points;

	void OnTriggerEnter2D(Collider2D other){




		if(other.gameObject.tag == "Player1"){
			GameManager.Instance.coinSound.Play ();
			GameManager.Instance.POneScore += points;
            Debug.Log("Cur Health is "+ GameManager.Instance.pOneHealthCount);

            if (GameManager.Instance.pOneHealthCount <5)
            {

                if (GameManager.Instance.POneScore >= (GameManager.Instance.getMultiplier()*100))
                {
                    Debug.Log("Bulbasour");

                    GameManager.Instance.increaseMultiplier();
                    GameManager.Instance.addHealthP1();

                }

            }
            else
            {
                if (GameManager.Instance.POneScore >= (GameManager.Instance.getMultiplier() * 100))
                {
                    GameManager.Instance.increaseMultiplier();

                }

            }

            Debug.Log (GameManager.Instance.POneScore);
			Destroy (this.gameObject);

		}else if(other.gameObject.tag == "Player2"){
			GameManager.Instance.coinSound.Play ();
			GameManager.Instance.PTwoScore += points;

            if (GameManager.Instance.pTwoHealthCount < 5)
            {
                
                if (GameManager.Instance.PTwoScore >=  (GameManager.Instance.getMultiplier2()*100))
                {
                    GameManager.Instance.increaseMultiplier2();
                    GameManager.Instance.addHealthP2();
                }

            }
            else
            {
                if (GameManager.Instance.PTwoScore >= (GameManager.Instance.getMultiplier2() * 100))
                {
                    GameManager.Instance.increaseMultiplier2();

                }

            }


            Debug.Log (GameManager.Instance.PTwoScore);
			Destroy (this.gameObject);
		}
	}


}
