using UnityEngine;

public class ManualController : MonoBehaviour
{
    public GameObject manualPanel;

    public void ToggleManual()
    {
        bool isActive = manualPanel.activeSelf;
        manualPanel.SetActive(!isActive);
    }
}
