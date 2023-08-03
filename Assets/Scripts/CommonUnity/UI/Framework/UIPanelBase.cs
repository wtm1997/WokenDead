using System.Collections.Generic;
using CommonLogic;
using UnityEngine;
using UnityEngine.UI;

public enum UILayerId
{
	Hud,
	Panel,
	Pop,
	Top,
	GM
}

public class UIPanelBase : UIBase
{
	[SerializeField]
	private UILayerId _layerId;

	[SerializeField]
	private bool _needBackToLastUI = true;

	private Canvas _canvas;

	private GraphicRaycaster _raycaster;

	private string _uiName = "";

	private ListDictionary<string, UIViewBase> _needReverseView = null;

	public UILayerId layerId
	{
		get
		{
			return _layerId;
		}
		set
		{
			_layerId = value;
		}
	}

	public Canvas canvas => _canvas;

	public GraphicRaycaster raycaster => _raycaster;

	public bool NeedBackToLastUI
	{
		get
		{
			return _needBackToLastUI;
		}
		set
		{
			_needBackToLastUI = value;
		}
	}

	public override void Initialize()
	{
		_canvas = gameObject.AddComponent<Canvas>();
		_raycaster = gameObject.AddComponent<GraphicRaycaster>();
		base.Initialize();
	}

	public void Show(string uiName, bool needAdd = true)
	{
		_uiName = uiName;
		_isShow = true;
		if (needAdd)
		{
			UIManager.Inst.Add(this);
		}
		onShow();
	}

	public void Hide()
	{
		if (UIViews == null)
		{
			ViewHideInternal();
			return;
		}
		CollectNeedReverse(UIViews.List);
		if (_needReverseView != null)
		{
			for (int i = 0; i < _needReverseView.List.Count; i++)
			{
				_needReverseView.List[i].DoScaleReverse(CheckViewsReverseOver);
			}
		}
		else
		{
			ViewHideInternal();
		}
	}

	private void CheckViewsReverseOver(string viewName)
	{
		_needReverseView.Remove(viewName);
		if (_needReverseView.Count == 0)
		{
			ViewHideInternal();
		}
	}

	public void CollectNeedReverse(List<UIViewBase> views)
	{
		UIViewBase uiViewBase = null;
		for (int i = 0; i < views.Count; i++)
		{
			if (views[i].NeedDoScale)
			{
				if (_needReverseView == null)
				{
					_needReverseView = new ListDictionary<string, UIViewBase>();
				}
				if (!_needReverseView.TryGetValue(((Object)views[i].gameObject).name, out uiViewBase))
				{
					_needReverseView.Add(((Object)views[i].gameObject).name, views[i]);
				}
			}
			if (views[i].UIViews != null && views[i].UIViews.Count != 0)
			{
				CollectNeedReverse(views[i].UIViews.List);
			}
		}
	}

	private void ViewHideInternal()
	{
		if (UIViews != null && UIViews.Count != 0)
		{
			for (int i = 0; i < UIViews.Count; i++)
			{
				UIViewBase uIViewBase = UIViews.List[i];
				uIViewBase.Hide();
			}
		}
		InternalHide();
	}

	private void InternalHide()
	{
		if (NeedDoScale)
		{
			DoScaleReverse(PanelInternalHide);
		}
		else
		{
			PanelInternalHide(((Object)gameObject).name);
		}
	}

	private void PanelInternalHide(string panelName)
	{
		if (panelName == gameObject.name)
		{
			_isShow = false;
			UIManager.Inst.Remove(this);
			onHide();
			OnDestroy();
			Object.Destroy((Object)(object)gameObject.gameObject);
		}
	}

	public bool CheckPanelHasNeedDoScale()
	{
		bool flag = false;
		flag |= NeedDoScale;
		if (UIViews != null && UIViews.Count != 0)
		{
			for (int i = 0; i < UIViews.Count; i++)
			{
				flag |= UIViews.List[i].NeedDoScale;
			}
		}
		return flag;
	}

	public bool CheckPanelScaleOver()
	{
		bool flag = false;
		if (NeedDoScale)
		{
			flag |= IsOpenScaleOver;
		}
		if (UIViews != null && UIViews.Count != 0)
		{
			for (int i = 0; i < UIViews.Count; i++)
			{
				if (UIViews.List[i].NeedDoScale)
				{
					flag |= UIViews.List[i].IsOpenScaleOver;
				}
			}
		}
		return flag;
	}
}
