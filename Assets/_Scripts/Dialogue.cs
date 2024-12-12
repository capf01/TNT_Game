using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public string speechText; // Texto único do diálogo
    public LayerMask playerLayer; // Camada do jogador
    public float radius = 1.0f; // Raio de interação

    private DialogueControl dialogueControl;
    private bool onRadius; // Verifica se o jogador está dentro do raio de interação
    private bool isDialogueActive = false; // Controle de estado do diálogo

    void Start()
    {
        dialogueControl = FindObjectOfType<DialogueControl>(); // Encontrar o DialogueControl na cena
        if (dialogueControl == null)
        {
            Debug.LogError("DialogueControl não encontrado na cena.");
        }
    }

    void Update()
    {
        // Verifica se o jogador pressionou Tab e está no raio de interação
        if (Input.GetKeyDown(KeyCode.Tab) && onRadius)
        {
            if (!isDialogueActive) // Inicia o diálogo se ele não estiver ativo
            {
                StartDialogue();
            }
            else // Finaliza o diálogo se ele já estiver ativo
            {
                EndDialogue();
            }
        }
    }

    private void StartDialogue()
    {
        isDialogueActive = true; // Marca o diálogo como ativo
        dialogueControl.StartDialogue(speechText); // Inicia o diálogo no DialogueControl com o texto correto
        Debug.Log("Diálogo iniciado: " + speechText);
    }

    private void EndDialogue()
    {
        isDialogueActive = false; // Marca o diálogo como finalizado
        dialogueControl.EndDialogue(); // Finaliza o diálogo
        Debug.Log("Diálogo finalizado.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador entrou no raio de interação
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            onRadius = true; // Ativa o raio de interação
            Debug.Log("Jogador entrou no raio de interação.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o jogador saiu do raio de interação
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            onRadius = false; // Desativa o raio de interação
            Debug.Log("Jogador saiu do raio de interação.");
        }
    }
}
