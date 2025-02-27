using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Classe que ser� posta em gameobjects que ir�o guardar os di�logos e pra indicar se j� executou ou n�o
public class DialogueTrigger : MonoBehaviour {

	public bool canPlay = true;
	public bool canRepeat;
	public bool playedBefore = false; //Flag pra indicar se ele j� rodou anteriormente ou n�o
	public Dialogue dialogue;


    private void Start()
    {
		canPlay = true;
    }
}
