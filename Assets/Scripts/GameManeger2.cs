/*************************************************
*** Designer : 御堂
*** Date : 2025.6.8
*** Purpose : ゲーム管理
*************************************************/
using UnityEngine;
using System.Collections.Generic;

public class GameManager2 : MonoBehaviour
{
    public List<GameObject> tilePrefabs;  //登録した山札
    public GameObject startTile;  //スタートタイル
    private List<GameObject> deck;  //山札
    public DrawnTileUI tileUI;  //UIの参照
    private GameObject heldTile;  //現在「手札」で保持しているタイル
    private Sprite heldTileSprite;  //UI表示用スプライト
    private GameObject previewTile;  //仮置きタイル
    private Vector3 lastSnapTilePos = Vector3.positiveInfinity; //仮置きタイルが最後に置かれた場所
    private Vector3 lastSnapMeeplePos = Vector3.positiveInfinity; //仮置きタイルが最後に置かれた場所
    public GameObject Meeple;  //ミープル
    private GameObject previewMeeple;  //仮置きミープル

/************************************************
*** Function Name : start
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: 山札をシャッフルし、１枚引いてゲームをスタート
*** Return : void
************************************************/
    void Start()
    {
        //登録した山札をコピー
        deck = new List<GameObject>(tilePrefabs);
        ShuffleDeck();

        //スタートタイルの設置
        Instantiate(startTile, new Vector3(0f, 0f, 0f), Quaternion.identity);

        //最初の1枚を引く
        DrawOneTile();
    }

/************************************************
*** Function Name : Update
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: ゲーム進行
*** Return : void
************************************************/
    void Update()
    {
        TilePlace();
    }

/************************************************
*** Function Name : TilePlace
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: 右クリックを押したときにタイルを配置する
*** Return : void
************************************************/
    void TilePlace()
    {
        //左クリックが押された場合の処理
        if (heldTile != null && Input.GetMouseButtonDown(1)){
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            float gridSize = 2.5f;
            float x = Mathf.Round(mouseWorldPos.x / gridSize) * gridSize;
            float y = Mathf.Round(mouseWorldPos.y / gridSize) * gridSize;
            Vector3 snappedPos = new Vector3(x, y, 0f);
            //仮置きタイルの処理
            if (previewTile == null){
                previewTile = Instantiate(heldTile, snappedPos, Quaternion.identity);
                lastSnapTilePos = snappedPos;
            }else{
                //回転処理
                if (snappedPos == lastSnapTilePos){
                    previewTile.transform.Rotate(0f, 0f, -90f);
                }else{
                    previewTile.transform.position = snappedPos;
                    lastSnapTilePos = snappedPos;
                }
            }
        }
    }

/************************************************
*** Function Name : ShuffleDeck
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: 山札をシャッフルする
*** Return : void
************************************************/
    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++){
            int randomIndex = Random.Range(i, deck.Count);
            GameObject temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

/************************************************
*** Function Name : ConfirmPlacement
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: タイルやミープルの配置を確定する
*** Return : void
************************************************/
    public void ConfirmPlacement()
    {
        if (previewTile != null){
            Vector3 finalTilePos = previewTile.transform.position;
            Quaternion finalTileRot = previewTile.transform.rotation;

            //配置を確定
            Instantiate(heldTile, finalTilePos, finalTileRot);

            previewTile = null;
            lastSnapTilePos = Vector3.positiveInfinity;
            heldTile = null;
            tileUI.ClearTile();
            DrawOneTile();
            Turn.Instance.TurnNumPlus();
            if (MeepleNum.Instance.place != ""){
                if (Turn.Instance.TurnNum()%2 == 0){
                    MeepleNum.Instance.DecreaseR();
                }else{
                    MeepleNum.Instance.DecreaseB();
                }
            }
            MeepleNum.Instance.initplace();
        }
    }

/************************************************
*** Function Name : DrawOneTile
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: 山札から１枚引くとともに、UIに表示させる
*** Return : void
************************************************/
    public void DrawOneTile()
    {
        if (deck.Count == 0){
            Debug.Log("山札が空です");
            return;
        }
        GameObject tile = deck[0];
        deck.RemoveAt(0);
        heldTile = tile;

        //UI表示
        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
        if (sr != null){
            tileUI.ShowTile(sr.sprite);
        } 
    }
}
