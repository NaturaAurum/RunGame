using Sirenix.OdinInspector;
using Stella.Data;
using Stella.GameLogic.Manager;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Stella.Outgame
{
    public class MapSelectPopup : MonoBehaviour
    {
        [Required, SerializeField] private GameObject origin = null;
        [Required, SerializeField] private Button closeButton = null;

        private static Subject<Unit> onOpen = new Subject<Unit>();
        public static void Open()
        {
            onOpen.OnNext(default);
        }

        private void Awake()
        {
            gameObject.SetActive(false);
            onOpen.Subscribe(_ => OpenInternal()).AddTo(this);
            
            origin.SetActive(false);
            var mapDataList = MapDataContainer.MapDataList;
            foreach (var mapData in mapDataList)
            {
                var instance = Instantiate(origin, origin.transform.parent);
                instance.SetActive(true);
                var button = instance.GetComponent<Button>();
                var text = instance.GetComponentInChildren<Text>();

                text.text = $"{mapData.Id.Theme}";

                button.OnClickAsObservable().Subscribe(_ =>
                {
                    // TODO : Load Game Scene with map data
                    MapDeliver.MapID = mapData.SubType;
                    SceneManager.LoadScene("GameScene");
                }).AddTo(this);
            }

            closeButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false)).AddTo(this);
        }

        private void OpenInternal()
        {
            gameObject.SetActive(true);
            
        }
    }
}