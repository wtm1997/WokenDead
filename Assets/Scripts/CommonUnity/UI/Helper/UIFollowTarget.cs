using UnityEngine;

public class UIFollowTarget : MonoBehaviour
{
    public Transform target;
    public float height = 10f;

    private RectTransform _self;

    private void Awake()
    {
        _self = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 screenPos = World2Screen(target.position, height);
        _self.anchoredPosition = screenPos;
    }

    public static Vector3 World2Screen(Vector3 worldpos, float offsetY = 0)
    {
        Vector3 result = Camera.main.WorldToViewportPoint(worldpos);
        result.x *= 750f;
        result.y *= Screen.height * 750f / Screen.width;
        result.y += offsetY;
        result.z = 0f;
        return result;
    }
}