using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
	[SerializeField] public float vel=10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.forward * vel*Time.deltaTime;

		//RaycastHit r;
		//if (Physics.Raycast(transform.position, transform.forward, out r))
		//{
		//	if (r.collider.gameObject != gameObject && r.collider.gameObject.CompareTag("destructible"))
		//		Destroy(r.collider.gameObject);
		//}
	}

	void OnCollisionEnter(Collision C)
	{
		Debug.Log("acertou collision");

		if (C.gameObject.CompareTag("destructible"))
		{
			C.gameObject.GetComponent<Enemy>().TomarDano(15);
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider C)
	{
		Debug.Log("acertou trigger");

		if (C.gameObject.CompareTag("destructible"))
		{
			C.gameObject.GetComponent<Enemy>().TomarDano(15);
			Destroy(gameObject);
		}
	}
}
