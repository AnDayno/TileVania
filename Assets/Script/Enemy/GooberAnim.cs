using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PolygonCollider2D), typeof(SpriteRenderer))]
public class GooberAnim : MonoBehaviour
{
    PolygonCollider2D poly;
    SpriteRenderer spr;
    Sprite lastSprite;

    void Awake()
    {
        poly = GetComponent<PolygonCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        lastSprite = null;
    }

    void LateUpdate()
    {
        if (spr.sprite != lastSprite)
        {
            ApplyShape(spr.sprite);
            lastSprite = spr.sprite;
        }
    }

    void ApplyShape(Sprite s)
    {
        int count = s.GetPhysicsShapeCount();
        poly.pathCount = count;
        for (int i = 0; i < count; i++)
        {
            var pts = new List<Vector2>();
            s.GetPhysicsShape(i, pts);
            poly.SetPath(i, pts.ToArray());
        }
    }
}
