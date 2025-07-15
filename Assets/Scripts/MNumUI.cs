/*************************************************
*** Designer : 御堂
*** Date : 2025.6.30
*** Purpose : Meepleの数を表示させる
*************************************************/

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MNumUI : MonoBehaviour
{
    public TMP_Text meepleText; //表示用のUIImage
    public Image MeepleRImage;
    public Image MeepleBImage;
    public Sprite MeepleRSprite;
    public Sprite MeepleBSprite;


/************************************************
*** Function Name : Update
*** Designer : 御堂
*** Date : 2025.6.30
*** Function: UIに表示する
*** Return : void
************************************************/
    void Update()
    {
        if (MeepleNum.Instance != null){
            if (MeepleNum.Instance.MeepleB >= 1 && MeepleNum.Instance.MeepleR >= 1){
                meepleText.text = $"                      {MeepleNum.Instance.MeepleB}                       {MeepleNum.Instance.MeepleR}";
            }
            if (MeepleNum.Instance.MeepleB < 1 && MeepleNum.Instance.MeepleR >= 1){
                meepleText.text = $"                      {"None"}                       {MeepleNum.Instance.MeepleR}";
            }
            if (MeepleNum.Instance.MeepleB >= 1 && MeepleNum.Instance.MeepleR < 1){
                meepleText.text = $"                      {MeepleNum.Instance.MeepleB}                       {"None"}";
            }
            if (MeepleNum.Instance.MeepleB < 1 && MeepleNum.Instance.MeepleR < 1){
                meepleText.text = $"                      {"None"}                       {"None"}";
            }
        }
        MeepleRImage.sprite = MeepleRSprite;
        MeepleBImage.sprite = MeepleBSprite;
    }

}
