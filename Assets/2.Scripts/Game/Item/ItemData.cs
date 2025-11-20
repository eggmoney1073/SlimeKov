using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public Sprite _icon;
    public int _xSize;
    public int _ySize;
    public int _maxStack;
}
