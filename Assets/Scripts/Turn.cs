using UnityEngine;
public class Turn : MonoBehaviour
{
    public static Turn Instance; //インスタンスの定義
    public int turnNum = 0; //ターン数
    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
        }
    }
    public int TurnNum()
    {
        return turnNum;
    }
    public void TurnNumPlus()
    {
        turnNum++;
    }
}