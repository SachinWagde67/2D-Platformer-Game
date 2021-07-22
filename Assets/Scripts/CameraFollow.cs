using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Target;

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(Target.position.x, Target.position.y, transform.position.z);
    }
}
