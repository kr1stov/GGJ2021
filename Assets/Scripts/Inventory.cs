using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "CG TOOLS/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public Item[] items;
    }
}