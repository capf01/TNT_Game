using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem : MonoBehaviour
{
    [Header("UI")]
    public Image itemImage; //imagem da UI que vai ganhar cor/sprite da lata
    public Sprite originalSprite;
    public Sprite mangoSprite;
    public Sprite zeroSprite;
    public Sprite focusSprite;
    public Sprite acaiSprite;

    [Header("Som")]
    public AudioClip getItemSound;
    private AudioSource audioSource;

    [Header("PowerUps")]
    public PlayerController playerController; //dash e pulo duplo
    public ScriptableStats taroDevStats; //dash e pulo duplo
    public PowerFocusPinkLemonade powerFocusPinkLemonade; //script de visualizar o invisivel
    public Destroy_Cesar destroy;  // destruir parede
    public PushOrDestroy_Cesar pushOrDestroy;  // empurrar parede



    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); //AudioSource do player
    }

    private void OnTriggerEnter2D(Collider2D collision) //checar tag do objeto coletado e atualizar a imagem da UI

    {
        if (collision.gameObject.CompareTag("Original"))
        {
            AtualizarUI(originalSprite, "Original");
            Destroy(collision.gameObject); //remover item da cena

            //verificar pulo duplo
            if (playerController != null)
            {
                playerController.EnableDoubleJump(); // habilitar o pulo duplo no PlayerController
            }
            Debug.Log("Pulo pulo ativado");
        }

        else if (collision.gameObject.CompareTag("Mango"))
        {
            AtualizarUI(mangoSprite, "Mango");
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.CompareTag("Zero"))
        {
            AtualizarUI(zeroSprite, "Zero");
            Destroy(collision.gameObject);

            //verificar dash
            if (taroDevStats != null && taroDevStats.AllowDash)
            {
                if (playerController != null)
                {
                    playerController.ActivateDash(); // habilitar o dash no PlayerController
                }
            }
            Debug.Log("Dash ativado");

        }

        else if (collision.gameObject.CompareTag("Focus"))
        {
            AtualizarUI(focusSprite, "Focus");
            Destroy(collision.gameObject);

            // ativar a visualiza  o do invis vel
            if (powerFocusPinkLemonade != null)
            {
                powerFocusPinkLemonade.ActivateVisionReveal();
                Debug.Log("Poder de visualiza  o ativado");
            }
        }

        else if (collision.gameObject.CompareTag("Acai"))
        {
            AtualizarUI(acaiSprite, "Acai");
            Destroy(collision.gameObject);

            //ativar destruir a parede
            if (destroy != null)
            {
                destroy.CheckForDestruction();
                Debug.Log("destruiu a parede");
            }

            //ativar empurrar a parede
            if (pushOrDestroy != null)
            {
                pushOrDestroy.PushObject();
                Debug.Log("empurrou a parede");
            }
        }
    }

    //att ui
    private void AtualizarUI(Sprite novoSprite, string itemNome)
    {
        Debug.Log($"Player pegou a lata {itemNome}");

        if (itemImage != null && novoSprite != null)
        {
            itemImage.sprite = novoSprite; //att sprite na ui
            itemImage.color = Color.white; //colocar cor branca no sprite na ui
        }

        PlayGetItem(); //tocar som
    }

    //som quando pega item
    private void PlayGetItem()
    {
        if (audioSource != null && getItemSound != null)
        {
            audioSource.PlayOneShot(getItemSound);
        }
    }
}
/* L GICA SUPONDO QUE ADD NO PLAYERCONTROLLER 
public void ActivateDash() //DASH
{
    if (_canDash) 
    {
        HandleDash(); 
    }
}

public void EnableDoubleJump() //PULO DUPLO
{
    if (taroDevStats != null && taroDevStats.MaxAirJumps > 0) {
        _airJumpsRemaining = taroDevStats.MaxAirJumps;
        Debug.Log("Pulo Duplo Ativado!");
    }
}

/* TAG ACAI - CESAR; Destroy_Cesar e PushOrDestroy_Cesar 
add refer ncias do CollectItem aos scripts Destroy_Cesar e PushOrDestroy_Cesar:

*/