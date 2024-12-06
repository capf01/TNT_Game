using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TarodevController;

public class DownStair : MonoBehaviour
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

            if (playerController != null)
            {
                if (playerController._isCrouching)
                {
                    m_BoxCollider.isTrigger = true;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController._downStairs = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController._downStairs = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_BoxCollider.isTrigger = false;
        }
    }
}