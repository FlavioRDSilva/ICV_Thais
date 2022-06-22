using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

	public Image hpBar;
	public int pontosDeVida = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void TomarDano(int dano)
	{
		pontosDeVida -= dano;
		pontosDeVida = Mathf.Max(pontosDeVida, 0);

		hpBar.fillAmount = (float)pontosDeVida / 100;

		if (pontosDeVida <= 0)
			Destroy(gameObject);
	}
}
