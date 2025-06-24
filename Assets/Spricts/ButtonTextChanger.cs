using UnityEngine;
using TMPro; // TextMeshProを使用する場合

public class ButtonTextChanger : MonoBehaviour
{
    public TextMeshProUGUI myButtonText; // Inspectorからドラッグ＆ドロップで設定

    void Start()
    {
        // 例: ゲーム開始時にテキストを変更する
        if (myButtonText != null)
        {
            myButtonText.text = "Play";
        }
    }

}