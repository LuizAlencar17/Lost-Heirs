using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inimigos : MonoBehaviour {
	Rigidbody2D rb2d;
	SpriteRenderer sprite;

	public int vida;
	public float velocidade;  // velocidade do inimigo
	public float duracaoDirecao;  // duração para adar em uma direção
	private float tempoNaDirecao;  // quanto tempo ele está anadando nesta direção

	public int DirecaoX;

	public GameObject Tiro;
	public Transform origemBala;

	public bool dispara;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		rb2d = GetComponent<Rigidbody2D>();

		Invoke ("Atack1", 1f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		MovePlataforma ();

		if(vida<=0){
			Destroy (gameObject);
		}

	}


	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") {
			Jogador abj = other.gameObject.GetComponent<Jogador> ();
			abj.DamagePlayer ();
		}


	}

	void MovePlataforma(){
		transform.Translate(new Vector3(DirecaoX * velocidade * Time.deltaTime,0,0));  // sempre movimentando
		tempoNaDirecao += Time.deltaTime;  // incrementação do tempo

		if (tempoNaDirecao >= duracaoDirecao) {  // quando o tempo estourar 
			tempoNaDirecao = 0;  // zera o tempo
			DirecaoX = DirecaoX * -1;
			sprite.flipX = !sprite.flipX;
		}
	}

	void Atack1(){
		if(dispara){
			GameObject cloneBala = Instantiate (Tiro, origemBala.position, origemBala.rotation);
			float aux = Random.Range (1, 3);

			if (sprite.flipX) {
				cloneBala.transform.eulerAngles = new Vector3 (0, 0, 180);
				origemBala.position = new Vector3 (this.transform.position.x - 0.6f, origemBala.position.y, origemBala.position.z);
			} else {
				cloneBala.transform.eulerAngles = new Vector3 (0, 0, 0);
				origemBala.position = new Vector3 (this.transform.position.x + 0.6f, origemBala.position.y, origemBala.position.z);
			}

			Invoke ("Atack1", aux);
		}

	}

}