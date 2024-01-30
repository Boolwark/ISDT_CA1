using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// XR teleportation anchor keeps clipping player below the ground surface. Attach script to player gameObject to
/// prevent this. 
/// </summary>
public class KeepPlayerAboveGround : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private LayerMask whatIsGround;
    public float offset = 0.3f;
    private Transform player;

    private void Start()
    {
        player = FindObjectOfType<Player.Player>().transform;
    }

    public void OnPlayerTeleport(SelectExitEventArgs t)
    {
        player.transform.position = new Vector3(player.transform.position.x, transform.position.y + offset,
            player.transform.position.z);
    }
}
// Update is called once per frame
    


