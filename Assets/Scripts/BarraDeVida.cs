using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour {

	public Sprite[] sheets;
	public Jogador player;
	Image imagem;

	void Start () {
		imagem = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		imagem.sprite = sheets[player.life];
	}
}
