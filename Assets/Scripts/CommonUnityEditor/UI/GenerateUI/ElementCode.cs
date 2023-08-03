public class ElementCodeDefine
{
    public static string imgCompFormat =
        @"
    [NonSerialized]
    public Image #命名#Img;";

    public static string btnCompFormat =
        @"
    [NonSerialized]
    public Button #命名#Btn;";

    public static string rawImgCompFormat =
        @"
    [NonSerialized]
    public RawImage #命名#RawImg;";

    public static string txtCompFormat =
        @"
    [NonSerialized]
    public TextMeshProUGUI #命名#Txt;";

    public static string scrollCompFormat =
        @"
    [NonSerialized]
    public EnhancedScroller #命名#Scroll;";
    
    public static string scrollCellCompFormat =
        @"
    public EnhancedScrollerCellView[] #命名#Cell;";

    public static string togCompFormat =
        @"
    [NonSerialized]
    public Toggle #命名#Tog;";

    public static string inputFieldCompFormat =
        @"
    [NonSerialized]
    public TMP_InputField #命名#;";
    
    public static string scrollDelegateCompFormat =
        @"
    [NonSerialized]
    public BaseScrollDelegate #命名#;";
    
    public static string sliderCompFormat =
        @"
    [NonSerialized]
    public Slider #命名#;";
    
    public static string viewBaseCompFormat =
        @"
    [NonSerialized]
    public UIViewBase #命名#;";
    public static string dropDownCompFormat =
        @"
    [NonSerialized]
    public TMP_Dropdown #命名#;";
    
    public static string objCompFormat =
        @"
    [NonSerialized]
    public GameObject #命名#;";
}

public class ElementCodeInitDefine
{
    public static string imgCompInitFormat =
        @"
        #名字#Img = this.transform.Find(#路径#).GetComponent<Image>();";

    public static string btnCompInitFormat =
        @"
        #名字#Btn = this.transform.Find(#路径#).GetComponent<Button>();";

    public static string rawImgCompInitFormat =
        @"
        #名字#RawImg = this.transform.Find(#路径#).GetComponent<RawImage>();";

    public static string txtCompInitFormat =
        @"
        #名字#Txt = this.transform.Find(#路径#).GetComponent<TextMeshProUGUI>();";

    public static string scrollCompInitFormat =
        @"
        #名字#Scroll = this.transform.Find(#路径#).GetComponent<EnhancedScroller>();";

    public static string togCompInitFormat =
        @"
        #名字#Tog = this.transform.Find(#路径#).GetComponent<Toggle>();";

    public static string inputFieldInitFormat =
        @"
        #名字# = this.transform.Find(#路径#).GetComponent<TMP_InputField>();";
    
    public static string scrollDelegateInitFormat =
        @"
        #名字# = this.transform.Find(#路径#).GetComponent<BaseScrollDelegate>();";
    
    public static string sliderInitFormat =
        @"
        #名字# = this.transform.Find(#路径#).GetComponent<Slider>();";
    
    public static string viewBaseInitFormat =
        @"
        #名字# = this.transform.Find(#路径#).GetComponent<UIViewBase>();";
    public static string dropDownInitFormat =
        @"
        #名字# = this.transform.Find(#路径#).GetComponent<TMP_Dropdown>();";
    public static string objInitFormat =
        @"
        #名字# = this.transform.Find(#路径#).gameObject;";
}