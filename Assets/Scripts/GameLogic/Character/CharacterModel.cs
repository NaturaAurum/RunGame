using System;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// Character Model을 불러오는 Component
    /// </summary>
    public class CharacterModel : MonoBehaviour
    {
        private CharacterBase _characterBase = null;

        private void Awake()
        {
            // TODO : 여기서 CharacterData를 불러와서 모델 프리팹을 로드하는 것을 만들고 싶다.
        }
    }
}