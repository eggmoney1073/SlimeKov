using UnityEngine;

public class SelectedSlotController : MonoBehaviour
{
    [Header("Slot Size")]
    [SerializeField] int _x;
    [SerializeField] int _y;

    [Header("Slot Position")]
    [SerializeField] int _n;
    [SerializeField] int _m;

    const float FirstSlot_Position_X = 2f;
    const float FirstSlot_Position_y = -3f;

    const float Base_Slot_Size = 85f;

    RectTransform _rectTransform;

    /// <summary>
    /// 칸의 크기를 정수로 입력하여 정확한 사이즈를 설정합니다.
    /// </summary>
    /// <param name="x"> 1 <= int x <= 6 </param>
    /// <param name="y"> 1 <= int y <= 9 </param>    
    public void SetSelectedSlotSize(int x, int y)
    {
        _rectTransform.sizeDelta = new Vector2(x * Base_Slot_Size, y * Base_Slot_Size);
    }


    /// <summary>
    /// 칸의 위치를 정수로 입력하여 정확한 위치를 설정합니다.
    /// </summary>
    /// <param name="n"> 0 <= int x <= 5 </param>
    /// <param name="m"> 0 <= int y <= 8 </param>    
    public void SetSelectedSlotPosition(int n, int m)
    {
        _rectTransform.anchoredPosition = new Vector2
        (FirstSlot_Position_X + (n * Base_Slot_Size),FirstSlot_Position_y -(m * Base_Slot_Size));
    }

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        SetSelectedSlotPosition(_n, _m);
        SetSelectedSlotSize(_x, _y);
    }
}
