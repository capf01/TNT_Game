using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueBox; // GameObject do painel de diálogo
    public Text speechText; // Componente Text do diálogo

    [Header("Settings")]
    public float typingSpeed = 0.05f; // Velocidade da digitação
    private string sentence; // Única sentença do diálogo
    private bool isDialogueActive = false; // Controle de estado do diálogo

    void Update()
    {
        // Verifica se o jogador pressionou Tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isDialogueActive)
            {
                StartDialogue(""); 
            }
            else
            {
                EndDialogue();
            }
        }
    }

    public void StartDialogue(string txt)
    {
        if (dialogueBox == null || speechText == null)
        {
            Debug.LogWarning("DialogueControl: dialogueBox ou speechText não foram atribuídos.");
            return;
        }

        if (!isDialogueActive) // Inicia o diálogo apenas se não estiver ativo
        {
            dialogueBox.SetActive(true); // Ativa o painel do diálogo
            sentence = txt; // Define o texto do diálogo
            StartCoroutine(TypeSentence()); // Começa a digitação da sentença
            isDialogueActive = true;
        }
    }

    IEnumerator TypeSentence()
    {
        speechText.text = ""; // Garante que o texto comece vazio
        foreach (char letter in sentence.ToCharArray())
        {
            speechText.text += letter; // Adiciona uma letra de cada vez
            yield return new WaitForSeconds(typingSpeed); // Aguarda pela velocidade de digitação
        }
    }

    public void EndDialogue()
    {
        if (isDialogueActive) // Só finaliza se o diálogo estiver ativo
        {
            dialogueBox.SetActive(false); // Desativa o painel do diálogo
            speechText.text = ""; // Reseta o texto
            isDialogueActive = false; // Define que o diálogo foi encerrado
            Debug.Log("Diálogo finalizado.");
        }
    }
}
