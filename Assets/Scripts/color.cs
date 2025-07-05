/*************************************************
*** Designer : 御堂
*** Date : 2025.7.3
*** Purpose : プレイヤーが入力した色の取得
*************************************************/
using UnityEngine;
using TMPro;

public class color : MonoBehaviour
{
    public TMP_InputField inputcolor; //プレイヤーが入力する色

    void Start()
    {
        if (inputcolor != null){
            inputcolor.onEndEdit.AddListener(Inputcolor);
        }
    }

/************************************************
*** Function Name : Inputcolor
*** Designer : 御堂
*** Date : 2025.7.3
*** Function: プレイヤーが入力した色の取得
*** Return : void
************************************************/
    public void Inputcolor(string color)
    {
        //R,,r,B,bが入力された時の処理
        if (color == "R" || color == "r"){
            MeepleNum.Instance.color = "R";
            MeepleNum.Instance.n = 1;
        }else if (color == "B" || color == "b"){
            MeepleNum.Instance.color = "B";
            MeepleNum.Instance.n = 1;
        }else{
            MeepleNum.Instance.color = "";
        }
        inputcolor.text = "";
    }
}