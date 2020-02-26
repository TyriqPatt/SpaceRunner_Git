using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public GameObject[] Asteriods;
    public List<GameObject> a = new List<GameObject>();
    public LayerMask layer;
    public bool IsPulling;
    float duration;
    // Start is called before the first frame update
    void OnEnable()
    {
        duration = 0;
        IsPulling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPulling)
        {
            duration += Time.deltaTime;
            if (duration >= 3)
            {
                GetComponent<BlackHole>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false); 
                transform.GetChild(1).gameObject.SetActive(true);
                
            }
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, 75, Vector3.forward, 10, layer);
            if (hits.Length > 0)
            {
                Asteriods = new GameObject[hits.Length];
                for (int i = 0; i < hits.Length; i++)
                {
                    Asteriods[i] = hits[i].collider.gameObject;
                }
                foreach (GameObject s in Asteriods)
                {
                    s.transform.position = Vector3.MoveTowards(s.transform.position, transform.position, .5f);
                }
            }
        }
    }

    private void OnDisable()
    {
        IsPulling = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 75);
    }
}
