using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleItemReveal : MonoBehaviour
{
    public float detectionDistance = 2f;
    public bool hasRevealPower = true;
    private SpriteRenderer itemSpriteRenderer;

    void Update()
    {
        if (hasRevealPower)
        {
            DetectInvisibleItems();
        }
    }

    void DetectInvisibleItems()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, detectionDistance);

        foreach (Collider2D obj in objects)
        {
            if (obj.CompareTag("InvisibleItem"))
            {
                itemSpriteRenderer = obj.GetComponent<SpriteRenderer>();

                if (itemSpriteRenderer != null)
                {
                    if (!itemSpriteRenderer.enabled)
                    {
                        itemSpriteRenderer.enabled = true;
                    }

                    if (itemSpriteRenderer.color.a == 0f)
                    {
                        itemSpriteRenderer.color = new Color(itemSpriteRenderer.color.r, itemSpriteRenderer.color.g, itemSpriteRenderer.color.b, 0.5f);
                    }
                }
            }
        }
    }
}
