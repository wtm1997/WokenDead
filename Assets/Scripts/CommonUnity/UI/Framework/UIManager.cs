using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CommonLogic;
using CommonUnity;
using CommonUnity.Resource;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
	private class Layer
	{
		public UILayerId id;

		public int order;

		public GameObject root;

		public List<UIPanelBase> list = new List<UIPanelBase>();

		public UIPanelBase Find(string name)
		{
			return list.Find((UIPanelBase ui) => name == ui.gameObject.name);
		}
	}

	public enum ItemBgColor
	{
		Brown,
		Green,
		Blue,
		Purple,
		Red,
		Yellow
	}

	private const int ORDER_STEP = 10;

	private List<Layer> _layers = new List<Layer>();

	private Dictionary<string, UILayerId> _name2layerDic = new Dictionary<string, UILayerId>();

	private StackList<string> uiStack = new StackList<string>();

	private UIRoot _root;

	public Dictionary<string, ConstructorInfo> UIHandlerDic = new Dictionary<string, ConstructorInfo>();

	private Dictionary<ItemBgColor, Sprite> ItemBgPool = new Dictionary<ItemBgColor, Sprite>();

	public UIRoot root => _root;

	public void Init()
	{
		GameObject obj = GameObject.Find("UIRoot");
		Transform transform = obj.transform;
		transform.localScale = Vector3.one;
		transform.localPosition = new Vector3(0f, 10000f, 0f);
		transform.localRotation = Quaternion.identity;
		_root = new UIRoot();
		_root.gameObject = obj;
		obj.AddComponent<DontDestroyMono>();
		_root.Init();
		_layers.Add(new Layer
		{
			id = UILayerId.Hud,
			order = 0
		});
		_layers.Add(new Layer
		{
			id = UILayerId.Panel,
			order = 10000
		});
		_layers.Add(new Layer
		{
			id = UILayerId.Pop,
			order = 20000
		});
		_layers.Add(new Layer
		{
			id = UILayerId.Top,
			order = 30000
		});
		_layers.Add(new Layer
		{
			id = UILayerId.GM,
			order = 32767
		});
		int i = 0;
		for (int count = _layers.Count; i < count; i++)
		{
			Layer layer = _layers[i];
			GameObject layerObj = new GameObject("layer-" + layer.id);
			RectTransform rect = layerObj.AddComponent<RectTransform>();
			rect.SetParent(_root.canvas.transform, false);
			rect.localScale = Vector3.one;
			rect.localPosition = Vector3.zero;
			rect.localRotation = Quaternion.identity;
			rect.anchoredPosition = Vector2.zero;
			rect.anchorMin = Vector2.zero;
			rect.anchorMax = Vector2.one;
			rect.sizeDelta = Vector2.zero;
			layer.root = layerObj;
		}
		CollectUI();
		DebugUIHandlerDic();
	}

	private void CollectUI()
	{
		Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			// Type[] types = Assembly.GetExecutingAssembly().GetTypes();
			Type[] types = assemblies[i].GetTypes();
			Type[] array = types;
			foreach (Type type in array)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(UIBaseHandlerAttribute), inherit: false);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					if (typeof(UIBase).IsAssignableFrom(type))
					{
						UIBaseHandlerAttribute attr = customAttributes[j] as UIBaseHandlerAttribute;
						ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
						ReflectionRegisterDic(attr, constructor);
					}
					else
					{
						Log.Error($"{type} is an invalid UILogicHandler");
					}
				}
			}
		}
	}

	private void ReflectionRegisterDic(UIBaseHandlerAttribute attr, ConstructorInfo ci)
	{
		if (UIHandlerDic.TryGetValue(attr.PrefabName, out var value))
		{
			Log.Error($"Prefab:{attr.PrefabName} already has a handler {value.DeclaringType}!");
		}
		else
		{
			UIHandlerDic.Add(attr.PrefabName, ci);
		}
	}

	private void DebugUIHandlerDic()
	{
		foreach (KeyValuePair<string, ConstructorInfo> item in UIHandlerDic)
		{
			Debug.Log((object)("【UIPrefabName】 UIPrefabName is " + item.Key));
		}
	}

	private Layer GetLayer(UILayerId id)
	{
		return _layers[(int)id];
	}

	public T AddView<T>(Transform parentTrans, string name) where T : UIViewBase
	{
		if (parentTrans == null)
		{
			Log.Error("add view without parent");
			return null;
		}
		GameObject gameObject = ResourcesManager.Inst.LoadObj(name);
		if (gameObject == null)
		{
			Log.Error("invalid ui prefab: " + name);
			return null;
		}
		if (UIHandlerDic.TryGetValue(name, out var value))
		{
			T t = value?.Invoke(null) as T;
			if (t == null)
			{
				Log.Error($"View = {name} is not int UIHandler");
				return null;
			}
			t.gameObject = gameObject;
			RectTransform rectTransform = gameObject.GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(parentTrans, false);
			rectTransform.localScale = Vector3.one;
			rectTransform.localPosition = Vector3.zero;
			rectTransform.localRotation = Quaternion.identity;
			rectTransform.anchoredPosition = Vector2.zero;
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.sizeDelta = Vector2.zero;
			t.BindUI();
			t.onCreate();
			t.onShow();
			return t;
		}
		return null;
	}

	public UIPanelBase Add(UIPanelBase ui)
	{
		Layer layer = GetLayer(ui.layerId);
		UIPanelBase uIPanelBase = layer.Find(ui.gameObject.name);
		if (uIPanelBase != null)
		{
			Log.Error("invalid ui instance found: " + ui);
			return null;
		}
		GameObject gameObject = ui.gameObject;
		RectTransform rect = gameObject.GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
		rect.SetParent(layer.root.transform, false);
		rect.localScale = Vector3.one;
		rect.localPosition = Vector3.zero;
		rect.localRotation = Quaternion.identity;
		rect.anchoredPosition = Vector2.zero;
		rect.anchorMin = Vector2.zero;
		rect.anchorMax = Vector2.one;
		rect.sizeDelta = Vector2.zero;
		ui.Initialize();
		int count = layer.list.Count;
		UIPanelBase uIPanelBase2 = ((count == 0) ? null : layer.list[count - 1]);
		Canvas canvas = ui.canvas;
		canvas.overrideSorting = true;
		canvas.sortingOrder = ((uIPanelBase2 == null) ? layer.order : (uIPanelBase2.canvas.sortingOrder + 10));
		GraphicRaycaster raycaster = ui.raycaster;
		layer.list.Add(ui);
		return ui;
	}

	public void Remove(UIPanelBase ui)
	{
		Layer layer = GetLayer(ui.layerId);
		layer.list.Remove(ui);
		ui.gameObject.SetActive(false);
	}

	public UIPanelBase Find(string name)
	{
		UILayerId value = UILayerId.Hud;
		if (!_name2layerDic.TryGetValue(name, out value))
		{
			return null;
		}
		Layer layer = _layers[(int)value];
		return layer.Find(name);
	}

	public T Find<T>(string name) where T : UIPanelBase
	{
		return Find(name) as T;
	}

	public T ShowSync<T>(string name) where T : UIPanelBase
	{
		return ShowSync(name) as T;
	}

	public void Show(string name)
	{
		UIPanelBase uIPanelBase = Find(name);
		ConstructorInfo value;
		if (uIPanelBase != null)
		{
			HandleUINeedBackToLastUI(uIPanelBase);
			if (!uIPanelBase.isShow)
			{
				uIPanelBase.Show(name);
			}
		}
		else if (UIHandlerDic.TryGetValue(name, out value))
		{
			UIPanelBase uIPanelBase2 = (UIPanelBase)(value?.Invoke(null));
			if (uIPanelBase2 == null)
			{
				Log.Error("[UIManager] UIPanelBase reflection failed!");
				return;
			}
			uIPanelBase2.InitializeParams();
			TaskManager.Inst.StartTask(PrepareShowUI(name, uIPanelBase2));
		}
		else
		{
			Log.Error($"[UIManager] UI Name : {name} has no Attribute!");
		}
	}

	public UIPanelBase ShowSync(string name)
	{
		UIPanelBase uIPanelBase = Find(name);
		if (uIPanelBase != null)
		{
			HandleUINeedBackToLastUI(uIPanelBase);
			if (!uIPanelBase.isShow)
			{
				uIPanelBase.Show(name);
			}
			return uIPanelBase;
		}
		if (UIHandlerDic.TryGetValue(name, out var value))
		{
			UIPanelBase uIPanelBase2 = (UIPanelBase)(value?.Invoke(null));
			if (uIPanelBase2 == null)
			{
				Log.Error("[UIManager] UIPanelBase reflection failed!");
				return null;
			}
			uIPanelBase2.InitializeParams();
			PrepareShowUISync(name, uIPanelBase2);
			return uIPanelBase2;
		}
		Log.Error($"[UIManager] UI Name : {name} has no Attribute!");
		return null;
	}

	private IEnumerator PrepareShowUI(string name, UIPanelBase uiPanelBase)
	{
		yield return uiPanelBase.Prepare();
		GameObject go = ResourcesManager.Inst.LoadObj(name);
		GetLayer(uiPanelBase.layerId);
		go.name = name;
		uiPanelBase.gameObject = go;
		_name2layerDic[name] = uiPanelBase.layerId;
		HandleUINeedBackToLastUI(uiPanelBase);
		uiPanelBase.BindUI();
		yield return ConstructViewBase(uiPanelBase);
		uiPanelBase.onCreate();
		uiPanelBase.Show(name);
	}

	private void PrepareShowUISync(string name, UIPanelBase uiPanelBase)
	{
		uiPanelBase.Prepare();
		GameObject go = ResourcesManager.Inst.LoadObj(name);
		Layer layer = GetLayer(uiPanelBase.layerId);
		go.name = name;
		uiPanelBase.gameObject = go;
		_name2layerDic[name] = uiPanelBase.layerId;
		HandleUINeedBackToLastUI(uiPanelBase);
		uiPanelBase.BindUI();
		ConstructViewBaseSync(uiPanelBase);
		uiPanelBase.onCreate();
		uiPanelBase.Show(name);
	}

	private void ConstructViewBaseSync(UIBase uiBase)
	{
		if (uiBase.ViewNameList == null)
		{
			return;
		}
		for (int i = 0; i < uiBase.ViewNameList.Count; i++)
		{
			if (!UIHandlerDic.TryGetValue(uiBase.ViewNameList[i], out var value))
			{
				continue;
			}
			UIViewBase uIViewBase = (UIViewBase)(value?.Invoke(null));
			if (uIViewBase == null)
			{
				Log.Error($"[UIManager] UIBase -> {uiBase.ViewNameList[i]}, has no attribute");
				continue;
			}
			uIViewBase.InitializeParams();
			uIViewBase.Prepare();
			BindViewBase2Obj(uiBase.transform, uiBase.ViewNameList[i], uIViewBase);
			if (uIViewBase.gameObject == null)
			{
				Log.Error($"[UIManager] UIName UIBase -> {uiBase.gameObject.name}, has no prefab -> {uiBase.ViewNameList[i]}");
			}
			else
			{
				if (uiBase.UIViews == null)
				{
					uiBase.UIViews = new ListDictionary<string, UIViewBase>();
				}
				uiBase.UIViews.Add(uiBase.ViewNameList[i], uIViewBase);
				uIViewBase.BindUI();
				uIViewBase.onCreate();
			}
			ConstructViewBaseSync(uIViewBase);
		}
	}

	private IEnumerator ConstructViewBase(UIBase uiBase)
	{
		if (uiBase.ViewNameList == null)
		{
			yield break;
		}
		for (int i = 0; i < uiBase.ViewNameList.Count; i++)
		{
			if (UIHandlerDic.TryGetValue(uiBase.ViewNameList[i], out var ci))
			{
				UIViewBase viewBase = (UIViewBase)(ci?.Invoke(null));
				viewBase.InitializeParams();
				yield return viewBase.Prepare();
				BindViewBase2Obj(uiBase.transform, uiBase.ViewNameList[i], viewBase);
				if (viewBase.gameObject == null)
				{
					Log.Error($"[UIManager] UIName UIBase -> {uiBase.gameObject.name}, has no prefab -> {uiBase.ViewNameList[i]}");
				}
				else
				{
					if (uiBase.UIViews == null)
					{
						uiBase.UIViews = new ListDictionary<string, UIViewBase>();
					}
					uiBase.UIViews.Add(uiBase.ViewNameList[i], viewBase);
					viewBase.BindUI();
					viewBase.onCreate();
				}
				yield return ConstructViewBase(viewBase);
			}
			ci = null;
		}
	}

	private void BindViewBase2Obj(Transform parent, string viewName, UIViewBase viewBase)
	{
		for (int i = 0; i < parent.childCount; i++)
		{
			if (parent.GetChild(i).name == viewName)
			{
				viewBase.gameObject = ((Component)parent.GetChild(i)).gameObject;
				break;
			}
			BindViewBase2Obj(parent.GetChild(i), viewName, viewBase);
		}
	}

	private void HandleUINeedBackToLastUI(UIPanelBase ui)
	{
		if (ui.NeedBackToLastUI && !uiStack.Contains(ui.gameObject.name))
		{
			uiStack.Push(ui.gameObject.name);
		}
	}

	public void Hide(string name)
	{
		UIPanelBase uIPanelBase = Find(name);
		if (uIPanelBase != null)
		{
			uIPanelBase.Hide();
			
			CheckNeedOpenLastUI(uIPanelBase);
		}
	}

	public void CheckNeedOpenLastUI(UIPanelBase curHideBase)
	{
		if (curHideBase.NeedBackToLastUI)
		{
			if (uiStack.Peek() == curHideBase.gameObject.name)
			{
				uiStack.Pop();
			}
			else
			{
				Log.Error($"UI {(curHideBase.gameObject).name} 是被NeedBackToLastUI标记控制的，但是他不是最上层UI，却被Hide了，检查是否真的需要这样的操作？");
			}
			UIPanelBase uIPanelBase = Find(uiStack.Peek());
			if (uIPanelBase != null && !uIPanelBase.isShow)
			{
				Show(uiStack.Peek());
			}
		}
	}

	public void HideAll(UILayerId layerId)
	{
		Layer layer = GetLayer(layerId);
		for (int num = layer.list.Count - 1; num >= 0; num--)
		{
			UIPanelBase uIPanelBase = layer.list[num];
			uIPanelBase.Hide();
		}
	}

	public void ClearUIStack()
	{
		uiStack.Clear();
	}

	public bool IsPanelScaleOver(string name)
	{
		UIPanelBase uIPanelBase = Find(name);
		if (uIPanelBase == null)
		{
			return true;
		}
		return uIPanelBase.CheckPanelScaleOver() || !uIPanelBase.CheckPanelHasNeedDoScale();
	}
}
