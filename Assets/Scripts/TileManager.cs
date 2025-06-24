using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.IO;

public class TileManager : MonoBehaviour
{
    [Header("タイルの設置対象Tilemap")]
    public Tilemap tilemap;

    [Header("設置に使うTile一覧（最大19個）")]
    public TileBase[] tileBases;

    [Header("盤面サイズ（正方形の1辺の長さ）")]
    public int boardSize = 142;

    private Dictionary<string, TileBase> nameToTile;

    private Dictionary<string, EdgeData> edgeDataDict = new Dictionary<string, EdgeData>();

    private int HalfSize => boardSize / 2;

    private int currentTileIndex = 0;

    // 「確定していない」仮置きタイルの座標（nullなら無し）
    private Vector3Int? tempTilePos = null;

    // 仮置きタイルの回転角度（0,90,180,270）
    private int tempTileRotation = 0;

void Start()
    {
        CustomTile[] customTiles = Resources.LoadAll<CustomTile>("Tiles"); // ← CustomTile型で読み込む
        tileBases = customTiles; // TileBase[] に格納（Inspector 用）

        nameToTile = new Dictionary<string, TileBase>();

        foreach (var tile in customTiles)
        {
            if (tile != null)
            {
                nameToTile[tile.name] = tile;
                edgeDataDict[tile.name] = tile.edgeData;

                Debug.Log($"読み込んだタイル: {tile.name}");
            }
        }

        // (0,0,0) に初期固定タイル設置（回転0度）
        if (tileBases != null && tileBases.Length > 0 && tileBases[0] != null)
        {
            tilemap.SetTile(Vector3Int.zero, tileBases[0]);
            tilemap.SetTransformMatrix(Vector3Int.zero, Matrix4x4.identity);
        }

        PickNextRandomTile();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // 右クリックで処理
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = tilemap.WorldToCell(mouseWorld);

            if (!IsInsideBoard(cell))
            {
                Debug.LogWarning("範囲外のセルです");
                return;
            }

            TileBase clickedTile = tilemap.GetTile(cell);

            if (clickedTile != null)
            {
                // タイルが既にあるセルをクリック

                if (cell == Vector3Int.zero)
                {
                    Debug.Log("初期タイルは操作できません");
                    return;
                }

                if (tempTilePos.HasValue && cell == tempTilePos.Value)
                {
                    RotateTempTile(cell);
                    SaveTilemap();
                    return;
                }
            }
            else
            {
                // 空セルクリック

                if (tempTilePos.HasValue)
                {
                    // 移動先が隣接セルかチェック
                    if (!IsAdjacentToExistingTile(cell))
                    {
                        Debug.LogWarning("隣接セルにしか置けません（移動時）");
                        return;
                    }

                    // 元の位置のタイルを消す
                    tilemap.SetTile(tempTilePos.Value, null);

                    // 新しい位置にタイルをセット
                    PlaceTempTile(cell);

                    Debug.Log($"仮置きタイルを {cell} に移動しました");
                }
                else
                {
                    if (IsAdjacentToExistingTile(cell))
                    {
                        PlaceTempTile(cell);
                        Debug.Log($"新しい仮置きタイルを {cell} に設置しました");
                    }
                    else
                    {
                        Debug.LogWarning("隣接セルにしか置けません");
                    }
                }
            }
        }

        // 確定キー (Enter) で仮置きタイルを確定
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (tempTilePos.HasValue)
            {
                tempTilePos = null;
                tempTileRotation = 0;
                PickNextRandomTile();
                SaveTilemap();
                Debug.Log("仮置きタイルを確定しました");
            }
        }

        // Lキーでロード
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadTilemap();
        }
    }

    void PlaceTempTile(Vector3Int cell)
    {
        TileBase tile = tileBases[currentTileIndex];
        if (tile == null)
        {
            Debug.LogError("tileBases にタイルが設定されていません");
            return;
        }

        if (!edgeDataDict.TryGetValue(tile.name, out EdgeData edgeData))
        {
            Debug.LogWarning($"'{tile.name}' に EdgeData が未登録");
            return;
        }

        if (!IsEdgeMatch(cell, edgeData, tempTileRotation))
        {
            Debug.LogWarning("辺の情報が隣接タイルと一致しません");
            return;
        }

        tilemap.SetTile(cell, tile);
        tilemap.SetTransformMatrix(cell, Matrix4x4.Rotate(Quaternion.Euler(0, 0, tempTileRotation)));

        tempTilePos = cell;
    }

    void RotateTempTile(Vector3Int cell)
    {
        // 90度ずつ回転（時計回り）
        tempTileRotation = (tempTileRotation + 90) % 360;
        tilemap.SetTransformMatrix(cell, Matrix4x4.Rotate(Quaternion.Euler(0, 0, tempTileRotation)));

        Debug.Log($"仮置きタイル {cell} を90度回転（現在 {tempTileRotation} 度）");
    }

    bool IsInsideBoard(Vector3Int cell)
    {
        return cell.x >= -HalfSize && cell.x < HalfSize &&
               cell.y >= -HalfSize && cell.y < HalfSize;
    }

    bool IsAdjacentToExistingTile(Vector3Int cell)
    {
        // 盤面内で、上下左右どれか隣にタイルがあるか確認
        Vector3Int[] neighbors = new Vector3Int[]
        {
            new Vector3Int(cell.x + 1, cell.y, cell.z),
            new Vector3Int(cell.x - 1, cell.y, cell.z),
            new Vector3Int(cell.x, cell.y + 1, cell.z),
            new Vector3Int(cell.x, cell.y - 1, cell.z),
        };

        foreach (var n in neighbors)
        {
            if (tilemap.GetTile(n) != null)
                return true;
        }

        return false;
    }

    void SaveTilemap()
    {
        TileMapSaveData saveData = new TileMapSaveData();

        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tilemap.GetTile(pos);
            if (tile != null)
            {
                Matrix4x4 mat = tilemap.GetTransformMatrix(pos);
                Quaternion rot = mat.rotation;
                // Z軸回転角度を0,90,180,270に丸める
                int rotZ = Mathf.RoundToInt(rot.eulerAngles.z / 90) * 90 % 360;

                saveData.tiles.Add(new TileData
                {
                    x = pos.x,
                    y = pos.y,
                    z = pos.z,
                    rotation = rotZ,
                    tileName = tile.name,
                    edgeData = edgeDataDict.ContainsKey(tile.name) ? edgeDataDict[tile.name] : new EdgeData()
                });
            }
        }

        string path = Application.persistentDataPath + "/tilemap.json";
        File.WriteAllText(path, JsonUtility.ToJson(saveData, true));
        Debug.Log($"保存完了: {path}");
    }

    void LoadTilemap()
    {
        string path = Application.persistentDataPath + "/tilemap.json";
        if (!File.Exists(path))
        {
            Debug.LogWarning("保存ファイルが見つかりません");
            return;
        }

        string json = File.ReadAllText(path);
        TileMapSaveData saveData = JsonUtility.FromJson<TileMapSaveData>(json);

        tilemap.ClearAllTiles();

        foreach (TileData data in saveData.tiles)
        {
            Vector3Int pos = new Vector3Int(data.x, data.y, data.z);
            if (nameToTile.TryGetValue(data.tileName, out TileBase tile))
            {
                tilemap.SetTile(pos, tile);
                tilemap.SetTransformMatrix(pos, Matrix4x4.Rotate(Quaternion.Euler(0, 0, data.rotation)));

                // ここで各辺の情報を使って判定処理などができる
                EdgeData e = data.edgeData;
                Debug.Log($"tile {data.tileName} edges: up={e.up} right={e.right} down={e.down} left={e.left}");
            }
        }


        // 読み込み後は仮置きタイルなしにリセット
        tempTilePos = null;
        tempTileRotation = 0;

        Debug.Log("読込完了");
    }

    void PickNextRandomTile()
    {
        if (tileBases == null || tileBases.Length == 0)
        {
            Debug.LogError("tileBases が空です。TileBaseをInspectorに設定してください。");
            return;
        }
        currentTileIndex = Random.Range(0, tileBases.Length);
    }

    EdgeData RotateEdgeData(EdgeData original, int rotation)
    {
        // 0度ならそのまま返す
        if (rotation == 0) return original;

        EdgeData rotated = new EdgeData();

        switch (rotation % 360)
        {
            case 90:
                rotated.up = original.left;
                rotated.right = original.up;
                rotated.down = original.right;
                rotated.left = original.down;
                break;
            case 180:
                rotated.up = original.down;
                rotated.right = original.left;
                rotated.down = original.up;
                rotated.left = original.right;
                break;
            case 270:
                rotated.up = original.right;
                rotated.right = original.down;
                rotated.down = original.left;
                rotated.left = original.up;
                break;
        }

        return rotated;
    }

    bool IsEdgeMatch(Vector3Int targetPos, EdgeData newTileEdges, int rotation)
    {
        EdgeData rotatedNew = RotateEdgeData(newTileEdges, rotation);

        // 隣接座標
        Vector3Int up = targetPos + Vector3Int.up;
        Vector3Int right = targetPos + Vector3Int.right;
        Vector3Int down = targetPos + Vector3Int.down;
        Vector3Int left = targetPos + Vector3Int.left;

        bool result = true;

        // 各方向の隣接タイルを取得して比較
        result &= MatchEdgeWithNeighbor(rotatedNew.up, down, Direction.down);
        result &= MatchEdgeWithNeighbor(rotatedNew.right, left, Direction.left);
        result &= MatchEdgeWithNeighbor(rotatedNew.down, up, Direction.up);
        result &= MatchEdgeWithNeighbor(rotatedNew.left, right, Direction.right);

        return result;
    }

    bool MatchEdgeWithNeighbor(Kind myEdge, Vector3Int neighborPos, Direction neighborSide)
    {
        TileBase neighborTile = tilemap.GetTile(neighborPos);
        if (neighborTile == null) return true;

        if (!edgeDataDict.TryGetValue(neighborTile.name, out EdgeData neighborEdge)) return false;

        Quaternion rot = tilemap.GetTransformMatrix(neighborPos).rotation;
        int rotZ = Mathf.RoundToInt(rot.eulerAngles.z / 90) * 90 % 360;
        EdgeData rotatedNeighbor = RotateEdgeData(neighborEdge, rotZ);

        Kind neighborValue = Kind.grass; // 初期値（必要なら例外対応も）

        switch (neighborSide)
        {
            case Direction.up: neighborValue = rotatedNeighbor.up; break;
            case Direction.right: neighborValue = rotatedNeighbor.right; break;
            case Direction.down: neighborValue = rotatedNeighbor.down; break;
            case Direction.left: neighborValue = rotatedNeighbor.left; break;
        }

        return myEdge == neighborValue;
    }

}
