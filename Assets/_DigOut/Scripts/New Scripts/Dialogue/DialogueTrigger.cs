using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Classe que será posta em gameobjects que irão guardar os diálogos e pra indicar se já executou ou não
public class DialogueTrigger : MonoBehaviour {

	public bool canPlay = true;
	public bool canRepeat;
	public bool playedBefore = false; //Flag pra indicar se ele já rodou anteriormente ou não
	public Dialogue dialogue;


    private void Start()
    {
		canPlay = true;
    }
}
