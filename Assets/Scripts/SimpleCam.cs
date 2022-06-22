using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCam : MonoBehaviour {

	[SerializeField] private Transform alvo;
	[SerializeField] private float alura = 3;
	[SerializeField] private float distancia = 6;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position = alvo.position + Vector3.up * alura + Vector3.back * distancia;
	}
}
