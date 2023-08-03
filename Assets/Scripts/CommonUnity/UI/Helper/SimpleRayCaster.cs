using System;
using System.Collections.Generic;
using CommonUnity.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CommonUnity
{

    //GraphicRaycaster，极简版 ，不支持BlockObjects ,UICanvas 必须指定Camera
    public class SimpleRayCaster : GraphicRaycaster
    {
        public Camera uiCamera;

        //必须设定Camera
        public override Camera eventCamera => uiCamera;

        protected override void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private Canvas _canvas;

        [NonSerialized] private readonly List<Graphic> _raycastResults = new List<Graphic>();

        public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
        {
            if (_canvas == null)
                return;

            var canvasGraphics = GraphicRegistry.GetGraphicsForCanvas(_canvas);
            if (canvasGraphics == null || canvasGraphics.Count == 0)
                return;

            int displayIndex;
            var currentEventCamera = eventCamera; //cach
            displayIndex = currentEventCamera.targetDisplay;

            var eventPosition = Display.RelativeMouseAt(eventData.position);
            if (eventPosition != Vector3.zero)
            {
                int eventDisplayIndex = (int)eventPosition.z;
                if (eventDisplayIndex != displayIndex)
                    return;
            }
            else
            {
                eventPosition = eventData.position;
            }

            // Convert to view space
            var pos = currentEventCamera.ScreenToViewportPoint(eventPosition);
            if (pos.x < 0f || pos.x > 1f || pos.y < 0f || pos.y > 1f)
                return;

            _raycastResults.Clear();
            Raycast(_canvas, currentEventCamera, eventPosition, canvasGraphics, _raycastResults);

            int totalCount = _raycastResults.Count;
            for (var index = 0; index < totalCount; index++)
            {
                var go = _raycastResults[index].gameObject;
                bool appendGraphic = true;

                if (ignoreReversedGraphics)
                {
                    var cameraFoward = currentEventCamera.transform.rotation * Vector3.forward;
                    var dir = go.transform.rotation * Vector3.forward;
                    appendGraphic = Vector3.Dot(cameraFoward, dir) > 0;
                }

                if (appendGraphic)
                {
                    float distance = 0;

                    Ray ray = new Ray();
                    ray = currentEventCamera.ScreenPointToRay(eventPosition);
                    Transform trans = go.transform;
                    Vector3 transForward = trans.forward;
                    distance = (Vector3.Dot(transForward, trans.position - currentEventCamera.transform.position) / Vector3.Dot(transForward, ray.direction));
                    if (distance < 0)
                        continue;
                    var castResult = new RaycastResult
                    {
                        gameObject = go,
                        module = this,
                        distance = distance,
                        screenPosition = eventPosition,
                        index = resultAppendList.Count,
                        depth = _raycastResults[index].depth,
                        sortingLayer = _canvas.sortingLayerID,
                        sortingOrder = _canvas.sortingOrder
                    };
                    resultAppendList.Add(castResult);
                }
            }
        }

        private static void Raycast(Canvas canvas, Camera eventCamera, Vector2 pointerPosition, IList<Graphic> foundGraphics, List<Graphic> results)
        {
            int totalCount = foundGraphics.Count;
            Graphic upGraphic = null;
            int upIndex = -1;
            for (int i = 0; i < totalCount; ++i)
            {
                var graphic = foundGraphics[i];
                if (!graphic.raycastTarget)
                    continue;
                int depth = graphic.depth;
                if (depth == -1 || graphic.canvasRenderer.cull)
                    continue;

                if (!RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, pointerPosition, eventCamera))
                    continue;

                if (eventCamera != null && eventCamera.WorldToScreenPoint(graphic.rectTransform.position).z > eventCamera.farClipPlane)
                    continue;

                if (graphic.Raycast(pointerPosition, eventCamera))
                {
                    if (depth > upIndex)
                    {
                        upIndex = depth;
                        upGraphic = graphic;
                    }
                }
            }
            if (upGraphic != null)
                results.Add(upGraphic);
        }
    }

}