using UnityEngine;

public class CrossHairController : MonoBehaviour
{
    RectTransform _rectTransform;

    SubCrossHairController _subCrossHair;

    public SubCrossHairController _SubCrossHair { get { return _subCrossHair; } }

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        _subCrossHair = transform.GetChild(1).GetComponent<SubCrossHairController>();
    }

    void Update()
    {
        _rectTransform.anchoredPosition = ScreenManager._Instance._MousePosition;
    }
}
