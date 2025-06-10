using UnityEngine;
using UnityEngine.UI;

public class DrawnTileUI : MonoBehaviour
{
    public Image tileImage; // 表示用のUI Image

    public void ShowTile(Sprite tileSprite)
    {
        tileImage.sprite = tileSprite;
        tileImage.enabled = true; // 非表示のときは表示する
    }

    public void ClearTile()
    {
        tileImage.sprite = null;
        tileImage.enabled = false;
    }
}
