using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAMERA_MANAGER : MonoBehaviour
{
    public Transform player;
    public Camera cam;

    public float profondeur;
    public float hauteur;

    private void Update()
    {
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + hauteur, player.transform.position.z + profondeur);
    }

    private void OnValidate()
    {
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + hauteur, player.transform.position.z + profondeur);
    }
}
