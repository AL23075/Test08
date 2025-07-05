/*************************************************
*** Designer : 御堂
*** Date : 2025.6.8
*** Purpose : 山札から引いたタイルのUI表示
*************************************************/

using UnityEngine;
using UnityEngine.UI;

public class DrawnTileUI : MonoBehaviour
{
    public Image tileImage;  //表示用のUIImage

/************************************************
*** Function Name : ShowTile
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: UIに表示する
*** Return : void
************************************************/
    public void ShowTile(Sprite tileSprite)
    {
        tileImage.sprite = tileSprite;
        tileImage.enabled = true;
    }

/************************************************
*** Function Name : ClearTile
*** Designer : 御堂
*** Date : 2025.6.8
*** Function: 表示されていたタイルを非表示にする
*** Return : void
************************************************/
    public void ClearTile()
    {
        tileImage.sprite = null;
        tileImage.enabled = false;
    }
}
