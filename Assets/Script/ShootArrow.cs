using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject arrow;
    public Transform attackPoint;

    public void Shoot()
    {
        Vector3 force = new Vector3(100, 0, 0);
        arrow.GetComponent<Rigidbody>().AddRelativeForce(force, ForceMode.Impulse);
        Instantiate(arrow, attackPoint.position, attackPoint.rotation);
    }
}
