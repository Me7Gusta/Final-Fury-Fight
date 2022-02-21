using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int healPower = 10;
    private float destroyTime = 0.3f;
    private float timer = 0;

    private bool pickedUp = false;

    private void Update()
    {
        if (pickedUp)
        {
            healPower = 0;
            timer += Time.deltaTime;
            if (timer >= destroyTime)
            {
                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    public bool GetPickedUp()
    {
        return pickedUp;
    }

    public int PickUp()
    {
        pickedUp = true;
        return healPower;
    }
}
