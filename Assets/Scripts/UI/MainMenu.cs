using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator _crossfadePanelAnimator;
    
    private bool status = true;
    // Start is called before the first frame update

    private static float transitionTime = 1.0f;

    private static Animator crossfadePanelAnimator;

    public static MainMenu Instance;

    private void Start()
    {
        Instance = this;
    crossfadePanelAnimator = _crossfadePanelAnimator;        
    }

    public void Activate(GameObject currentPanel, GameObject nextPanel)
    {
        this.StartCoroutine(ActivateCoroutine(currentPanel, nextPanel));
    }

    private IEnumerator ActivateCoroutine(GameObject currentPanel, GameObject nextPanel)
    {
        crossfadePanelAnimator.SetTrigger("end");
        yield return new WaitForSeconds(transitionTime);
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        crossfadePanelAnimator.SetTrigger("ini");
        Debug.Log("Terminé");
    }
}
