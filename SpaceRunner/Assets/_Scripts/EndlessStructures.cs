using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessStructures : MonoBehaviour
{
    public float speed;
    public float Lifetime;
    public Transform spawn;
    public GameObject[] Structures;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject newStructure;
        if(other.gameObject.tag == "Player")
        {
            newStructure = Instantiate(Structures[0], spawn.transform.position, transform.rotation);

        }
    }

    IEnumerator destroySelf(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
