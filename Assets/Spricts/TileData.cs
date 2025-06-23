// TileData.cs (ScriptableObject)
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTileData", menuName = "Carcassonne/Tile Data")]
public class TileData : ScriptableObject
{
    public GameObject tileModelPrefab; // 3Dモデルのプレハブ
    public Sprite tileSprite; // 2Dスプライト (必要に応じて)

    public enum FeatureType { None, Road, City, Monastery, Field }
    public enum Side { Top, Right, Bottom, Left }

    // 各辺が持つ特徴のタイプ (初期方向)
    public FeatureType topFeature;
    public FeatureType rightFeature;
    public FeatureType bottomFeature;
    public FeatureType leftFeature;

    // タイル内部の特徴 (例: 修道院があるか)
    public FeatureType centerFeature;

    // このタイルが持つ全ての特徴の具体的な定義
    // 各 TileData アセットで、このリストに特徴を追加していく
    public List<TileFeatureData> FeaturesData;

    /// <summary>
    /// タイルの回転を考慮して、指定された辺のFeatureTypeを返します。
    /// </summary>
    /// <param name="side">元の（回転していない状態での）辺。</param>
    /// <param name="rotationCount">現在の回転数（0, 1, 2, 3）。</param>
    /// <returns>回転後の指定された辺のFeatureType。</returns>
    public FeatureType GetFeature(Side side, int rotationCount)
    {
        // 回転数に基づいて、どの辺が現在のTop/Right/Bottom/Leftに当たるかを計算
        Side rotatedSide = (Side)(((int)side - rotationCount + 4) % 4); // 反時計回りに回転した辺を取得

        switch (rotatedSide)
        {
            case Side.Top: return topFeature;
            case Side.Right: return rightFeature;
            case Side.Bottom: return bottomFeature;
            case Side.Left: return leftFeature;
            default: return FeatureType.None;
        }
    }
}
