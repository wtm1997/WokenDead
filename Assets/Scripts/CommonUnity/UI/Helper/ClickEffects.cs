using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CommonUnity
{

    public class ClickEffects : MonoBehaviour
    {
        public Camera uiCamera;
        public RectTransform parent;
        public GameObject[] effectGos;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            foreach (var go in effectGos)
            {
                if (!go.activeSelf)
                {
                    ShowEffect(go);
                    break;
                }
            }
        }

        private async void ShowEffect(GameObject go)
        {
            go.SetActive(true);

            // Vector3 clickPoint = uiCamera.ScreenToWorldPoint(Input.mousePosition);

            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (parent, Input.mousePosition, uiCamera, out Vector2 localPos);

            go.GetComponent<RectTransform>().anchoredPosition = localPos;

            await Task.Delay(500);

            go.SetActive(false);
        }
    }

}
