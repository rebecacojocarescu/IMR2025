using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusBehavior : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;

    public float minimumDistance = 0.25f;

    void Update()
    {
        float distance = Vector3.Distance(target1.transform.position, target2.transform.position);

        if (distance <= minimumDistance)
        {
            target1.GetComponent<Animator>().SetBool("isInRange", true);
            target2.GetComponent<Animator>().SetBool("isInRange", true);
            Debug.Log("Within attack range");
        }
        else
        {
            target1.GetComponent<Animator>().SetBool("isInRange", false);
            target2.GetComponent<Animator>().SetBool("isInRange", false);
            Debug.Log("Not within attack range");
        }
    }
}