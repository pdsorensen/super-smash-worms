﻿using UnityEngine;
using System.Collections;

public class GunBehaviour : MonoBehaviour {

    public Transform weaponEdge; 
    public LayerMask whatToHit;
    public GameObject bulletTrailBrefab; 
         
    private AudioSource m_AudioSource; 

	void Start () {
        m_AudioSource = GetComponentInChildren<AudioSource>();
    }

    public void Shoot()
    {
        Vector2 screenMousePos = Input.mousePosition;
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(screenMousePos).x, Camera.main.ScreenToWorldPoint(screenMousePos).y);
        Vector2 weaponEdgePos = new Vector2(weaponEdge.position.x, weaponEdge.position.y);

        RaycastHit2D hit = Physics2D.Raycast(weaponEdge.position, mousePosition - weaponEdgePos, 1000.0f, whatToHit);
        CreateBulletTrail(); 
        if (hit.collider != null)
        {
            //Debug.DrawLine(weaponEdgePos, hit.point, Color.red);
            //Debug.Log("We hit" + hit.collider.name);


            if (hit.collider.gameObject.tag == "Enemy")
            {
                EnemyHealth enemy = hit.collider.gameObject.GetComponent<EnemyHealth>();
                enemy.ApplyDamage(10);
            }
            else
            {
                m_AudioSource.Play();
            }
        }
    }

    void CreateBulletTrail()
    {
        Instantiate(bulletTrailBrefab, weaponEdge.transform.position, weaponEdge.rotation); 
    }
}