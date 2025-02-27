using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

	//Classe utilizada para guardar informações dos nomes e setenças dos diálogos
	public string[] names;

	[TextArea(3, 10)]
	public string[] sentences;

}
