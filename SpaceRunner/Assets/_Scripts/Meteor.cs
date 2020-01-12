using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{

    public GameObject Child;
    public float speed;
    float rot;
    float rot2;
    float rot3;
    public Vector3 size;
    public float finalSize;

    // Start is called before the first frame update
    void Start()
    {
        rot = Random.Range(1, 5);
        rot2 = Random.Range(1, 5);
        rot3 = Random.Range(1, 5);

        transform.localScale = new Vector3(0, 0, 0);
        finalSize = Random.Range(1, 11);
        if (finalSize > 4 && finalSize < 8)
        {
            finalSize = 1;
        }
        if (finalSize > 7 && finalSize < 11)
        {
            finalSize = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Child.transform.Rotate(rot, rot2, rot3);

        transform.Translate(0, 0, -speed);

        if (size.x <= finalSize && size.y <= finalSize && size.z <= finalSize)
        {
            size.x += Time.deltaTime * .5f;
            size.y += Time.deltaTime * .5f;
            size.z += Time.deltaTime * .5f;
            transform.localScale = size;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "KillBox")
        {
            Destroy(gameObject);
        }
        
    }

    public void Shrink()
    {
        //Child.transform.localScale = new Vector3(Child.transform.localScale.x - Damage, Child.transform.localScale.y - Damage, Child.transform.localScale.z - Damage);
    }
}
