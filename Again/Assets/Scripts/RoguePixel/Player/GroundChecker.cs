using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    CharController charController;

    void Start()
    {
        charController = GetComponentInParent<CharController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        charController.Grounded = true;
        Debug.Log(charController.Grounded);
        charController.GetComponent<Animator>().Play("Rogue_Idle");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        charController.Grounded = false;
    }
}
