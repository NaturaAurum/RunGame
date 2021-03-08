using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Stella.GameLogic.Environment
{
    public class SkyboxLoader : MonoBehaviour
    {
        [SerializeField] private List<GameObject> skyboxList = new List<GameObject>();

        private void Awake()
        {
            skyboxList[Random.Range(0, skyboxList.Count)].SetActive(true);
        }
    }
}
