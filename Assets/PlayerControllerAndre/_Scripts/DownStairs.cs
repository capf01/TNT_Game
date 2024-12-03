using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class DownStairs : MonoBehaviour
{
    private BoxCollider2D m_BoxCollider;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (collision.gameObject.CompareTag("Player"))
        {
            playerController._downStairs = true;
            Debug.Log("Esta no Player");

            if (playerController != null)
            {
                if (playerController._isCrouching)
                {
                    Debug.Log("O Player está abaixado (_crouching == true)");
                    m_BoxCollider.isTrigger = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController._downStairs = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController._downStairs = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_BoxCollider.isTrigger = false;
    }
}
