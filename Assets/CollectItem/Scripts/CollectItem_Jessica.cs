using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectItem_Jessica : MonoBehaviour
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
        }
        else if (collision.gameObject.CompareTag("Focus"))
        {
            AtualizarUI(focusSprite, "Focus");
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Acai"))
        {
            AtualizarUI(acaiSprite, "Acai");
            Destroy(collision.gameObject);
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
