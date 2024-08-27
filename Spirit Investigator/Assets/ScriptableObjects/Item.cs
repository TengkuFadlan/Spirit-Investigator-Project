using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Texture itemImage;
}
