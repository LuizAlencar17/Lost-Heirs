using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {

	public int damage=1;       //dano da bala
	public float Velocidade;        //Velocidade da bala
	public float tempoDuracao; //Tempo de destruição da bala
	public bool DamagePlayers;
	// Use this for initialization
	void Start () {
		//Destroi balla
		Destroy(gameObject, tempoDuracao);
	}
	
	// Update is called once per frame
	void Update () {
		//Direção da bala
		transform.Translate (Vector2.right*Velocidade*Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Enemy" & !(other.gameObject.name == "Boss")) {
			Inimigos abj = other.gameObject.GetComponent<Inimigos> ();
			abj.vida--;
			Destroy (gameObject);
		}
		if(other.gameObject.name == "Boss"){
			Boss abj = other.gameObject.GetComponent<Boss> ();
			abj.DamageBoss();
			Destroy (gameObject);
		}
		if (other.gameObject.tag == "Player" & DamagePlayers) {
			Jogador abj = other.gameObject.GetComponent<Jogador> ();
			abj.DamagePlayer ();
			Destroy (gameObject);
		}
	}
}