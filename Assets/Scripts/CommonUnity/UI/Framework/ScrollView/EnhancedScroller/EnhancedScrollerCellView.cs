using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace EnhancedUI.EnhancedScroller
{
    /// <summary>
    /// This is the base class that all cell views should derive from
    /// </summary>
    public class EnhancedScrollerCellView
    {
        /// <summary>
        /// The cellIdentifier is a unique string that allows the scroller
        /// to handle different types of cells in a single list. Each type
        /// of cell should have its own identifier
        /// </summary>
        public string cellIdentifier;

        /// <summary>
        /// The cell index of the cell view
        /// This will differ from the dataIndex if the list is looping
        /// </summary>
        [NonSerialized]
        public int cellIndex;

        /// <summary>
        /// The data index of the cell view
        /// </summary>
        [NonSerialized]
        public int dataIndex;

        /// <summary>
        /// Whether the cell is active or recycled
        /// </summary>
        [NonSerialized]
        public bool active;
        
        private List<Material> _grayMaterialList;

        /// <summary>
        /// This method is called by the scroller when the RefreshActiveCellViews is called on the scroller
        /// You can override it to update your cell's view UID
        /// </summary>
        public virtual void RefreshCellView() { }

        public GameObject gameObject;
        public Transform transform => gameObject.transform;


        public virtual void BindUI(){}

        public virtual void RegisterEvents() { }
        
        public virtual void UnRegisterEvents() { }

        public virtual void onCreate()
        {
            RegisterEvents();
        }
        
        public virtual void onShow(){}
        
        public virtual void onHide(){}

        public virtual void onDestroy()
        {
            UnRegisterEvents();
        }
        
        public void SetImgGrayShaderOpen(Image img, bool open)
        {
            if (img.material == null) return;
        
            if (_grayMaterialList == null)
                _grayMaterialList = new List<Material>();
            Material material = Object.Instantiate(img.material);
            material.SetFloat("_Open", open ? 1 : 0);
            img.material = material;
            _grayMaterialList.Add(material);
        }
    }
}