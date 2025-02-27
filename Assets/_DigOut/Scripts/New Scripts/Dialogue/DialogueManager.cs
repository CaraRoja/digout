using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {



	public GameObject dialoguePanel;
	public TextMeshProUGUI nameText;
	public TextMeshProUGUI dialogueText;

	private Queue<string> names;
	private Queue<string> sentences;
	public string nameOfDialogueInExecution;

	public bool isDialogueActive = false;
	public bool textIsUpdating = false;

	public List<DialogueTrigger> List_Of_Dialogues;


	private void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
		names = new Queue<string>();

		DialogueGameObjectsInCanvas dialogueGameObjects = GameObject.Find("Canvas").GetComponent<DialogueGameObjectsInCanvas>();
		dialoguePanel = dialogueGameObjects.DialoguePanel;
		nameText = dialogueGameObjects.nameCharacter;
		dialogueText = dialogueGameObjects.dialogue;

		StartCoroutine(PlayDialogueOnSceneWithDelay("DialogoTeste", 1f));


	}

    void Update()
    {
		SkipDialogue();
    }


    //M�todo que iniciar� o procedimento para dar play ao di�logo espec�fico, passando a string
    public void PlayDialogueOnScene(string name)
	{
		nameOfDialogueInExecution = name;

		if (List_Of_Dialogues != null)
		{
            foreach (DialogueTrigger dialogueOBJ in List_Of_Dialogues)
            {
                if (dialogueOBJ.name.Equals(name) && !dialogueOBJ.playedBefore && dialogueOBJ.canPlay)
                {
                    dialogueOBJ.canPlay = false;
                    isDialogueActive = true;


                    //game.player.playerIsInDialogue = true;
                    dialoguePanel.SetActive(true);
                    dialogueOBJ.playedBefore = true;            //Informar que j� o executou, para n�o repetir (Real uso dele � para os que n�o s�o repet�veis)
                    StartDialogue(dialogueOBJ.dialogue);
                    break;
                }
            }
        }
		


	}

	public IEnumerator PlayDialogueOnSceneWithDelay(string name, float time)
    {
        yield return new WaitForSeconds(time);
        if (List_Of_Dialogues != null)
		{
            nameOfDialogueInExecution = name;
            foreach (DialogueTrigger dialogueOBJ in List_Of_Dialogues)
            {
                if (dialogueOBJ.name.Equals(name) && !dialogueOBJ.playedBefore && dialogueOBJ.canPlay)
                {
                    dialogueOBJ.canPlay = false;
                    isDialogueActive = true;

                    //game.player.playerIsInDialogue = true;
                    dialoguePanel.SetActive(true);
                    dialogueOBJ.playedBefore = true;            //Informar que j� o executou, para n�o repetir (Real uso dele � para os que n�o s�o repet�veis)
                    StartDialogue(dialogueOBJ.dialogue);
                    break;
                }
            }
        }

		
	}

	//M�todo que inicia o di�logo
	public void StartDialogue (Dialogue dialogue)
	{

		names.Clear();
		sentences.Clear();

		foreach (string name in dialogue.names)
		{
			names.Enqueue(name);
		}

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		//Debug.Log("NAME OF LOCUTOR: " + names.Peek());
		if (names.Peek().Equals("Iordz"))
        {
			//IordzImage.SetActive(true);
        }
		else if (names.Peek().Equals("Filosofino"))
		{
			//FilosofinoImage.SetActive(true);
		}
		else if (names.Peek().Equals("Mari"))
		{
			//MariImage.SetActive(true);
		}

		DisplayNextSentence();
	}




	//M�todo que mostra o pr�ximo texto dos di�logos
	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string nameofsequence = names.Dequeue();
		string sentence = sentences.Dequeue();



		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence, nameofsequence));
	}

	public void SkipDialogue()
	{
        if (DialogueIsRunning() && Input.GetKeyDown(KeyCode.E))
        {
            if (textIsUpdating)
            {
                textIsUpdating = false;
            }
            else
            {
                DisplayNextSentence();
            }

        }
    }


	//M�todo que mostra sequencialmente cada letra da frase
	IEnumerator TypeSentence (string sentence, string name)
	{
		textIsUpdating = true;	
		nameText.text = name;
		dialogueText.text = "";

		foreach (char letter in sentence.ToCharArray())
		{
			if (!textIsUpdating)
			{
				dialogueText.text = sentence;
				break;
            }
			dialogueText.text += letter;
			yield return new WaitForSeconds(0.02f);
		}
		textIsUpdating = false;
	}


	//M�todo que finaliza o di�logo
	void EndDialogue()
	{
		//Ao finalizar o di�logo, � necess�rio dizer para a lista de di�logos os que podem repetir ou n�o
        foreach (DialogueTrigger item in List_Of_Dialogues)
        {
			//Se o item pode repetir, ent�o desative a flag que avisa que j� executou alguma vez, porque o flag � usado apenas para n�o repetir na pr�xima execu��o
			if (item.canRepeat)
            {
				item.playedBefore = false;
            }
        }

		dialoguePanel.SetActive(false);
			
		isDialogueActive = false;  //Informar para a classe que o di�logo n�o est� mais em execu��o
		//animator.SetBool("IsOpen", false); N�O EST� SENDO USADO

		nameOfDialogueInExecution = null;
	}


	public bool DialogueIsRunning()
    {
		return isDialogueActive;
    }

	public IEnumerator UpdateInitialDialogueInfo(string dialogueName)
    {
		yield return new WaitForSeconds(0.2f);
		foreach (DialogueTrigger item in List_Of_Dialogues)
		{
			//Se o item pode repetir, ent�o desative a flag que avisa que j� executou alguma vez, porque o flag � usado apenas para n�o repetir na pr�xima execu��o
			if (item.name.Equals(dialogueName))
			{
				item.playedBefore = true;
			}
		}
	}

	public void AuthorizeDialogues()
	{
		foreach (DialogueTrigger dialogueOBJ in List_Of_Dialogues)
		{
			dialogueOBJ.canPlay = true;
		}
	}

}
