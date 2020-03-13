using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Configuracao : MonoBehaviour {
	public Sprite somAtivado;
	public Sprite somDesativado;

	public Button BtnSom;

	void Update () {
		if(PlayerPrefs.GetInt ("Som")==1){
			BtnSom.image.sprite = somAtivado;
		}else{
			BtnSom.image.sprite = somDesativado;
		}
	}

	public void AtivaSom () {
		if(PlayerPrefs.GetInt ("Som")==1){
			PlayerPrefs.SetInt ("Som", 0);
		}else{
			PlayerPrefs.SetInt ("Som", 1);
		}
	}
}
