using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLogic : MonoBehaviour
{
    [SerializeField] private float arrowSpeedX;
    [SerializeField] private float arrowSpeedY;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(arrowSpeedX, arrowSpeedY, rb.velocity.z);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("parryBarrier"))
        {
            Debug.Log("aaaa");
            other.GetComponentInParent<characterController>().EnableExtraJump();
            Destroy(gameObject);
        }
        else if(other.CompareTag("destroyArrows"))
        {
            Destroy(gameObject);
        }
    }
}
