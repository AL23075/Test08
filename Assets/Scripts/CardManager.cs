using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CardManager : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public List<GameObject> tilePrefabs;
    public DrawnTileUI tileUI;
    private List<GameObject> deck;
    private GameObject heldTile;
    private Sprite heldTileSprite;
    private GameObject previewTile;

    void Start() {
        deck = new List<GameObject>(tilePrefabs);
        ShuffleDeck();
        DrawOneTile();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

            if (heldTile != null) {
                SpriteRenderer sr = heldTile.GetComponent<SpriteRenderer>();
                if (sr != null) {
                    // タイルを動的に作成してスプライトを設定
                    Tile tile = ScriptableObject.CreateInstance<Tile>();
                    tile.sprite = sr.sprite;
                    tilemap.SetTile(cellPos, tile);
                    Debug.Log("クリックされた座標: " + cellPos);
                    Debug.Log("heldTile: " + heldTile);
                    if (sr != null) Debug.Log("Sprite: " + sr.sprite);
                }
            }
        }
    }

    void ShuffleDeck() {
        for (int i = 0; i < deck.Count; i++) {
            int randomIndex = Random.Range(i, deck.Count);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void DrawOneTile() {
        if (deck.Count == 0) {
            Debug.Log("山札が空です");
            return;
        }

        GameObject tile = deck[0];
        deck.RemoveAt(0);
        heldTile = tile;

        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
        if (sr != null) {
            tileUI.ShowTile(sr.sprite);  // UIにスプライト表示
        }
    }
}
