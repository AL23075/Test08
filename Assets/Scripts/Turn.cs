/*************************************************
*** Designer : 御堂
*** Date : 2025.6.23
*** Purpose : ターンの表示
*************************************************/
using UnityEngine;
using TMPro;

public class Turn : MonoBehaviour
{
    public static Turn Instance; //インスタンスの定義
    public int turnNum = 0; //ターン数
    public TMP_Text TurnText; //ターンを表示させるUI
/************************************************
*** Function Name : Awake
*** Designer : 御堂
*** Date : 2025.6.23
*** Function: インスタンス定義
*** Return : void
************************************************/
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
/************************************************
*** Function Name : TurnNum
*** Designer : 御堂
*** Date : 2025.6.23
*** Function: ターン数を返す
*** Return : int
************************************************/
    public int TurnNum()
    {
        return turnNum;
    }
/************************************************
*** Function Name : TurnNumPlus,Update
*** Designer : 御堂
*** Date : 2025.6.23
*** Function: ターンの増加とテキスト処理
*** Return : void
************************************************/
    public void TurnNumPlus()
    {
        turnNum++;
    }
    void Update()
    {
        if (turnNum%2 == 0){
            TurnText.text = $"player1's Turn";
        }else{
            TurnText.text = $"player2's Turn";
        }
    }
}