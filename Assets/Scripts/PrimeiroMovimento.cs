using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeiroMovimento : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		bool correndo = Input.GetKey(KeyCode.LeftShift);

		float velocidade = correndo ? 10 : 4;
		//if((Mathf.Abs(h)>0)^(Mathf.Abs(v)>0))
			transform.position += new Vector3(h, 0, v).normalized * Time.deltaTime*velocidade;
	}
}
