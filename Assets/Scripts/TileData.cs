using System;

[Serializable]
public class TileData
{
    public int x, y, z;
    public int rotation;   // 0, 90, 180, 270度
    public EdgeData edgeData;
    public string tileName;
}
