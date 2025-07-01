using UnityEngine;
public class MeepleNum : MonoBehaviour
{
    public static MeepleNum Instance;  //インスタンスの定義
    public int MeepleR = 12;  //赤ミープルの初期数
    public int MeepleB = 12;  //青ミープルの初期数
    public string place = "";
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    public void DecreaseR()
    {
        MeepleR--;
    }
    public void DecreaseB()
    {
        MeepleB--;
    }
    public bool CheckMeepleR()
    {
        if (MeepleR <= 0){
            return false;
        }else{
            return true;
        }
    }
    public bool CheckMeepleB()
    {
        if (MeepleB <= 0){
            return false;
        }else{
            return true;
        }
    }
    public void setplace(string indplace)
    {
        place = indplace;
    }
    public void initplace()
    {
        place = "";
    }
}