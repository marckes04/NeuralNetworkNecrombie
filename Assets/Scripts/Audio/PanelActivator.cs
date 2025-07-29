using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PanelActivator : MonoBehaviour
{
    #region serializedfields

    [SerializeField] private GameObject currentPanel;
    [SerializeField] private GameObject nextPanel;
    #endregion

    #region privatefields
    private Button _button;
    #endregion

    #region privatemethods
    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(() => MainMenu.Instance.Activate(currentPanel,nextPanel));
    }

    #endregion
}
