using System;
using UnityEngine;

namespace Stella.GameLogic.Character
{
    /// <summary>
    /// Character Model을 불러오는 Component
    /// </summary>
    public class CharacterModel : MonoBehaviour
    {

        public Transform Character;

        private CharacterBase CharacterBase;

        private void Awake()
        {
            CharacterBase = GetComponent<CharacterBase>();

            var characterData = CharacterBase.CharacterData;
            var modelPrefab = characterData.ModelPrefab;

            var instance = Instantiate(modelPrefab, transform);
            instance.transform.localPosition = characterData.LocalPosition;
            instance.transform.localRotation = Quaternion.Euler(characterData.LocalRotation);

            // TODO : 여기서 CharacterData를 불러와서 모델 프리팹을 로드하는 것을 만들고 싶다.
            Instantiate(Resources.Load("Prefebs/" + "Player"), new Vector3(0, 0, 0), Quaternion.identity);
            
        }
    }
}