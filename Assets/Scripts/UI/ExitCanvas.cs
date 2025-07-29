using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCanvas : MonoBehaviour
{
    public void OnButtonPushed() 
    { 
       gameObject.SetActive(false);
    }    
}
