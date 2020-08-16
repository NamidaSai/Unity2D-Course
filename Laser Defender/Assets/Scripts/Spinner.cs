using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] float spinSpeed = 180f;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f,0f,spinSpeed * Time.deltaTime);
    }
}
