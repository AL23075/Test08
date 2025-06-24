using UnityEngine;
using UnityEngine.SceneManagement; // SceneManagerを使用するために必要

public class TransetoPlay : MonoBehaviour
{
    // このメソッドをボタンのOnClickイベントに設定します
    public void GoToGameScene()
    {
        // "GameScene" は遷移したいシーンの名前です。
        // ProjectSettings -> Build Settings でシーンの名前を確認し、必要に応じて変更してください。
        SceneManager.LoadScene("Play");
    }
}