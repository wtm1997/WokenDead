using UnityEngine;

public abstract class UIViewBase : UIBase
{
	private Transform _parent;

	public override void Initialize()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		base.Initialize();
		Transform val = gameObject.transform;
		val.SetParent(_parent, false);
		val.localScale = new Vector3(1f, 1f, 1f);
		Show();
	}

	public void SetParent(Transform parent)
	{
		_parent = parent;
	}

	public void Show()
	{
		if (!gameObject.activeSelf)
		{
			gameObject.SetActive(true);
		}
		_isShow = true;
		onShow();
	}

	public void Hide()
	{
		if (gameObject.activeSelf)
		{
			gameObject.SetActive(false);
		}
		_isShow = false;
		onHide();
	}

	public void ShowWithoutActive()
	{
		_isShow = true;
		onShow();
	}

	public void HideWithoutInactive()
	{
		_isShow = false;
		onHide();
	}
}
