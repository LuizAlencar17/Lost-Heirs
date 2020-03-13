using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour {

	public float velocidade;  // velocidade do inimigo
	public float duracaoDirecao;  // duração para adar em uma direção
	private float tempoNaDirecao;  // quanto tempo ele está anadando nesta direção

	public int DirecaoX;

	void Start(){
		
	}

	// Update is called once per frame
	void FixedUpdate () {
		MovePlataforma ();

	}

	void MovePlataforma(){
		transform.Translate(new Vector3(DirecaoX * velocidade * Time.deltaTime,0,0));  // sempre movimentando
		tempoNaDirecao += Time.deltaTime;  // incrementação do tempo

		if (tempoNaDirecao >= duracaoDirecao) {  // quando o tempo estourar 
			tempoNaDirecao = 0;  // zera o tempo
			DirecaoX = DirecaoX * -1;
		}
	}

}