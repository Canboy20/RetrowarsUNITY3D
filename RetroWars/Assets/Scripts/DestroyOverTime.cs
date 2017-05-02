using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {

	// Use this for initialization

	public float lifeTime;

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {

		lifeTime -= Time.deltaTime;

		if(lifeTime < 0 ){
			Destroy (gameObject);
		}
	
	}
}
