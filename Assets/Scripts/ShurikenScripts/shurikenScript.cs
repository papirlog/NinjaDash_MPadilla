using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shurikenScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeedShuriken;

    static public int shurikensCollectedInThisTry;

    private void Awake()
    {
        shurikensCollectedInThisTry = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeedShuriken * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shurikensCollectedInThisTry++;
            GameObject.Destroy(gameObject);
        }
    }
}
