using System;
using UnityEngine;

namespace Stella.GameLogic.Character.Data
{
    [CreateAssetMenu(fileName = "New CharacterData")]
    public class CharacterData : ScriptableObject
    {
        public float AccelerationOnGround = 50f;
        public float MaxSpeedOnGround = 5f;

        public float AccelerationOnAir = 50f;
        public float MaxSpeedOnAir = 5f;

        public float GravityOnAir = 50f;

        public float MaxFallingSpeedOnAir = 3f;

        public int MaxJumpCount;

        public float Jump01Speed = 20f;
        public float Jump02Speed = 10f;

        public GameObject ModelPrefab;
        public Vector3 LocalPosition;
        public Vector3 LocalRotation;
        public Vector3 LocalScale = Vector3.one;
        
        public int HpCount = 3;

        public int HitstopFrame = 60;
        public Vector3 HitKnockbackDirection = new Vector3(-1, 1, 0);
        public float HitKnockbackPower = 30f;
        public Vector3 HitKnockback => HitKnockbackDirection * HitKnockbackPower;
    }
}
