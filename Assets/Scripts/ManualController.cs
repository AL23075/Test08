using UnityEngine;

/*************************************************
*** Designer : 武田和大
*** Date : 2025.7.4
*** Purpose : マニュアルを表示する
*************************************************/
public class ManualController : MonoBehaviour
{
    public GameObject manualPanel;

    public void ToggleManual()
    {        
        bool isActive = manualPanel.activeSelf;
        manualPanel.SetActive(!isActive);
    }
}
