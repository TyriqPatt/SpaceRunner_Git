using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JR_RemoveSign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
  
}
