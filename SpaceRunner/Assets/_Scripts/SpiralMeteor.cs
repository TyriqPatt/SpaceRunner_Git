using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMeteor : MonoBehaviour
{
    public GameObject Parent;
    public GameObject sparks;
    public float speed;
    float rot;
    float rot2;
    float rot3;

    // Start is called before the first frame update
    void Start()
    {
        rot = Random.Range(1, 5);
        rot2 = Random.Range(1, 5);
        rot3 = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rot, rot2, rot3);
        sparks.transform.RotateAround(Parent.transform.position, Vector3.forward, 100 * Time.deltaTime);

        transform.RotateAround(Parent.transform.position, Vector3.forward, 100 * Time.deltaTime);

        Parent.transform.Translate(0, 0, -speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "KillBox")
        {
            Destroy(gameObject);
        }
        
    }

    public void Shrink()
    {
        //Child.transform.localScale = new Vector3(Child.transform.localScale.x - Damage, Child.transform.localScale.y - Damage, Child.transform.localScale.z - Damage);
    }

    void OnDestroy()
    {
        Destroy(Parent);
    }
}
