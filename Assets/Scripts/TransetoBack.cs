// Assets/Scripts/SceneManagement/RuleExplanationSceneManager.cs
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理のための名前空間

/*************************************************
*** Designer : 倉地
*** Date : 2025.7.8
*** Purpose : ルール説明画面からタイトル画面に戻るための関数
*************************************************/
public class RuleExplanationSceneManager : MonoBehaviour
{
    // Inspector から設定するボタンなどがあればここに public 変数を追加

    /// <summary>
    /// ゲームシーンをロードします。
    /// </summary>
    public void GoBackToGame()
    {
        // ゲームシーンの名前でロード
        SceneManager.LoadScene("Title");
        // もしくは、Build Settingsでのインデックスでロード
        // SceneManager.LoadScene(0); // 例: GameSceneが1番目 (インデックス0) の場合
    }

    // その他のルール説明シーンのロジック (RuleDisplayManagerはここにあるはず)
}