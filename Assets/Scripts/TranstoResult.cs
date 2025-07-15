using UnityEngine;
using UnityEngine.SceneManagement; // SceneManagerを使用するために必要

/*************************************************
*** Designer : 樫原
*** Date : 2025.7.8
*** Purpose : リザルト画面に遷移するための関数
*************************************************/
public class TransetoResult : MonoBehaviour
{
    // このメソッドをボタンのOnClickイベントに設定します
    public void GoToResultScene()
    {
        // "GameScene" は遷移したいシーンの名前です。
        // ProjectSettings -> Build Settings でシーンの名前を確認し、必要に応じて変更してください。
        SceneManager.LoadScene("Result");
    }
}