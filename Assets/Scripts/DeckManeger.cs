using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<GameObject> tilePrefabs;
    private List<GameObject> deck;
    public DrawnTileUI tileUI;        // UIの参照

    private GameObject heldTile; // 現在「手札」で保持しているタイル
    private Sprite heldTileSprite; // UI表示用スプライト

    private GameObject previewTile;

    private Vector3 lastSnapPos = Vector3.positiveInfinity;

    void Start()
    {
        // シャッフル用に山札をコピー
        deck = new List<GameObject>(tilePrefabs);
        ShuffleDeck();

        // 最初の1枚を引いて表示
        DrawOneTile();
    }


    void Update()
    {
        if (heldTile != null && Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // 2DなのでZ座標は0固定

            // グリッドサイズ（1ユニットごと）
            float gridSize = 2.5f;

            // 📌 グリッドにスナップ
            float x = Mathf.Round(mouseWorldPos.x / gridSize) * gridSize;
            float y = Mathf.Round(mouseWorldPos.y / gridSize) * gridSize;
            Vector3 snappedPos = new Vector3(x, y, 0f);


            if (previewTile == null)
            {
                previewTile = Instantiate(heldTile, snappedPos, Quaternion.identity);
                lastSnapPos = snappedPos;
            }
            else
            {
                if (snappedPos == lastSnapPos)
                {
                    previewTile.transform.Rotate(0f, 0f, -90f);
                }
                else
                {
                    previewTile.transform.position = snappedPos;
                    lastSnapPos = snappedPos;
                }
            }
            //heldTile = null;
            //ileUI.ClearTile();

            //DrawOneTile();
        }
    }



    // 山札をシャッフル（Fisher–Yates）
    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int randomIndex = Random.Range(i, deck.Count);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }


    public void ConfirmPlacement()
    {
        if (previewTile != null)
        {
            Vector3 finalPos = previewTile.transform.position;
            Quaternion finalRot = previewTile.transform.rotation;

            Instantiate(heldTile, finalPos, finalRot); // 本物として配置


            // 確定配置：そのままにして previewTile を null にする
            previewTile = null;
            lastSnapPos = Vector3.positiveInfinity;

            // 手持ちをクリア
            heldTile = null;
            tileUI.ClearTile();

            // 次のタイルを引く
            DrawOneTile();
        }
    }

    // 山札の先頭から1枚引いて表示
    public void DrawOneTile()
    {
        if (deck.Count == 0)
        {
            Debug.Log("山札が空です");
            return;
        }

        GameObject tile = deck[0];
        deck.RemoveAt(0);

        // ここでインスタンス化（コピー）して手持ちタイルにする！
        heldTile = tile;

        // UI表示用
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            tileUI.ShowTile(sr.sprite);
        }

        /*heldTile = tile; // 今持ってるカードとして保持
        heldTileSprite = tile.GetComponent<SpriteRenderer>().sprite;
        tileUI.ShowTile(heldTileSprite);*/

    }
}
