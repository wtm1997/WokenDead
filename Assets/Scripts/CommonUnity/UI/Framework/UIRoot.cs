using UnityEngine;

public class UIRoot
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Canvas _canvas;

    public GameObject gameObject;

    public Camera camera => _camera;

    public Canvas canvas => _canvas;

    public Transform transform => gameObject.transform;

    public void Init()
    {
        _camera = _camera ?? ((Component)transform).GetComponentInChildren<Camera>();
        _canvas = _canvas ?? ((Component)transform).GetComponentInChildren<Canvas>();
    }
}