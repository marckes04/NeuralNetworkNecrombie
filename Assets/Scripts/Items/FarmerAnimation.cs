using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAnimation : MonoBehaviour
{
    private Animator anim;
    public static FarmerAnimation instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.Play("FarmerOk");

            StartCoroutine(DisableFarmer());
        }
    }


    IEnumerator DisableFarmer()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
       
    }

}
