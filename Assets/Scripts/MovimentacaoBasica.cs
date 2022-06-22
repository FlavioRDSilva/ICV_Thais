using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoBasica : MonoBehaviour {

	[SerializeField] private Rigidbody rb;
	public float velocidade = 10;
	[SerializeField]float velTroque = .25f;
	float velMax = 10;
	[SerializeField]private GameObject G;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey(KeyCode.W))
			rb.AddForce(velocidade*transform.forward);

		Vector3 velPlane = Vector3.ProjectOnPlane(rb.velocity, Vector3.up);

        if (velPlane.magnitude > velMax)
        {
			Vector3 v = velPlane.normalized * velMax;
			rb.velocity = new Vector3(v.x, rb.velocity.y, v.z);
			
        }

        if (Input.GetKey(KeyCode.D))
			rb.AddTorque(Vector3.up * velTroque);
	}
}
