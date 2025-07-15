using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*************************************************
*** Designer : 樫原, 宇野
*** Date : 2025.7.8
*** Purpose : リザルト画面に結果を表示し，タイトル画面もしくはプレイ画面に遷移するための関数
*************************************************/
public class ChangeScene : MonoBehaviour {

    private int Score = 0;

    void Start() {
        for (int i = 0; i < transform.childCount; i++) {
            int player1_score = int.Parse(ScoreofPlayer1Manager.score_of_player1) ;
            int player2_score = int.Parse(ScoreofPlayer2Manager.score_of_player2); 
            Transform child = transform.GetChild(i);
            Debug.Log("Child name: " + child.name);         // デバッグ：子要素のコンポーネントの一覧表示

            Text textComponent = child.GetComponent<Text>();
            // テキストコンポーネントがあるとき
            if (textComponent != null) {
                if (child.name == "ResultText") {       // ResultTextコンポーネント
                    if(player1_score > player2_score){
                        textComponent.text = "Player1 WIN!!!";
                    }
                    else if(player1_score < player2_score){
                        textComponent.text = "Player2 WIN!!!";
                    }
                    else{
                        textComponent.text = "Draw";
                    }
                }
                else if (child.name == "ScoreText") {       // ScoreTextコンポーネント：DisplayScore()は実質これ
                    textComponent.text = "Score: " + Score.ToString();
                    if(player1_score > player2_score){
                        textComponent.text = "Score : " + player1_score;
                    }
                    else if(player1_score < player2_score){
                        textComponent.text = "Score : " + player2_score;
                    }
                    else{
                        textComponent.text = "Score : " + player1_score;
                    }
                }
            }
        }
    }

    // // スコアを表示する
    // private void DisplayScore() {
    //     // 今後スコアを表示する処理をここに追加可能
    // }

    // プレイ画面への遷移
    public void Trans2Result() {
        SceneManager.LoadScene("SampleScene");
    }

    // タイトル画面への遷移
    public void Trans2TitlePlay() {
        SceneManager.LoadScene("Title");
    }
}
