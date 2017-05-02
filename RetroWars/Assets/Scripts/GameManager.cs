using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public  class GameManager : MonoBehaviour {

	// Use this for initialization

	public enum GameModes{

        BeforeStart,
        Start,
		InGame,
		Pause,
		InAnimation


	};

	[Header("Game Modes")]
	public GameModes gModes;
	public GameObject pressToPlayText;

	[Header("Player Health")]
	public int pOneHealthCount ;
	public int pTwoHealthCount ;

	[Header("Player Prefabs")]
	public GameObject pOnePrefab;
	public GameObject pTwoPrefab;

	[Header("Player Coffes")]
	public  GameObject[] coffes;
	public GameObject[] pTwoCoffes;

	[Header("Player Start Animation Positions")]
	public Transform pOneStartPos;
	public Transform pOneEndPos;
	public Transform pTwoStartPos;
	public Transform pTwoEndPos;

	[Header("HUD")]
	public Text pOneDisplayText;
	public Text pTwoDisplayText;
	public GameObject hudObject;

	[Header("Grab Button Prefab")]
	public GameObject grabIcon;

	[Header("Player Inventory Slots")]
	public GameObject pOneInventorySlot;
	public GameObject PTwoInventorySlot;

	[Header("Sounds")]
	public AudioSource jumpSound;
	public AudioSource coinSound;
	public AudioSource powerUpSound;
	public AudioSource pacmanSound;
	public AudioSource NukeSound;
	public AudioSource shootSound;
	public AudioSource slamSound;
	public AudioSource pipeSound;

	[Header("Player Stats")]
	public GameObject statsScene;
	public Text pOneThrowText;
	public Text pTwoThrowText;
	public Text pOneJumpText;
	public Text pTwoJumpText;
	public Text pOnePowerUpText;
	public Text pTwoPowerUpText;
	public Text pOneHitRatioText;
	public Text pTwoHitRatioText;
	public Text pOneCoinText;
	public Text pTwoCoinText;
	public Text developerHolder;
	public Text designerHolder;

	//Private Variables
	private GameObject pOneStartObject;
	private GameObject pTwoStartObject;

    
	[Header("Score Objects")]
    public GameObject pOneScoreObjectt;
    public GameObject pTwoScoreObjectt;


    private int pOneScore;
	private int pTwoScore;
	private int pOneJumpCount;
	private int pTwoJumpCount;
	private int pOnePowerUpCount;
	private int pTwoPowerUpCount;
	private float pOneThrowCount = 0.0f;
	private float pTwoThrowCount = 0.0f;
	private float pOneHitCount;
	private float pTwoHitCount;
	private int pOneDamagedCount;
	private int pTwoDamagedCount;

    //public int pOneCoinColCountForLives=0;
    //public int pTwoCoinColCountForLives=0;
    public int multiplier = 1;
	public int multiplier2 = 1;

	public int POneDamagedCount{
		
		get{
			return pOneDamagedCount;
		}
		set{

			pOneDamagedCount = value;
		}
	}

	public int PTwoDamagedCount{

		get{

			return pTwoDamagedCount;
		}
		set{

			pTwoDamagedCount = value;
		}
	}


    public float POneHitCount{
		get{

			return pOneHitCount;
		}
		set{

			pOneHitCount = value;
		}
	}

	public float PTwoHitCount{
		get{

			return pTwoHitCount;
		}
		set{

			pTwoHitCount = value;
		}
	}

	public float POneThrowCount{
		get{
			return pOneThrowCount;
		}
		set{
			pOneThrowCount = value;
		}
	}

	public float PTwoThrowCount{
		get{
			return pTwoThrowCount;
		}
		set{
			pTwoThrowCount = value;
		}
	}

	public int POnePowerUpCount{

		get{
			return pOnePowerUpCount;
		}
		set{

			pOnePowerUpCount = value;
		}
	}

	public int PTwoPowerUpCount{

		get{
			return pTwoPowerUpCount;
		}
		set{

			pTwoPowerUpCount = value;
		}
	}

	public int POneJumpCount{

		get{
			return pOneJumpCount;
		}
		set{

			pOneJumpCount = value;
		}
	}

	public int PTwoJumpCount{

		get{
			return pTwoJumpCount;
		}
		set{

			pTwoJumpCount = value;
		}
	}

	public  int POneScore { 
		
		get{
			return pOneScore;	
		} 
		set{
			pOneScore = value;
		} 
	}

	public  int PTwoScore { 
		
		get{
			return pTwoScore;
		}
		set{
			pTwoScore = value;
		}
	}



    public int getMultiplier()
    {
        return multiplier;
    }


	public int getMultiplier2(){
		return multiplier2;
	}

    public void increaseMultiplier()
    {
        multiplier++;
    }

	public void increaseMultiplier2(){
		multiplier2++;
	}
		
	public static GameManager Instance { get; set;}

	void Awake () {

		if(Instance != null &&  Instance!= this){

			GameManager.Instance.coffes = this.coffes;
			GameManager.Instance.pTwoCoffes = this.pTwoCoffes;
			GameManager.Instance.pOneScore = 0;
			GameManager.Instance.pTwoScore = 0;
            //pOneCoinColCountForLives = 0;
            //pTwoCoinColCountForLives = 0;
            GameManager.Instance.gModes = GameModes.InGame;
			GameManager.Instance.pOneStartPos = this.pOneStartPos;
			GameManager.Instance.pOneEndPos = this.pOneEndPos;
			GameManager.Instance.pTwoStartPos = this.pTwoStartPos;
			GameManager.Instance.pTwoEndPos = this.pTwoEndPos;
			GameManager.Instance.pOneDisplayText = this.pOneDisplayText;
			GameManager.Instance.pTwoDisplayText = this.pTwoDisplayText;
			GameManager.Instance.pOneInventorySlot = this.pOneInventorySlot;
			GameManager.Instance.PTwoInventorySlot = this.PTwoInventorySlot;
			GameManager.Instance.pOneJumpText = this.pOneJumpText;
			GameManager.Instance.pTwoJumpText = this.pTwoJumpText;
			GameManager.Instance.pOneThrowText = this.pOneThrowText;
			GameManager.Instance.pTwoThrowText = this.pTwoThrowText;
			GameManager.Instance.pOnePowerUpText = this.pOnePowerUpText;
			GameManager.Instance.pTwoPowerUpText = this.pTwoPowerUpText;
			GameManager.Instance.pOneHitRatioText = this.pOneHitRatioText;
			GameManager.Instance.pTwoHitRatioText = this.pTwoHitRatioText;



			pOneStartObject = Instantiate (pOnePrefab, new Vector3 (-6f, 1f, 0f), Quaternion.identity) as GameObject;
			pTwoStartObject = Instantiate (pTwoPrefab, new Vector3 (6f, 1f, 0f), Quaternion.identity) as GameObject;

			Destroy (gameObject);

		}else{


            //pOneCoinColCountForLives = 0;
            //pTwoCoinColCountForLives = 0;
            pressToPlayText.SetActive (true);
			Instance = this;
			GameManager.Instance.gModes = GameModes.BeforeStart;
			Debug.Log ("Awake " + Instance.gModes);
		}

		//DontDestroyOnLoad (gameObject);

	}

	void Start(){

		switch(gModes){


         
            case GameModes.BeforeStart:
                pOneStartObject = Instantiate(pOnePrefab, pOneStartPos.position, Quaternion.identity) as GameObject;
                pTwoStartObject = Instantiate(pTwoPrefab, pTwoStartPos.position, Quaternion.identity) as GameObject;
			  
                break;

            case GameModes.Start:




			//pOneStartObject = Instantiate (pOnePrefab, pOneStartPos.position, Quaternion.identity) as GameObject;
			//pTwoStartObject = Instantiate (pTwoPrefab, pTwoStartPos.position, Quaternion.identity) as GameObject;

		
			break;

		case GameModes.InGame:
			
			pOneStartObject = Instantiate (pOnePrefab, new Vector3 (-5f, 1f, 0f), Quaternion.identity) as GameObject;
			pTwoStartObject = Instantiate (pTwoPrefab, new Vector3 (5f, 1f, 0f), Quaternion.identity) as GameObject;


			break;
		case GameModes.Pause:
			
			break;
		case GameModes.InAnimation:
			
			break;
		default:
			break;


		}
	}

	public void ResetPlayerOne(){

		pOneHealthCount = 5;

		//SceneManager.LoadScene (0);
		//Application.LoadLevel (0);
		ShowStatsPanel ("Developer", "<color=orange>Winner</color> Designer");



		
	}

	public void ResetPlayerTwo(){

		pTwoHealthCount = 5;

		//SceneManager.LoadScene (0);
		//Application.LoadLevel (0);
		ShowStatsPanel ("<color=orange>Winner</color> Developer", "Designer");
	

	}

	IEnumerator LatePause(){

		yield return new WaitForSeconds (3f);
		Camera.main.GetComponent <CameraMovement>().cameraSpeed = 0.0f;

	}



    public void addHealthP1()
    {
        pOneHealthCount++ ;
    }

    public void addHealthP2()
    {
        pTwoHealthCount++;
    }

    public void SubstractHealth(GameObject damagedPlayer){

		if(damagedPlayer.tag == "Player1" || damagedPlayer.tag == "Player1Sheild"){

			if(pOneHealthCount > 1 ){
				pOneHealthCount--;
				Destroy (damagedPlayer.gameObject);
				GameObject spawnableGO = Instantiate (pOnePrefab, new Vector3 (Camera.main.transform.position.x - 5f, Camera.main.transform.position.y + 1f, 0f), Quaternion.identity) as GameObject;

			}else if(pOneHealthCount == 1 ){

				ResetPlayerOne ();

			}
				
		}else if(damagedPlayer.tag == "Player2" || damagedPlayer.tag == "Player2Sheild"){

			if(pTwoHealthCount > 1 ){
				
				pTwoHealthCount--;
				Destroy (damagedPlayer.gameObject);
				GameObject spawnableGo = Instantiate (pTwoPrefab, new Vector3 (Camera.main.transform.position.x + 5f, Camera.main.transform.position.y + 1f, 0f), Quaternion.identity) as GameObject;
				
			}else if(pTwoHealthCount == 1){

				ResetPlayerTwo ();
			}
				
		}
	}

	public void AdjustPoneCoffe(){

		for(int i = 0 ; i < coffes.Length ; i++){

			if(pOneHealthCount > i){

				coffes [i].gameObject.GetComponent <Image> ().enabled = true;

			}else{
				coffes [i].gameObject.GetComponent <Image> ().enabled = false;
			}
		}
	}

	public void AdjustPtwoCoffe(){

		for(int i = 0 ; i < pTwoCoffes.Length ; i ++ ){

			if(pTwoHealthCount > i){

				pTwoCoffes [i].gameObject.GetComponent <Image> ().enabled = true;

			}else{

				pTwoCoffes [i].gameObject.GetComponent <Image> ().enabled = false;
			}
		}
	}

	public void DisplayText(int pOne , int pTwo){

		//Player 1 display setup
		if(pOne >= 0 && pOne < 10 ){

			pOneDisplayText.text = "0000" + pOne.ToString ();

		}else if (pOne >= 10 && pOne < 100){
			
			pOneDisplayText.text = "000" + pOne.ToString ();

		}else if (pOne >= 100 && pOne < 1000){
			
			pOneDisplayText.text = "00" + pOne.ToString ();

		}else if (pOne >= 1000 && pOne < 10000){

			pOneDisplayText.text = "0" + pOne.ToString ();

		}else if (pOne >= 10000){
			
			pOneDisplayText.text = pOne.ToString ();

		}

		//Player 2 display setup
		if(pTwo >= 0 && pTwo < 10){

			pTwoDisplayText.text = "0000" + pTwo.ToString ();

		}else if (pTwo >= 10 && pTwo < 100){

			pTwoDisplayText.text = "000" + pTwo.ToString ();

		}else if (pTwo >= 100 && pTwo < 1000){

			pTwoDisplayText.text = "00" + pTwo.ToString ();

		}else if (pTwo >= 1000 && pTwo < 10000){

			pTwoDisplayText.text = "0" + pTwo.ToString ();

		}else if (pTwo >= 10000){

			pTwoDisplayText.text = pTwo.ToString ();

		}
			


	}

	void Update(){


		if(Instance.gModes == GameModes.Start){
			GameManager.Instance.pressToPlayText.SetActive(false);
			GameManager.Instance.pressToPlayText.SetActive(false);


			if(pOneStartObject != null){

				pOneStartPos.position = Vector3.Lerp (pOneStartPos.position, pOneEndPos.position, 0.1f * Time.timeSinceLevelLoad);
			}

			if(pTwoStartObject != null){

				pTwoStartPos.position = Vector3.Lerp (pTwoStartPos.position, pTwoEndPos.position, 0.1f * Time.timeSinceLevelLoad);
			}

		}

		if(pOneStartPos.position == pOneEndPos.position && pTwoStartPos.position == pTwoEndPos.position && Instance.gModes != GameModes.Pause){

			Instance.gModes = GameModes.InGame;
		}
			

		AdjustPoneCoffe ();
		AdjustPtwoCoffe ();
		DisplayText (pOneScore,pTwoScore);


		if(Instance.gModes == GameModes.Pause){

			if(Input.GetButtonDown ("JoystickStart")){

				Debug.Log ("Girdim AMK");
			
				SceneManager.LoadScene (0);
			}
		}



	}
		

    public void performShowScoreAnimation()
    {

       // iTween.MoveTo(pOneScoreObjectt, new Vector3(100, 580, 0), 1);
        //iTween.MoveTo(pTwoScoreObjectt, new Vector3(900, 580, 0), 1);


    }



    public void playPipeSound()
    {
        pipeSound.Play();
    }


	public void ShowStatsPanel(string developer, string designer){

		Instance.gModes = GameModes.Pause;
	
		pOneThrowText.text = pOneThrowCount.ToString ();
		pTwoThrowText.text = pTwoThrowCount.ToString ();

		pOneJumpText.text = pOneJumpCount.ToString ();
		pTwoJumpText.text = pTwoJumpCount.ToString ();

		pOnePowerUpText.text = pOnePowerUpCount.ToString ();
		pTwoPowerUpText.text = pTwoPowerUpCount.ToString ();

		pOneHitRatioText.text = (pOneThrowCount > 0.0f ) ?  GetHitRatio (pOneThrowCount,pOneHitCount).ToString ("P") : "0.0%";
		pTwoHitRatioText.text = (pOneThrowCount > 0.0f ) ?  GetHitRatio (pTwoThrowCount,pTwoHitCount).ToString ("P") : "0.0%";

		pOneCoinText.text = pOneScore.ToString ("0000");
		pTwoCoinText.text = pTwoScore.ToString ("0000");

		developerHolder.text = developer;
		designerHolder.text = designer;
	
		statsScene.SetActive (true);
		hudObject.SetActive (false);

		StartCoroutine (LatePause ());

	}

	public float GetHitRatio(float throwCount, float hitCount){



		return (hitCount/throwCount);
	}


}
