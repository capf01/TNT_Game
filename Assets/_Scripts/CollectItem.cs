using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectItem : MonoBehaviour
{
    [Header("UI")]
    public Image itemImage; //imagem da UI que vai ganhar cor/sprite da lata
    public Sprite originalSprite;
    public Sprite mangoSprite;
    public Sprite zeroSprite;
    public Sprite focusSprite;
    public Sprite acaiSprite;
    public TMP_Text _debugText;

    [Header("Som")]
    public AudioClip getItemSound;
    private AudioSource audioSource;

    [Header("PowerUps")]
    public PlayerController playerController; //dash e pulo duplo
    public ScriptableStats taroDevStats; //dash e pulo duplo
    public PowerFocusPinkLemonade powerFocusPinkLemonade; //script de visualizar o invisivel

    public GameObject collectableParticles;
    //public Destroy_Cesar destroy;  // destruir parede
    //public PushOrDestroy_Cesar pushOrDestroy;  // empurrar parede



    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); //AudioSource do player
    }

    private void OnTriggerEnter2D(Collider2D collision) //checar tag do objeto coletado e atualizar a imagem da UI

    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Original"))
            {
                //AtualizarUI(originalSprite, "Original");
                _debugText.text = "TNT Original = Double Jump activated";
                taroDevStats.AllowDoubleJump = true;
                taroDevStats.AllowDash = false;
                taroDevStats.AllowAttacks = false;
                taroDevStats.AllowGrapplingHook = false;
                taroDevStats.AllowHyperFocus = false;
                Destroy(gameObject); //remover item da cena
            }
            else if (gameObject.CompareTag("Zero"))
            {
                //AtualizarUI(originalSprite, "Original");
                _debugText.text = "TNT Zero = Dash activated";
                taroDevStats.AllowDash = true;
                taroDevStats.AllowDoubleJump = false;
                taroDevStats.AllowAttacks = false;
                taroDevStats.AllowGrapplingHook = false;
                taroDevStats.AllowHyperFocus = false;
                Destroy(gameObject); //remover item da cena
            }
            else if (gameObject.CompareTag("Acai"))
            {
                //AtualizarUI(originalSprite, "Original");
                _debugText.text = "TNT Açai = Power Up";
                taroDevStats.AllowAttacks = true;
                taroDevStats.AllowDash = false;
                taroDevStats.AllowDoubleJump = false;
                taroDevStats.AllowGrapplingHook = false;
                taroDevStats.AllowHyperFocus = false;
                Destroy(gameObject); //remover item da cena
            }
            else if (gameObject.CompareTag("Mango"))
            {
                //AtualizarUI(originalSprite, "Original");
                _debugText.text = "TNT Mango = Grappling Hook Activated";
                taroDevStats.AllowGrapplingHook = true;
                taroDevStats.AllowAttacks = false;
                taroDevStats.AllowDash = false;
                taroDevStats.AllowDoubleJump = false;
                taroDevStats.AllowHyperFocus = false;
                Destroy(gameObject); //remover item da cena
            }
            else if (gameObject.CompareTag("PinkLemonade"))
            {
                //AtualizarUI(originalSprite, "Original");
                _debugText.text = "TNT PinkLemonade = Hyper Focus";
                taroDevStats.AllowHyperFocus = true;
                taroDevStats.AllowGrapplingHook = false;
                taroDevStats.AllowAttacks = false;
                taroDevStats.AllowDash = false;
                taroDevStats.AllowDoubleJump = false;
                Destroy(gameObject); //remover item da cena
            }

            else if (gameObject.CompareTag("Collectable"))
            {
                Instantiate(collectableParticles, transform.position, transform.rotation);
                Destroy(gameObject); //remover item da cena
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