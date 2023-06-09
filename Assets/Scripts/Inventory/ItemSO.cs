using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Item", menuName = "Assets/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public Sprite itemWorldSprite;
}
