using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Stella.Data.Enums;
using Stella.GameLogic.Manager;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Stella.GameLogic
{
    public class IngameUI : MonoBehaviour
    {
        [SerializeField, Required] private GameObject gameReadyGroup;
        [SerializeField, Required] private Text readyCountText;

        [SerializeField, Required] private GameObject gameOverGroup;
        [SerializeField, Required] private Button gameOverExitButton;

        [SerializeField, Required] private GameObject gameClearGroup;
        [SerializeField, Required] private Button gameClearExitButton;

        [SerializeField, Required] private GameObject hpOrigin;

        private List<GameObject> spawnedHp = new List<GameObject>();

        [SerializeField, Required] private Text timeText;

        private void Start()
        {
            Observable.Merge(
                gameOverExitButton.OnClickAsObservable(),
                gameClearExitButton.OnClickAsObservable()
            ).Subscribe(_ =>
            {
                SceneManager.LoadScene("Lobby");
            }).AddTo(this);

            var gameManager = GameManager.Instance;
            gameManager.CurrentState.Subscribe(state =>
            {
                gameReadyGroup.SetActive(state == GameState.Ready);
                gameOverGroup.SetActive(state == GameState.Over);
                gameClearGroup.SetActive(state == GameState.Clear);

                if (state == GameState.Ready)
                {
                    StartCoroutine(ReadyCountRoutine());
                }
            }).AddTo(this);

            var player = gameManager.CurrentPlayer;
            var hpCount = player.CharacterData.HpCount;
            
            for (var i = 0; i < hpCount; i++)
            {
                var instance = Instantiate(hpOrigin, hpOrigin.transform.parent);
                spawnedHp.Add(instance);
            }

            hpOrigin.SetActive(false);
            
            player.HP.Subscribe(hp =>
            {
                foreach (var hpObject in spawnedHp)
                {
                    hpObject.SetActive(false);
                }

                for (var i = 0; i < hp; i++)
                {
                    spawnedHp[i].SetActive(true);
                }
            }).AddTo(this);

            gameManager.PlayTime.Subscribe(time =>
            {
                var timeSpan = TimeSpan.FromSeconds(time);

                timeText.text = timeSpan.ToString(@"mm\:ss");
            }).AddTo(this);
        }

        private IEnumerator ReadyCountRoutine()
        {
            var time = 3;
            readyCountText.text = $"{time}";
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                readyCountText.text = $"{time}";
            }

            readyCountText.text = "Start!";
            yield return new WaitForSeconds(1);
            
            gameReadyGroup.SetActive(false);
            GameManager.Instance.SetState(GameState.Play);
        }
    }
}
