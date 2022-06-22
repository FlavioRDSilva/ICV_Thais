using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeuPrimeiroScript : MonoBehaviour {
	
    
	// Use this for initialization
    void Start () {

		
		GameObject G = GameObject.Find("minhaLuz");
		print(G);
		Light L = G.GetComponent<Light>();
		Debug.Log(L + " : " + L.color);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
