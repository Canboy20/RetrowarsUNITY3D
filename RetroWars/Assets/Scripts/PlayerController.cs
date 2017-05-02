using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


	public enum PowerUpTypes{

		None,
		Nuke,
		Mushroom,
		BlackHole,
		Pacman,
		SwitchPos
	};

	//Public Variables
	[Header("Player Variables")]
	public float speed;
	public float jumpForce;
	public Transform groundCheckPoint;
	public bool isPlayerGrounded;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public GameObject snowBall;
	public GameObject NukePrefab;
	public GameObject MushroomPrefab;
	public Transform throwPoint;
	public Transform mushroomThrowPoint;
	public PowerUpTypes pType;
	public bool hasPowerUP;
	public bool infected;
	public bool hasShield;


	[Header("Keyboard Keys")]
	public string jumpButton;
	public string fireButton;
	public string horizontalAxisString;
	public string powerUpButton;
	public KeyCode powerUpkey;


	//Private Variables
	Rigidbody2D playerRB;
	float hVel;
	Vector2 playerVel;
	Vector2 lastVel;
	bool canJumpTwice;
	private Animator anim;
	public string controllerType;
	[HideInInspector]
	public string currentPlayerTag;
	private SpriteRenderer spRenderer;
	private const float playerCollisionMagnitudeX = 700f;
	private const float playerCollisionMagnitudeY = 70f;
	private GameObject OwnInvetorySlot;



	void Start () {

		playerRB = GetComponent<Rigidbody2D> ();
		anim = GetComponent <Animator> ();
		spRenderer = GetComponent <SpriteRenderer> ();
		canJumpTwice = false;
		hasPowerUP = false;
		pType = PowerUpTypes.None;
		infected = false;
		currentPlayerTag = gameObject.tag;
		OwnInvetorySlot = (gameObject.tag == "Player1") ? GameManager.Instance.pOneInventorySlot : GameManager.Instance.PTwoInventorySlot;


	}
	
	// Update is called once per frame
	void Update () {

        /*if (Input.GetButton ("kStart")) {
			controllerType = "k";
		}			
		else if (Input.GetButton ("jStart")) {
			controllerType = "j";
		}*/
        // burayi editle Keyboard icin "k", gamepad icin "j"


        if (GameManager.Instance.gModes == GameManager.GameModes.BeforeStart)
        {

            /*
            if (Input.anyKey)
            {

                GameManager.Instance.gModes = GameManager.GameModes.Start;
                GameManager.Instance.pressToPlayText.SetActive(false);
                GameManager.Instance.performShowScoreAnimation();
                GameManager.Instance.playPipeSound();
                Debug.Log("Entering Start mode....");




            }*/

        }
        else {


            controllerType = "k";

            playerVel = Vector2.zero;

            isPlayerGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

            if (!infected) {

                if (isPlayerGrounded) {

                    GetPlayerInput(1.0f, 1.0f, fireButton);

                    if (Input.GetButtonDown(controllerType + "_" + jumpButton) && GameManager.Instance.gModes == GameManager.GameModes.InGame) {

                        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                        GameManager.Instance.jumpSound.Play();
                        canJumpTwice = true;
						SetJumpCount ();
                    }

                } else {

                    GetPlayerInput(0.75f, 1.0f, fireButton);

                    if (Input.GetButtonDown(controllerType + "_" + jumpButton) && GameManager.Instance.gModes == GameManager.GameModes.InGame) {

                        if (canJumpTwice) {
                            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce / 1.5f);
                            GameManager.Instance.jumpSound.Play();
                            canJumpTwice = false;
							SetJumpCount ();

                        }
                    }

                }


			} else if (infected) {

		
                if (isPlayerGrounded) {

                    GetPlayerInput(1.0f, -1.0f, jumpButton);

                    if (Input.GetButtonDown(controllerType + "_" + fireButton) && GameManager.Instance.gModes == GameManager.GameModes.InGame) {

                        playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
                        GameManager.Instance.jumpSound.Play();
                        canJumpTwice = true;
						SetJumpCount ();
                    }

                } else {

                    GetPlayerInput(0.75f, -1.0f, jumpButton);

                    if (Input.GetButtonDown(controllerType + "_" + fireButton) && GameManager.Instance.gModes == GameManager.GameModes.InGame) {

                        if (canJumpTwice) {
                            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce / 1.5f);
                            GameManager.Instance.jumpSound.Play();
                            canJumpTwice = false;
							SetJumpCount ();

                        }
                    }

                }


            }


            if (playerRB.velocity.x < 0) {


                transform.localScale = new Vector3(-1, 1, 1);


            } else if (playerRB.velocity.x > 0) {


                transform.localScale = Vector3.one;


            }

            if (hasShield) {


                Color halfColor = spRenderer.color;
                halfColor.a = 0.5f;
                spRenderer.color = Color.Lerp(spRenderer.color, halfColor, 0.005f * Time.time);

            } else {

                Color fullColor = spRenderer.color;
                fullColor.a = 1f;
                spRenderer.color = Color.Lerp(spRenderer.color, fullColor, 0.005f * Time.time);
            }

            anim.SetFloat("speed", Mathf.Abs(playerRB.velocity.x));
            anim.SetBool("grounded", isPlayerGrounded);


            switch (pType) {

                case PowerUpTypes.None:

                    OwnInvetorySlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("IconEmpty");

                    break;
                case PowerUpTypes.Nuke:

                    OwnInvetorySlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("IconNuke");

                    break;
                case PowerUpTypes.Mushroom:

                    OwnInvetorySlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("IconMushroom");

                    break;
                case PowerUpTypes.Pacman:

                    OwnInvetorySlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("IconPacman");


                    break;
                case PowerUpTypes.SwitchPos:

                    OwnInvetorySlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("IconSwitch");

                    break;
                case PowerUpTypes.BlackHole:
                    break;

                default:
                    break;


            }
        }
			
	}

	void LateUpdate(){

		if(pType == PowerUpTypes.None){
			hasPowerUP = false;
		}
			
		if(infected){

			StartCoroutine (CureForInfection ());
		}


		if(hasShield){

			StartCoroutine (DeActivateSheild ());
		}
	}
		

	void GetPlayerInput(float speedFactor, float direction, string bttnstr){

		if(GameManager.Instance.gModes == GameManager.GameModes.InGame){

			hVel = Input.GetAxisRaw (controllerType + "_" + horizontalAxisString) * ((speed * speedFactor)  * direction);
			playerVel = new Vector2 (hVel, playerRB.velocity.y);
			playerRB.velocity = playerVel;
			lastVel = playerRB.velocity;

			if(Input.GetButtonDown (controllerType + "_" + bttnstr)){

				Fire (snowBall);

			}

			if (hasPowerUP && pType != PowerUpTypes.None) {

				//Power Up Varsa
				if(Input.GetButtonDown (controllerType + "_" +powerUpButton)){

					switch(pType){

					case PowerUpTypes.None:
						
						break;
					case PowerUpTypes.Nuke:
						

						StartCoroutine (Drope (NukePrefab));

						break;
					case PowerUpTypes.Mushroom:

						StartCoroutine (ThrowSomething (MushroomPrefab));
							
						break;
					case PowerUpTypes.Pacman:

						if(!hasShield){

							StartCoroutine (ActivateSheild ());
						}

						break;
					case PowerUpTypes.SwitchPos:
						
						StartCoroutine (SwitchPos ());

						break;
					case PowerUpTypes.BlackHole:
						break;
					
					default:
						break;


					}
				}
				
			} else {

				//Power Up yoksa
			}
				
		}else if (GameManager.Instance.gModes == GameManager.GameModes.Pause){

			Debug.Log ("Yarrak");
		}

	}

	void SetJumpCount(){

		if(currentPlayerTag == "Player1"){


			GameManager.Instance.POneJumpCount++;
			Debug.Log ("Player 1 Jump Count : " + GameManager.Instance.POneJumpCount);


		}else if(currentPlayerTag == "Player2"){

			GameManager.Instance.PTwoJumpCount++;
			Debug.Log ("Player 2 Jump Count : " + GameManager.Instance.PTwoJumpCount);

		}
	}

	IEnumerator SwitchPos(){

		GameObject otherPlayer = (this.gameObject.tag != "Player1") ? GameObject.FindGameObjectWithTag ("Player1") : GameObject.FindGameObjectWithTag ("Player2") as GameObject;
		GameObject ownPlayer = (this.gameObject.tag == "Player1") ? GameObject.FindGameObjectWithTag ("Player1") : GameObject.FindGameObjectWithTag ("Player2") as GameObject;

		Vector2 ownTransform = ownPlayer.transform.position;
		Vector2 otherTransform = otherPlayer.transform.position;

		Debug.Log ("Own : " + ownTransform  + " Other : " + otherTransform);
		yield return new WaitForSeconds (0.2f);
		pType = PowerUpTypes.None;
		otherPlayer.transform.position = new Vector2 (ownTransform.x,ownTransform.y);
		ownPlayer.transform.position = new Vector2 (otherTransform.x,otherTransform.y);
		
	}

	IEnumerator CureForInfection(){

		yield return new WaitForSeconds (10f);
		infected = false;
	}

	IEnumerator ActivateSheild(){
		GameManager.Instance.pacmanSound.Play ();
		hasShield = true;

		if(currentPlayerTag == "Player1"){

			gameObject.tag = "Player1Sheild";
		}else if(currentPlayerTag == "Player2"){
			gameObject.tag = "Player2Sheild";
		}
		//gameObject.tag = "Collectibles";
		yield return new WaitForSeconds (0.12f);
		pType = PowerUpTypes.None;
	}

	IEnumerator DeActivateSheild(){
		
		yield return new WaitForSeconds (5f);
		gameObject.tag = currentPlayerTag;
		hasShield = false;
	}

	void Fire(GameObject objectToFire){

		GameManager.Instance.shootSound.Play ();
		GameObject tempOBJ = (GameObject)Instantiate (objectToFire, throwPoint.position, throwPoint.rotation);
		tempOBJ.transform.localScale = transform.localScale /2;
		anim.SetTrigger ("throw");

		if(currentPlayerTag == "Player1"){

			GameManager.Instance.POneThrowCount++;
			Debug.Log ("Player 1 Throw Count : " + GameManager.Instance.POneThrowCount);
			
		}else if(currentPlayerTag == "Player2"){

			GameManager.Instance.PTwoThrowCount++;
			Debug.Log ("Player 2 Throw  Count : " + GameManager.Instance.PTwoThrowCount);
			
		}
	}

	IEnumerator Drope(GameObject nukeObject){

		Transform nukeDropPos = (this.tag == "Player1") ? GameObject.FindGameObjectWithTag ("Player2").transform : GameObject.FindGameObjectWithTag ("Player1").transform;
		GameObject tempNuke = Instantiate (nukeObject, new Vector3 (nukeDropPos.position.x , nukeDropPos.position.y + 10f , nukeDropPos.position.z), Quaternion.Euler (new Vector3 (0,0,180f))) as GameObject;
		yield return new WaitForSeconds (0.01f);
		pType = PowerUpTypes.None;

	}

	IEnumerator ThrowSomething(GameObject mushroomObject){

		GameObject tempMushroom = Instantiate (mushroomObject, mushroomThrowPoint.position, mushroomThrowPoint.rotation);
		tempMushroom.transform.localScale = transform.localScale;
		float xDirection = (transform.localScale.x > 0f) ? 1f : -1f;
		tempMushroom.GetComponent <Mushroom>().playerCtrl = this;
		tempMushroom.GetComponent <Rigidbody2D>().AddForce (new Vector2(xDirection * 500f,250f));
		anim.SetTrigger ("throw");

		yield return new WaitForSeconds (0.3f);
		pType = PowerUpTypes.None;
	
	}

	void OnCollisionStay2D(Collision2D other){

		if(other.gameObject.GetComponent <PlayerController>() && other.gameObject.tag !=this.gameObject.tag && other.gameObject.tag != "Collectibles"){
			GameManager.Instance.slamSound.Play ();
			if(other.rigidbody.position.x < this.gameObject.GetComponent <Rigidbody2D>().position.x){
				other.gameObject.GetComponent <Rigidbody2D>().AddForce (new Vector2(-1*playerCollisionMagnitudeX,playerCollisionMagnitudeY));
				this.gameObject.GetComponent <Rigidbody2D>().AddForce (new Vector2(playerCollisionMagnitudeX,playerCollisionMagnitudeY));
			}else{
				other.gameObject.GetComponent <Rigidbody2D>().AddForce (new Vector2(playerCollisionMagnitudeX,playerCollisionMagnitudeY));
				this.gameObject.GetComponent <Rigidbody2D>().AddForce (new Vector2(-1*playerCollisionMagnitudeX,playerCollisionMagnitudeY));
			}
		}
	}

		
}
