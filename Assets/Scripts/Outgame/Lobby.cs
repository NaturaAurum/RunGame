using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Stella.Outgame
{
    public class Lobby : MonoBehaviour
    {
        [Required, SerializeField] private Button button = null;

        private void Awake()
        {
            button.OnClickAsObservable().Subscribe(_ =>
            {
                MapSelectPopup.Open();
            }).AddTo(this);
        }
    }
}
