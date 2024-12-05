using UnityEngine;
using System.Collections.Generic;

public class WindArea : MonoBehaviour
{
    public Vector2 windForce; // Direção e força do vento
    private List<Rigidbody2D> affectedObjects = new List<Rigidbody2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && !affectedObjects.Contains(rb))
            {
                affectedObjects.Add(rb);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null && affectedObjects.Contains(rb))
            {
                affectedObjects.Remove(rb);
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody2D rb in affectedObjects)
        {
            if (rb != null)
            {
                rb.AddForce(windForce, ForceMode2D.Force);
            }
        }
    }
}
