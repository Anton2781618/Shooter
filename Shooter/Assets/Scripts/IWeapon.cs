using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon : Iholding
{
    void Fire();

    void Reload();
}
