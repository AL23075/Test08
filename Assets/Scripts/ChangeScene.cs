using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

    private int Score = 0;

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            // Score = ;            // スコア受け継ぐ処理の記述
            Transform child = transform.GetChild(i);
            Debug.Log("Child name: " + child.name);         // デバッグ：子要素のコンポーネントの一覧表示

            Text textComponent = child.GetComponent<Text>();
            // テキストコンポーネントがあるとき
            if (textComponent != null) {
                if (child.name == "ResultText") {       // ResultTextコンポーネント
                    textComponent.text = "WIN!!!";
                }
                else if (child.name == "ScoreText") {       // ScoreTextコンポーネント：DisplayScore()は実質これ
                    textComponent.text = "Score: " + Score.ToString();
                }
            }
        }
    }

    // // スコアを表示する
    // private void DisplayScore() {
    //     // 今後スコアを表示する処理をここに追加可能
    // }

    // リザルト画面への遷移
    public void Trans2Result() {
        SceneManager.LoadScene("Result");
    }

    // タイトル画面への遷移
    public void Trans2TitlePlay() {
        SceneManager.LoadScene("hozon");
    }
}
