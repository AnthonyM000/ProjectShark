using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGun : MonoBehaviour
{
    public Camera playerCamera;
    public Transform laserOrigin;
    public float gunrange = 50f;
    public float firerate= 0.2f;
    public float laserDuration = 0.05f;

    LineRenderer laserLine;


    private void Awake()
    {
       laserLine = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            laserLine.SetPosition(0, laserOrigin.position);
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if(Physics.Raycast(rayOrigin, playerCamera.transform.position, out hit, gunrange)) { 
            laserLine.SetPosition(1, hit.point);
                Destroy(hit.transform.gameObject);
            
            
            
            
            }
            else
            {
                laserLine.SetPosition(1,rayOrigin + (playerCamera.transform.forward * gunrange));

            }

        }
        IEnumerator ShootLaser()
        {
            laserLine.enabled = true;
            yield return new WaitForSeconds(laserDuration);
            laserLine.enabled = false;
        }
    }












}
