// Assets/Scripts/SceneManagement/GameSceneManager.cs
using UnityEngine;
using UnityEngine.SceneManagement; // シーン管理のための名前空間

/*************************************************
*** Designer : 石山
*** Date : 2025.7.8
*** Purpose : ルール説明画面へ遷移するための関数
*************************************************/
public class TransetoRule : MonoBehaviour
{
    // Inspector から設定するボタンなどがあればここに public 変数を追加

    /// <summary>
    /// ルール説明シーンをロードします。
    /// </summary>
    public void GoToRuleExplanation()
    {
        // シーンの名前でロード
        SceneManager.LoadScene("Rule");
        // もしくは、Build Settingsでのインデックスでロード
        // SceneManager.LoadScene(1); // 例: RuleExplanationSceneが2番目 (インデックス1) の場合
    }

    // その他のゲームシーンのロジック...
}