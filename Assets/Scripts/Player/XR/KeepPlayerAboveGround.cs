using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// XR teleportation anchor keeps clipping player below the ground surface. Attach script to player gameObject to
/// prevent this. 
/// </summary>
public class KeepPlayerAboveGround : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask whatIsGround;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Physics.Raycast(transform.position, -transform.up, 5f, whatIsGround))
        {
            transform.position = new(transform.position.x,transform.position.y + 1f,transform.position.z);
        }
    }
}
