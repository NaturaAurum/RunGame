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

        public float MaxJumpCount;

        public float Jump01Speed = 20f;
        public float Jump02Speed = 10f;

        public GameObject ModelPrefab;

        public Vector3 LocalPosition { get; internal set; }
        public Vector3 LocalRotation { get; internal set; }
    }
}
