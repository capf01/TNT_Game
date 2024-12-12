using UnityEngine;
using TMPro; // Importa o namespace do TextMeshPro
using System.Collections;
using TarodevController;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private GameObject _pressStartText;
    [SerializeField] private MainMenuManager _menuManager;
    public TMP_Text displayText;  // Texto que será exibido usando TextMeshPro
    public string fullText = "Este é o texto que vai aparecer aos poucos, como uma máquina de escrever.";
    private float typingSpeed = 0.1f;  // Velocidade normal de digitação
    private bool isTyping = false;
    private PlayerInput _input;
    private bool canPressStart;

    protected virtual void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }

    void Start()
    {
        StartCoroutine(TypeText());
    }

    void Update()
    {
        // Aumenta a velocidade de digitação se o botão X (ou outro) for pressionado
        if (_input.FrameInput.JumpHeld) // "Fire1" geralmente é o botão "X" em controladores, ou o botão esquerdo do mouse
        {
            typingSpeed = 0.01f; // Acelera o texto
        }
        else
        {
            typingSpeed = 0.06f; // Velocidade normal
        }

        // Exibe todo o texto imediatamente se o botão Fire2 for pressionado
        if (_input.FrameInput.Start) // "Fire2" geralmente é o botão direito do mouse
        {
            StopAllCoroutines();  // Para qualquer corrotina de digitação em andamento
            displayText.text = fullText;  // Exibe o texto completo imediatamente
            OnTextFinished();  // Chama o método que você deseja após o texto ser exibido
        }

        if (_input.FrameInput.Start && canPressStart)
        {
            _menuManager.StartGameFinally();
        }
    }

    IEnumerator TypeText()
    {
        int index = 0;
        while (index < fullText.Length)
        {
            displayText.text += fullText[index];
            index++;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Chama o método após o término da digitação
        OnTextFinished();
    }

    // Método que será chamado após a exibição do texto
    void OnTextFinished()
    {
        _pressStartText.SetActive(true);
        StartCoroutine(CanPressDelay());
    }

    IEnumerator CanPressDelay()
    {
        yield return new WaitForSeconds(0.1f);
        canPressStart = true;
    }
}
