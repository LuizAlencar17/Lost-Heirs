using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecaoFases : MonoBehaviour {

	public Button[] Fases;

	// Use this for initialization
	void Start () {
		
		PlayerPrefs.SetInt ("Fase0", 1);

		for(int i=0; i<4; i++){
			if (PlayerPrefs.GetInt ("Fase" + (i).ToString ()) == 1) {
				Fases [i].interactable = true;
			} else {
				Fases [i].interactable = false;
			}
		}
	}

}
