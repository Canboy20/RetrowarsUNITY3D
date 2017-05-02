using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScoreLerp : MonoBehaviour {

    //Objects to be moved
    public GameObject playerOneScoreObject;
    public GameObject playerTwoScoreObject;

    //Start
    private Vector3 startPosPlayerOne;
    private Vector3 startPosPlayerTwo;


    //End
    private Vector3 endPosPlayerOne;
    private Vector3 endPosPlayerTwo;


    //Distance
    private float distance = 200f;

    //Time to take from start to end
    private float lerpTime = 0.2f;

    //This will update lerp time
    private float currentLerpTime = 0;

    private bool keyHasBeenPressed = false;

	private bool checkerRef = true;






	// Use this for initialization
	void Start () {

		startPosPlayerOne = playerOneScoreObject.transform.position+ Vector3.up * distance;
		startPosPlayerTwo = playerTwoScoreObject.transform.position+ Vector3.up * distance;


        endPosPlayerOne = playerOneScoreObject.transform.position ;
        endPosPlayerTwo = playerTwoScoreObject.transform.position;



    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.gModes == GameManager.GameModes.BeforeStart)
        {

			if (checkerRef) {
			
				checkerRef = false;
				playerOneScoreObject.transform.position= playerOneScoreObject.transform.position + Vector3.up * distance;
				playerTwoScoreObject.transform.position= playerTwoScoreObject.transform.position + Vector3.up * distance;
			}




            if (Input.anyKey)
            {

                keyHasBeenPressed = true;
                GameManager.Instance.gModes = GameManager.GameModes.Start;
                GameManager.Instance.pressToPlayText.SetActive(false);
                GameManager.Instance.playPipeSound();

            }
        }


            if (keyHasBeenPressed)
            {


                currentLerpTime += Time.deltaTime;
                if(currentLerpTime>=lerpTime)
                {
                    currentLerpTime = lerpTime;

                }


                float Perc = currentLerpTime / lerpTime;
                playerOneScoreObject.transform.position = Vector3.Lerp(startPosPlayerOne, endPosPlayerOne, Perc);
                playerTwoScoreObject.transform.position = Vector3.Lerp(startPosPlayerTwo, endPosPlayerTwo, Perc);


            }

    }



	public void unAnimatedPosOfTexts(){

		playerOneScoreObject.transform.position = startPosPlayerOne;
		playerTwoScoreObject.transform.position = startPosPlayerTwo;
	}
}
