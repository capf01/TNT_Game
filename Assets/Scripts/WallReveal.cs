using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReveal : MonoBehaviour
{
    public float detectionDistance = 2f;
    public LayerMask wallLayer;
    private bool isNearFalseWall = false;

    void Update()
    {
        DetectFalseWall();
    }

    void DetectFalseWall()
    {
        Collider2D[] walls = Physics2D.OverlapCircleAll(transform.position, detectionDistance, wallLayer);

        if (walls.Length > 0)
        {
            foreach (Collider2D wall in walls)
            {
                SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();

                if (wallSpriteRenderer != null)
                {
                    wallSpriteRenderer.color = new Color(wallSpriteRenderer.color.r, wallSpriteRenderer.color.g, wallSpriteRenderer.color.b, 0.5f);
                    wall.isTrigger = true;
                }
            }
        }
        else
        {
            foreach (Collider2D wall in walls)
            {
                SpriteRenderer wallSpriteRenderer = wall.GetComponent<SpriteRenderer>();
                if (wallSpriteRenderer != null)
                {
                    wallSpriteRenderer.color = new Color(wallSpriteRenderer.color.r, wallSpriteRenderer.color.g, wallSpriteRenderer.color.b, 1f);
                    wall.isTrigger = false;
                }
            }
        }
    }
}
