using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectItem : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text _debugText;

    [Header("PowerUps")]
    public ScriptableStats taroDevStats;
    public string PowerUpName;
    public GameObject collectableParticles;

    public void Original()
    {
        Debug.Log("Ativou Original");
        taroDevStats.AllowDoubleJump = true;
        taroDevStats.AllowDash = false;
        taroDevStats.AllowAttacks = false;
        taroDevStats.AllowGrapplingHook = false;
        taroDevStats.AllowHyperFocus = false;
    }

    public void Zero()
    {
        taroDevStats.AllowDash = true;
        taroDevStats.AllowDoubleJump = false;
        taroDevStats.AllowAttacks = false;
        taroDevStats.AllowGrapplingHook = false;
        taroDevStats.AllowHyperFocus = false;
    }

    public void Acai()
    {
        taroDevStats.AllowAttacks = true;
        taroDevStats.AllowDash = false;
        taroDevStats.AllowDoubleJump = false;
        taroDevStats.AllowGrapplingHook = false;
        taroDevStats.AllowHyperFocus = false;
    }

    public void Mango()
    {
        taroDevStats.AllowGrapplingHook = true;
        taroDevStats.AllowAttacks = false;
        taroDevStats.AllowDash = false;
        taroDevStats.AllowDoubleJump = false;
        taroDevStats.AllowHyperFocus = false;
    }

    public void PinkLemonade()
    {
        taroDevStats.AllowHyperFocus = true;
        taroDevStats.AllowGrapplingHook = false;
        taroDevStats.AllowAttacks = false;
        taroDevStats.AllowDash = false;
        taroDevStats.AllowDoubleJump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) //checar tag do objeto coletado e atualizar a imagem da UI

    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Original"))
            {
                _debugText.text = "TNT Original = Double Jump activated";
                Original();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Zero"))
            {
                _debugText.text = "TNT Zero = Dash activated";
                Zero();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Acai"))
            {
                _debugText.text = "TNT Açai = Power Up";
                Acai();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Mango"))
            {
                _debugText.text = "TNT Mango = Grappling Hook Activated";
                Mango();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("PinkLemonade"))
            {
                _debugText.text = "TNT PinkLemonade = Hyper Focus";
                PinkLemonade();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("Collectable"))
            {
                Instantiate(collectableParticles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}