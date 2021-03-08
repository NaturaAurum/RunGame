using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Stella.Data;
using UnityEngine;

namespace Stella.GameLogic.Environment
{
    public class Block : MonoBehaviour
    {
        public ItemId Id;
        [Required, SerializeField] private SpriteRenderer spriteRenderer = null;
        [Required, SerializeField] private PolygonCollider2D polygonCollider2D = null;

        [Button]
        public virtual void SetSprite()
        {
            if (spriteRenderer == null)
                spriteRenderer = GetComponent<SpriteRenderer>();

            var sprite = BlockDataContainer.GetSprite(Id);
            spriteRenderer.sprite = sprite;

            if (polygonCollider2D == null)
                polygonCollider2D = GetComponent<PolygonCollider2D>();

            var path = new List<Vector2>();
            polygonCollider2D.pathCount = sprite.GetPhysicsShapeCount();
            for (var i = 0; i < polygonCollider2D.pathCount; i++)
            {
                path.Clear();
                sprite.GetPhysicsShape(i, path);
                polygonCollider2D.SetPath(i, path.ToArray());
            }
        }
    }
}
