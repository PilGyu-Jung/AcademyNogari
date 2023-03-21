using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Assets/Databases/Item Database")]
public class G_ItemDateBase : ScriptableObject
{
    public List<G_Item> allItems;

}
