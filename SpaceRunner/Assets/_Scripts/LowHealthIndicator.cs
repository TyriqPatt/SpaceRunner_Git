using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowHealthIndicator : MonoBehaviour
{

    public Color DefualtColor;
    public Material DefualtMat;
    public Material RedtMat;
    public bool IsHealth;
    AudioSource source;

    // Start is called before the first frame update
    void OnEnable()
    {
        source = GetComponent<AudioSource>();
        if (IsHealth)
        {
            DefualtColor = GetComponent<Image>().color;
            StartCoroutine(LowHealth());
        }
        else
        {
            DefualtMat = GetComponent<Renderer>().material;
            StartCoroutine(LowHealthPlanes());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LowHealth()
    {

        yield return new WaitForSeconds(1f);
        GetComponent<Image>().color = Color.red;
        source.Play();
        yield return new WaitForSeconds(.2f);
        GetComponent<Image>().color = DefualtColor;
        yield return new WaitForSeconds(.2f);
        GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(.2f);
        GetComponent<Image>().color = DefualtColor;
        source.Stop();
        StartCoroutine(LowHealth());
    }

    IEnumerator LowHealthPlanes()
    {

        yield return new WaitForSeconds(1f);
        GetComponent<Renderer>().material = RedtMat;
        yield return new WaitForSeconds(.2f);
        GetComponent<Renderer>().material = DefualtMat;
        yield return new WaitForSeconds(.2f);
        GetComponent<Renderer>().material = RedtMat;
        yield return new WaitForSeconds(.2f);
        GetComponent<Renderer>().material = DefualtMat;
        StartCoroutine(LowHealthPlanes());
    }

    void OnDisable()
    {
        if (IsHealth)
        {
            GetComponent<Image>().color = DefualtColor;
        }
        else
        {
            GetComponent<Renderer>().material = DefualtMat;
        }
        source.Stop();
        StopAllCoroutines();
    }
}
