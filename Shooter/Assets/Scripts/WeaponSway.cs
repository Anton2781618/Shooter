using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс смещает оружие во время движения камеры
public class WeaponSway : MonoBehaviour
{
    public float amount;
    public float maxAmount;
    public float smootAamount;

    private Vector3 offset;
    private Vector3 initPosition;

    private void Start() 
    {
        initPosition = transform.localPosition;
    }

    private void Update() 
    {
        MoveWeapon();
    }

    private void MoveWeapon()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;

        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition +
                                             initPosition, Time.deltaTime * smootAamount);

    }
}
