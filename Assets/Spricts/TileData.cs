// TileData.cs (ScriptableObject)
using UnityEngine;
using System.Collections.Generic;

public class TileData : MonoBehaviour
{
    public enum FeatureType { reef, Road, City, Monastery, }

    // 各辺が持つ特徴のタイプ (初期方向)
    public FeatureType topFeature;
    public FeatureType rightFeature;
    public FeatureType bottomFeature;
    public FeatureType leftFeature;

    // タイル内部の特徴 (例: 修道院があるか)
    public FeatureType centerFeature

    public FeatureType[] edges = new FeatureType[4]

   
}
