using UnityEngine;
using System.Collections;


public class BasePowerUp : MonoBehaviour {


	public string name;
	public float speed;
	private Rigidbody2D theRB;


	public virtual void Start () {

		Debug.Log ("My Name is " + name);

		theRB = GetComponent <Rigidbody2D> ();
	
	}
	
	// Update is called once per frame
	public virtual void Update () {

		theRB.velocity = new Vector2 (speed * transform.localScale.x, 0);
	
	}
		
}
