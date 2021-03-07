using System;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// Character 피격처리 담당
    /// </summary>
    public class CharacterHurtBox : MonoBehaviour
    {
        private CharacterBase character = null;

        private Collider collider;

        private void Awake()
        {
            character = GetComponent<CharacterBase>();
        }
        
        
    }
}