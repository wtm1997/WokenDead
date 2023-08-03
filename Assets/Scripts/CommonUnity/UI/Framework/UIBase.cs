using System;
using System.Collections;
using System.Collections.Generic;
using CommonLogic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;
using Object = System.Object;

public abstract class UIBase : IScaleable
{
	protected bool _isShow = false;

	[NonSerialized]
	public bool IsOpenScaleOver = false;

	public GameObject gameObject;

	public List<string> ViewNameList = null;

	public bool NeedDoScale = false;

	private List<RenderTexture> _rtList;

	private List<Camera> _rtCameraList;

	private List<Material> _grayMaterialList;

	[NonSerialized]
	public ListDictionary<string, UIViewBase> UIViews;

	public bool isShow => _isShow;

	public Transform transform => gameObject.transform;

	public virtual void InitializeParams()
	{
	}

	public virtual IEnumerator Prepare()
	{
		yield break;
	}

	protected void AddViewName(List<string> viewNameList)
	{
		if (ViewNameList == null)
		{
			ViewNameList = viewNameList;
		}
		else
		{
			Log.Error("【UIBase】 ui view 已经添加过了，检查为何还要继续添加？");
		}
	}

	public virtual void Initialize()
	{
	}

	public void ShowView(string[] viewName)
	{
		for (int i = 0; i < viewName.Length; i++)
		{
			ShowView(viewName[i]);
		}
	}

	public void ShowView(string viewName)
	{
		UIViewBase uiViewBase = null;
		if (UIViews.TryGetValue(viewName, out uiViewBase))
		{
			uiViewBase.Show();
		}
		else
		{
			Log.Error("没有找到名字为" + viewName + "的ViewBase");
		}
	}

	public void HideView(string[] viewName)
	{
		for (int i = 0; i < viewName.Length; i++)
		{
			HideView(viewName[i]);
		}
	}

	public void HideView(string viewName)
	{
		UIViewBase uiViewBase = null;
		if (UIViews.TryGetValue(viewName, out uiViewBase))
		{
			uiViewBase.Hide();
		}
		else
		{
			Log.Error("没有找到名字为" + viewName + "的ViewBase");
		}
	}

	public void ShowAllView()
	{
		for (int i = 0; i < UIViews.Count; i++)
		{
			UIViews.List[i].Show();
		}
	}

	public void HideAllView()
	{
		for (int i = 0; i < UIViews.Count; i++)
		{
			UIViews.List[i].Hide();
		}
	}

	public bool GetViewShowStatus(string viewName)
	{
		UIViewBase uiViewBase = null;
		if (UIViews.TryGetValue(viewName, out uiViewBase))
		{
			return uiViewBase.isShow;
		}
		return false;
	}

	public UIViewBase GetViewByName(string viewName)
	{
		if (UIViews == null)
		{
			return null;
		}
		UIViewBase result = null;
		if (UIViews.TryGetValue(viewName, out result))
		{
			return result;
		}
		return null;
	}

	public void Show3dObj(RawImage rawImage, string _3dObjPath, Vector2 pos, Vector3 rot, float scale = 1f, bool canDrag = false, bool clear = false)
	{
		if (clear)
		{
			Clear3dObj();
		}
		RenderTexture val = new RenderTexture(1024, 1024, 0);
		if (_rtList == null)
		{
			_rtList = new List<RenderTexture>();
		}
		_rtList.Add(val);
		val.depthStencilFormat = (GraphicsFormat)90;
		rawImage.texture = (Texture)(object)val;
		GameObject val2 = new GameObject("RTCamera");
		Camera val3 = val2.AddComponent<Camera>();
		if (_rtCameraList == null)
		{
			_rtCameraList = new List<Camera>();
		}
		_rtCameraList.Add(val3);
		_3dObjCameraSetting(val3, val, rawImage);
		// TODO: use resource manager
		// GameObject res = ResPoolManager.Instance.GetRes(_3dObjPath);
		// res.transform.SetParent(((Component)val3).transform, false);
		// SetChildLayer(res.transform, LayerMask.NameToLayer("Model"));
		// Animator animator = res.GetComponent<Animator>();
		// if (animator == null)
		// {
		// 	val4 = res.GetComponentInChildren<Animator>();
		// }
		// if ((Object)(object)val4 != (Object)null)
		// {
		// 	val4.Play("Idle");
		// }
		// res.transform.localPosition = new Vector3(pos.x, pos.y, 2000f);
		// res.transform.localRotation = Quaternion.Euler(rot.x, rot.y, rot.z);
		// res.transform.localScale = new Vector3(scale, scale, scale);
		// if (canDrag)
		// {
		// 	_3dObjDrag(rawImage, res);
		// }
	}

	public void SetChildLayer(Transform trans, int layer)
	{
		((Component)trans).gameObject.layer = layer;
		if (trans.childCount != 0)
		{
			for (int i = 0; i < trans.childCount; i++)
			{
				SetChildLayer(trans.GetChild(i), layer);
			}
		}
	}

	private void _3dObjCameraSetting(Camera camera, RenderTexture rt, RawImage rawImage)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		camera.targetTexture = rt;
		camera.cullingMask = 1 << LayerMask.NameToLayer("Model");
		((Component)camera).transform.SetParent(((Component)rawImage).transform);
		((Component)camera).transform.localPosition = new Vector3(10000f, 0f, 0f);
		((Component)camera).transform.localScale = new Vector3(1f, 1f, 1f);
		camera.orthographic = true;
		camera.clearFlags = (CameraClearFlags)2;
	}

	private void Clear3dObj()
	{
		ClearRT();
		if (_rtCameraList != null)
		{
			for (int i = 0; i < _rtCameraList.Count; i++)
			{
				UnityEngine.Object.Destroy(_rtCameraList[i].gameObject);
			}
			_rtCameraList = null;
		}
	}

	public void SetImgGrayShaderOpen(Image img, bool open)
	{
		if (!((Object)(object)((Graphic)img).material == (Object)null))
		{
			if (_grayMaterialList == null)
			{
				_grayMaterialList = new List<Material>();
			}
			Material val = UnityEngine.Object.Instantiate<Material>(((Graphic)img).material);
			val.SetFloat("_Open", (float)(open ? 1 : 0));
			((Graphic)img).material = val;
			_grayMaterialList.Add(val);
		}
	}

	private void ClearRT()
	{
		if (_rtList != null)
		{
			for (int i = 0; i < _rtList.Count; i++)
			{
				_rtList[i].Release();
			}
			_rtList = null;
		}
	}

	private void ClearMaterial()
	{
		if (_grayMaterialList != null)
		{
			for (int i = 0; i < _grayMaterialList.Count; i++)
			{
				UnityEngine.Object.DestroyImmediate(_grayMaterialList[i], true);
			}
			_grayMaterialList = null;
		}
	}

	public virtual void BindUI()
	{
	}

	public virtual void RegisterEvents()
	{
	}

	public virtual void UnRegisterEvents()
	{
	}

	public virtual void onCreate()
	{
		RegisterEvents();
	}

	public virtual void onShow()
	{
		DoScale();
		if (UIViews != null)
		{
			for (int i = 0; i < UIViews.Count; i++)
			{
				UIViews.List[i].onShow();
			}
		}
	}

	public virtual void onHide()
	{
		ClearRT();
	}

	public virtual void OnDestroy()
	{
		UnRegisterEvents();
		if (UIViews != null)
		{
			for (int i = 0; i < UIViews.Count; i++)
			{
				UIViews.List[i].OnDestroy();
			}
			UIViews.Clear();
			UIViews = null;
		}
	}

	public void DoScale()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Expected O, but got Unknown
		if (NeedDoScale)
		{
			IsOpenScaleOver = false;
			gameObject.transform.localScale = new Vector3(0.01f, 0.01f, 1f);
			// Sequence val = DOTween.Sequence();
			// TweenSettingsExtensions.Append(val, (Tween)(object)ShortcutExtensions.DOScale(gameObject.transform, new Vector3(1.15f, 1.15f, 1f), 0.15f));
			// TweenSettingsExtensions.Append(val, (Tween)(object)ShortcutExtensions.DOScale(gameObject.transform, new Vector3(1f, 1f, 1f), 0.05f));
			// TweenSettingsExtensions.SetUpdate<Sequence>(val, true);
			// ((Tween)val).onComplete = (TweenCallback)delegate
			// {
			// 	IsOpenScaleOver = true;
			// };
		}
	}

	public void DoScaleReverse(Action<string> callBack)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		if (NeedDoScale)
		{
			// Sequence val = DOTween.Sequence();
			// TweenSettingsExtensions.Append(val, (Tween)(object)ShortcutExtensions.DOScale(gameObject.transform, new Vector3(1.15f, 1.15f, 1f), 0.05f));
			// TweenSettingsExtensions.Append(val, (Tween)(object)ShortcutExtensions.DOScale(gameObject.transform, new Vector3(0.01f, 0.01f, 1f), 0.15f));
			// TweenSettingsExtensions.SetUpdate<Sequence>(val, true);
			// ((Tween)val).onComplete = (TweenCallback)delegate
			// {
			// 	callBack?.Invoke(((Object)gameObject).name);
			// };
		}
	}

	// public CoroutineHandler StartCoroutine(IEnumerator enumerator)
	// {
	// 	return CoroutineObject.StartCoroutine(enumerator);
	// }
	//
	// public void StopCoroutine(CoroutineHandler handler)
	// {
	// 	CoroutineObject.StopCoroutine(handler);
	// }
	//
	// public void PauseCoroutine(CoroutineHandler handler)
	// {
	// 	CoroutineObject.PauseCoroutine(handler);
	// }
	//
	// public void ResumeCoroutine(CoroutineHandler handler)
	// {
	// 	CoroutineObject.ResumeCoroutine(handler);
	// }
	//
	// public void StopAllCoroutines()
	// {
	// 	CoroutineObject.StopAllCoroutines();
	// }
}
