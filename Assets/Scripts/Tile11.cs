/*************************************************
*** Designer : 御堂
*** Date : 2025.6.23
*** Purpose : Tile11におけるミープルの配置
*************************************************/
using UnityEngine;

public class Tile11 : MonoBehaviour
{
    public GameObject MeepleR;
    public GameObject MeepleB;
    private GameObject Meeple;
    private GameObject previewMeeple;
    private Vector3 lastSnapMeeplePos = Vector3.positiveInfinity;
    private bool check = true;

    private void OnMouseDown()
    {
        if (MeepleNum.Instance.n == 1){
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0f;
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

        if (Mathf.Abs(localPos.x) < Absthreshold && localPos.y > threshold){
                MeeplePos = new Vector3(0f, 0.9f, -0.1f);
                place = "city";
            }else if (Mathf.Abs(localPos.x) < Absthreshold && localPos.y < -threshold){
                MeeplePos = new Vector3(0f, -0.9f, -0.1f);
                place = "road";
            }else{
                return;
            }
            if (previewMeeple == null){
                previewMeeple = Instantiate(Meeple, transform);
                if (Turn.Instance.TurnNum()%2 == 0){
                    MeepleNum.Instance.DecreaseB();
                }else{
                    MeepleNum.Instance.DecreaseR();
                }
            }else{
                if (MeeplePos == lastSnapMeeplePos){
                    check = !check;
                    previewMeeple.SetActive(check);
                    if (check == false){
                        place = "";
                        if (Turn.Instance.TurnNum()%2 == 0){
                            MeepleNum.Instance.IncreaseB();
                            if (MeepleNum.Instance.color == "B"){
                                MeepleNum.Instance.DecreaseB();
                                MeepleNum.Instance.IncreaseR();
                                MeepleNum.Instance.color = "";
                            }
                        }else{
                            MeepleNum.Instance.IncreaseR();
                            if (MeepleNum.Instance.color == "R"){
                                MeepleNum.Instance.DecreaseR();
                                MeepleNum.Instance.IncreaseB();
                                MeepleNum.Instance.color = "";
                            }
                        }
                    }else{
                        if (Turn.Instance.TurnNum()%2 == 0){
                            MeepleNum.Instance.DecreaseB();
                        }else{
                            MeepleNum.Instance.DecreaseR();
                        }
                    }
                }else{
                    if (!check){
                        check = !check;
                        previewMeeple.SetActive(true);
                        if (Turn.Instance.TurnNum()%2 == 0){
                            MeepleNum.Instance.DecreaseB();
                        }else{
                            MeepleNum.Instance.DecreaseR();
                        }
                    }
                    lastSnapMeeplePos = MeeplePos;
                }
            }
            previewMeeple.transform.localPosition = MeeplePos;
            MeepleNum.Instance.setplace(place);
        }
    }
}