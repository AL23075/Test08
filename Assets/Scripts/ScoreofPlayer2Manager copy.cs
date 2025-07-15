using UnityEngine;
using TMPro; // TextMeshProを使用する場合に必要

/*************************************************
*** Designer : 樫原
*** Date : 2025.7.8
*** Purpose : プレイヤー2の得点を管理するための関数
*************************************************/
public class ScoreofPlayer2Manager : MonoBehaviour
{
    // UI要素への参照
    public TMP_InputField scoreInputField; // プレイヤーが入力する得点フィールド
    public TMP_Text totalScoreText;       // 合計得点を表示するテキスト


    private int currentTotalScore = 0; // 現在の合計得点
    public static string score_of_player2 = "0";

    void Start()
    {
        // 初期表示を更新
        UpdateScoreDisplay();

        // InputFieldのOnEndEditイベントにメソッドを登録
        // これにより、InputFieldでの入力が確定したときにOnScoreInputEndEditが呼び出される
        if (scoreInputField != null)
        {
            scoreInputField.onEndEdit.AddListener(OnScoreInputEndEdit);
        }
    }

    // InputFieldの入力が確定したときに呼び出されるメソッド
    public void OnScoreInputEndEdit(string inputString)
    {
        // 入力された文字列が有効な数値であるかチェック
        if (int.TryParse(inputString, out int scoreToAdd))
        {
            // 有効な数値であれば、合計得点に加算
            AddScore(scoreToAdd);

            // InputFieldをクリアする（任意）
            scoreInputField.text = "";
        }
        else
        {
            Debug.LogWarning("無効な得点入力です: " + inputString);
            // 必要であれば、ユーザーにエラーメッセージを表示するなどの処理を追加
        }
    }

    // 得点を加算するメソッド
    public void AddScore(int score)
    {
        currentTotalScore += score;
        UpdateScoreDisplay(); // 合計得点表示を更新
    }

    // 合計得点表示を更新するメソッド
    private void UpdateScoreDisplay()
    {
        if (totalScoreText != null)
        {
            score_of_player2 = currentTotalScore.ToString();
            totalScoreText.text = currentTotalScore.ToString();
        }
    }

    // 必要に応じて、得点をリセットするメソッド
    public void ResetScore()
    {
        currentTotalScore = 0;
        UpdateScoreDisplay();
    }
}