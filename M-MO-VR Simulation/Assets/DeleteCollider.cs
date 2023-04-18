using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "RightHand Controller") {
            StartCoroutine(Right());
        }

        if (gameObject.name == "LeftHand Controller")
        {
            StartCoroutine(Left());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Right()
    {
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("RightHand Controller/[RightHand Controller] Model Parent/Test Controller(Clone)").GetComponent<Collider>());
    }

    IEnumerator Left()
    {
        yield return new WaitForSeconds(2);
        Destroy(GameObject.Find("LeftHand Controller/[LeftHand Controller] Model Parent/Test Controller(Clone)").GetComponent<Collider>());
    }
}
