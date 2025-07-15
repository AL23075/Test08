using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshProを使用する場合

/*************************************************
*** Designer : 樫原
*** Date : 2025.7.8
*** Purpose : ルール説明画面を表示する
*************************************************/
public class EBookViewer : MonoBehaviour
{
    public TextMeshProUGUI pageText; // ページに表示するテキスト
    public Image pageImage;         // ページに表示する画像

    public Button nextPageButton;
    public Button previousPageButton;

    // --- ここにコンテンツを収納します ---

    // 例1: スプライトの配列（画像コンテンツの場合）
    public Sprite[] bookPageImages; 

    // 例2: 文字列の配列（テキストコンテンツの場合）
    public string[] bookPageTexts;

    // 例3: カスタムクラスのリスト（画像とテキストを組み合わせる場合）
    [System.Serializable] // Inspectorで表示・編集可能にするため
    public class PageContent
    {
        public string text;
        public Sprite image;
        // 必要に応じて、さらに他の要素（動画パス、インタラクティブ要素の情報など）を追加
    }
    public PageContent[] bookContents;


    private int currentPageIndex = 0;

    void Start()
    {
        if (nextPageButton != null)
            nextPageButton.onClick.AddListener(NextPage);
        if (previousPageButton != null)
            previousPageButton.onClick.AddListener(PreviousPage);

        UpdatePageContent();
    }

    void NextPage()
    {
        // bookPageImages, bookPageTexts, bookContents のいずれかの長さを基準にする
        int totalPages = 0;
        if (bookPageImages != null) totalPages = bookPageImages.Length;
        else if (bookPageTexts != null) totalPages = bookPageTexts.Length;
        else if (bookContents != null) totalPages = bookContents.Length;

        if (currentPageIndex < totalPages - 1)
        {
            currentPageIndex++;
            UpdatePageContent();
        }
    }

    void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePageContent();
        }
    }

    void UpdatePageContent()
    {
        // ページコンテンツの表示ロジック
        if (bookPageImages != null && currentPageIndex < bookPageImages.Length)
        {
            pageImage.sprite = bookPageImages[currentPageIndex];
        }
        else
        {
            pageImage.sprite = null; // 画像がないページの場合
        }

        if (bookPageTexts != null && currentPageIndex < bookPageTexts.Length)
        {
            pageText.text = bookPageTexts[currentPageIndex];
        }
        else
        {
            pageText.text = ""; // テキストがないページの場合
        }

        // カスタムクラスを使用する場合の例
        if (bookContents != null && currentPageIndex < bookContents.Length)
        {
            PageContent currentPageData = bookContents[currentPageIndex];
            pageText.text = currentPageData.text;
            pageImage.sprite = currentPageData.image;
        }

        UpdateNavigationButtons();
    }

    void UpdateNavigationButtons()
    {
        int totalPages = 0;
        if (bookPageImages != null) totalPages = bookPageImages.Length;
        else if (bookPageTexts != null) totalPages = bookPageTexts.Length;
        else if (bookContents != null) totalPages = bookContents.Length;

        if (nextPageButton != null)
            nextPageButton.interactable = (currentPageIndex < totalPages - 1);
        if (previousPageButton != null)
            previousPageButton.interactable = (currentPageIndex > 0);
    }
}