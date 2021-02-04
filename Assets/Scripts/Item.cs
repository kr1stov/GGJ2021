using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "CG TOOLS/Items", order = 0)]
public class Item : ScriptableObject
{
    public enum ItemType { Poison, Key}
    public ItemType type;
    public Sprite icon;

    public void DoSomething()
    {
        Debug.Log("Do something");
    }
}
