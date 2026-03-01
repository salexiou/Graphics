using UnityEngine;
using UnityEngine.UI;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panelToDeactivate;
    public GameObject panelToActivate;
    public Button switchButton;

    void Start()
    {
        if (switchButton != null)
        {
            switchButton.onClick.AddListener(SwitchPanels);
        }
    }

    void SwitchPanels()
    {
        if (panelToDeactivate != null)
        {
            panelToDeactivate.SetActive(false);
        }

        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }
}
