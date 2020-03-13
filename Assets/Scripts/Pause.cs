using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	public Animator anim;
	public List<Button> Botoes; 
	public float tempoSom=0.1f;

	public AudioSource clickSom;
	public string Cena;

	public Sprite somAtivado;
	public Sprite somDesativado;

	public Button BtnSom;

	void Start(){
		anim = GetComponent<Animator> ();
	}
		
	void Update () {
		if(PlayerPrefs.GetInt ("Som")==1){
			BtnSom.image.sprite = somAtivado;
		}else{
			BtnSom.image.sprite = somDesativado;
		}
	}

	public void Ajuda () {
		PlayerPrefs.SetString ("UltimaCena", SceneManager.GetActiveScene().name);
		//continuar


		ChamaCena ("Ajuda");
	}

	public void AtivaSom () {
		if(PlayerPrefs.GetInt ("Som")==1){
			PlayerPrefs.SetInt ("Som", 0);
		}else{
			PlayerPrefs.SetInt ("Som", 1);
		}
	}

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

	public void PausarJogo(){
		if (anim.GetBool ("Pause") == false) {
			anim.SetBool ("Pause", true);
			for(int i=0; i<Botoes.Count; i++){
				Botoes [i].interactable = true;
			}
		} else {
			anim.SetBool ("Pause", false);
			for(int i=0; i<Botoes.Count; i++){
				Botoes [i].interactable = false;
			}
		}
	}
}