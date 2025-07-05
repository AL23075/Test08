/*************************************************
*** Designer : 御堂
*** Date : 2025.6.30
*** Purpose : ミープルの数の操作
*************************************************/
using UnityEngine;
public class MeepleNum : MonoBehaviour
{
    public static MeepleNum Instance;  //インスタンスの定義
    public int MeepleR = 7;  //赤ミープルの初期数
    public int MeepleB = 7;  //青ミープルの初期数
    public string place = ""; //ミープルを置いた場所
    public int n = 0;  //タイル、ミープルを置ける状態
    public string color = "";  //プレイヤーが入力した色
/************************************************
*** Function Name : Awake
*** Designer : 御堂
*** Date : 2025.6.30
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
*** Function Name : DecreaseR,DecreaseB,IncreaseR,IncreaseB
*** Designer : 御堂
*** Date : 2025.6.30
*** Function: 駒の増減処理
*** Return : void
************************************************/
    public void DecreaseR()
    {
        MeepleR--;
    }
    public void DecreaseB()
    {
        MeepleB--;
    }
    public void IncreaseR()
    {
        MeepleR++;
    }
    public void IncreaseB()
    {
        MeepleB++;
    }
/************************************************
*** Function Name : setplace,initplace
*** Designer : 御堂
*** Date : 2025.6.30
*** Function: 駒を置いた場所の取得
*** Return : void
************************************************/
    public void setplace(string indplace)
    {
        place = indplace;
    }
    public void initplace()
    {
        place = "";
    }
/************************************************
*** Function Name : Meepleconfirm,okMeeple
*** Designer : 御堂
*** Date : 2025.7.3
*** Function: 駒の設置、除去するときの状態処理
*** Return : void
************************************************/
    public void Meepleconfirm()
    {
        n = 0;
        Turn.Instance.TurnNumPlus();
    }
    public void okMeeple()
    {
        n = 0;
    }
}