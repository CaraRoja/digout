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


    //Método que iniciará o procedimento para dar play ao diálogo específico, passando a string
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
                    dialogueOBJ.playedBefore = true;            //Informar que já o executou, para não repetir (Real uso dele é para os que não são repetíveis)
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
                    dialogueOBJ.playedBefore = true;            //Informar que já o executou, para não repetir (Real uso dele é para os que não são repetíveis)
                    StartDialogue(dialogueOBJ.dialogue);
                    break;
                }
            }
        }

		
	}

	//Método que inicia o diálogo
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




	//Método que mostra o próximo texto dos diálogos
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


	//Método que mostra sequencialmente cada letra da frase
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


	//Método que finaliza o diálogo
	void EndDialogue()
	{
		//Ao finalizar o diálogo, é necessário dizer para a lista de diálogos os que podem repetir ou não
        foreach (DialogueTrigger item in List_Of_Dialogues)
        {
			//Se o item pode repetir, então desative a flag que avisa que já executou alguma vez, porque o flag é usado apenas para não repetir na próxima execução
			if (item.canRepeat)
            {
				item.playedBefore = false;
            }
        }

		dialoguePanel.SetActive(false);
			
		isDialogueActive = false;  //Informar para a classe que o diálogo não está mais em execução
		//animator.SetBool("IsOpen", false); NÃO ESTÁ SENDO USADO

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
			//Se o item pode repetir, então desative a flag que avisa que já executou alguma vez, porque o flag é usado apenas para não repetir na próxima execução
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
