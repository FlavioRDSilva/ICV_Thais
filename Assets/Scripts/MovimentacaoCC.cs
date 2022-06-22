using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentacaoCC : MonoBehaviour {

	private CharacterController controle;
	[SerializeField] private float velocidade = 10;
	[SerializeField] private float velUp = 100;
	[SerializeField] private Transform verificaChao;
	[SerializeField] private float alturaDoPulo = 1;
	[SerializeField] private float velocidadeDoPulo = 11;
	[SerializeField] private GameObject[] objetos;
	[SerializeField] private float bulletVel = 100;

	private float ultimoYnoChao;
	private ThisState state = ThisState.livre;

	private enum ThisState
	{ 
		livre,
		pulando
	}

	// Use this for initialization
	void Start () {
		controle = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == ThisState.livre)
		{
			Vector3 moveDir = new Vector3(
				Input.GetAxis("Horizontal")
				, Input.GetKey(KeyCode.Space) ? velUp : 0,
				Input.GetAxis("Vertical")
				);

			controle.SimpleMove(moveDir * velocidade);
			
			if (Physics.Raycast(verificaChao.position, Vector3.down, 0.2f))
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					ultimoYnoChao = transform.position.y;
					state = ThisState.pulando;
				}

				Debug.Log("Está no chao");
			}

			RaycastHit hit = new RaycastHit();

			Debug.DrawLine(transform.position, transform.position + Vector3.forward * 5,Color.magenta);

			if (Physics.Raycast(transform.position, Vector3.back, out hit,5))
			{
				Debug.Log("O raio bateu em : " + hit.collider);
			}

			if (Input.GetKeyDown(KeyCode.F))
			{
				Atirar();
			}
		}
		else if (state == ThisState.pulando)
		{
			
			controle.Move(Vector3.up * velocidadeDoPulo*Time.deltaTime);

			if (transform.position.y - ultimoYnoChao > alturaDoPulo)
			{
				state = ThisState.livre;
			}
		}
	}

	public void Atirar()
	{
		GameObject G = Instantiate(objetos[Random.Range(0, 3)], transform.position + Vector3.forward, Quaternion.identity);
		BulletMove b = G.AddComponent<BulletMove>();
		b.vel = bulletVel;
		Destroy(G, 5);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.CompareTag("destructible"))
		{
			Debug.Log("encontei no inimigo");
		}
	}
}
