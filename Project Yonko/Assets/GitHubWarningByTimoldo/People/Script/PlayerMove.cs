using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Animator Playanimator;
    [SerializeField] float speed = 5f;

    void awake()
    {
        Playanimator = GetComponent<Animator>();
    }

    void Update()
    {
        /*
        float v = Input.GetAxis("Vertical");
        Playanimator.SetFloat("Walk", v);
        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);

        float h = Input.GetAxis("Horizontal");
        Playanimator.SetFloat("Right", h);
        transform.Translate(Vector3.right * h * speed * Time.deltaTime);
        */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Playanimator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Playanimator.SetTrigger("Sneak");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Playanimator.SetTrigger("Action");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Playanimator.SetTrigger("Sprint");
        }

    }
}