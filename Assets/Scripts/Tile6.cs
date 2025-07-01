using UnityEngine;

public class Tile6 : MonoBehaviour
{
    public GameObject MeepleR;
    public GameObject MeepleB;
    private GameObject Meeple;
    private GameObject previewMeeple;
    private Vector3 lastSnapMeeplePos = Vector3.positiveInfinity;
    private bool check = true;

    private void OnMouseDown()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0f;

        // ローカル座標に変換
        Vector3 localPos = transform.InverseTransformPoint(worldPos);


        float threshold = 0.6f;
        float Absthreshold = 0.3f;
        Vector3 MeeplePos = new Vector3(0f, 0f, 0f);

        if (Turn.Instance.TurnNum()%2 == 0){
            Meeple = MeepleR;
        }else{
            Meeple = MeepleB;
        }

        string place = "";

        if (Mathf.Abs(localPos.x) < Absthreshold && Mathf.Abs(localPos.y) < Absthreshold){
            MeeplePos = new Vector3(0f, 0f, -0.1f);
            place = "city";
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