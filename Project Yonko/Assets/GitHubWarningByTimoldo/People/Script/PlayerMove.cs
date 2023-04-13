using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator Playanimator;
    [SerializeField] float speed = 1f;

    void awake()
    {
        Playanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float v = Input.GetAxis("Vertical");
        Playanimator.SetFloat("Walk", v);
        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        Playanimator.SetFloat("Right", h);
        transform.Translate(Vector3.right * h * speed * Time.deltaTime);

        
    }
}
