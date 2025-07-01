/*************************************************
*** Designer : 御堂
*** Date : 2025.6.23
*** Purpose : Tile1のミープルの配置
*************************************************/

using UnityEngine;

public class Tile1 : MonoBehaviour
{
    public GameObject MeepleR;  //赤ミープル
    public GameObject MeepleB;  //青ミープル
    private GameObject Meeple;  //タイルに置くミープル
    private GameObject previewMeeple;  //仮置きミープル
    private Vector3 lastSnapMeeplePos = Vector3.positiveInfinity;
    private bool check = true;

/************************************************
*** Function Name : OnMouseDown
*** Designer : 御堂
*** Date : 2025.6.23
*** Function: Tile1におけるミープル配置
*** Return : void
************************************************/
    private void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;
        Vector3 localPos = transform.InverseTransformPoint(worldPos);

        float threshold = 0.6f;
        float Absthreshold = 0.3f;
        Vector3 MeeplePos = new Vector3(0f, 0f, 0f);

        //ターンで色を変える
        if (Turn.Instance.TurnNum()%2 == 0){
            Meeple = MeepleR;
        }else{
            Meeple = MeepleB;
        }

        string place = "";

        //ミープルの配置
        if (Mathf.Abs(localPos.x) < Absthreshold && localPos.y < -threshold){
            MeeplePos = new Vector3(0f, -0.9f, -0.1f);
            place = "road";
        }else if (Mathf.Abs(localPos.x) < Absthreshold && Mathf.Abs(localPos.y) < Absthreshold){
            MeeplePos = new Vector3(0f, 0f, -0.1f);
            place = "monastery";
        }else{
            return;
        }
        if (previewMeeple == null){
            previewMeeple = Instantiate(Meeple, transform);
        }else{
            if (MeeplePos == lastSnapMeeplePos){
                check = !check;
                previewMeeple.SetActive(check);
                if (check == false){
                    place = "";
                }
            }else{
                if (!check){
                    check = !check;
                    previewMeeple.SetActive(true);
                }
                lastSnapMeeplePos = MeeplePos;
            }
        }
        previewMeeple.transform.localPosition = MeeplePos;
        MeepleNum.Instance.setplace(place);
    }
}