using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCenas : MonoBehaviour {
	public float tempoSom=0.1f;
	public AudioSource clickSom;

	public static string Cena;

	public void ChamaCena (string other) {
		
		if(PlayerPrefs.GetInt("Som") == 1){
			clickSom.Play ();
		}
		Cena = other;

		Invoke("MudaCena", tempoSom);

	}

	void MudaCena () {
		SceneManager.LoadScene (Cena);
	}

	public void Retornar(){
		if(PlayerPrefs.GetInt("Som") == 1){
			clickSom.Play ();
		}
		Invoke("MudaCena", tempoSom);
	}
}
