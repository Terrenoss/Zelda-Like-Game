using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SaveData
{
    public Vector3 playerPosition;
    public string mapBoundary;
    public Sprite playerSprite;
    public List<InventorySaveData> inventorySaveData;
}
