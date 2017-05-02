using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	// Use this for initialization

	Vector3 startPos;
	Vector3 endPos;

	public float cameraActionDelay;
	public float cameraSpeed;

	void Start () {

		startPos = transform.position;
		endPos = new Vector3 (transform.position.x, 0.75f, transform.position.x);
	
	}


	IEnumerator DelayCameraAction(){

		yield return new WaitForSeconds (cameraActionDelay);

	}

	// Update is called once per frame
	void Update () {



		if(GameManager.Instance.gModes == GameManager.GameModes.InGame){

			if(cameraActionDelay >= 0.0f){

				cameraActionDelay -= Time.deltaTime;
				Debug.Log (cameraActionDelay);
			}

			if(cameraActionDelay <= 0.0f){

				transform.position += endPos * Time.deltaTime *cameraSpeed;
			}


			//StartCoroutine (DelayCameraAction ());
		}
			

		if(transform.position.y >= 72.5f){
			GetComponent <CameraMovement>().enabled = false;
		}
	}
		
}
