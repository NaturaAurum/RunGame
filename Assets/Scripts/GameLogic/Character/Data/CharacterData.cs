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
    }
}
