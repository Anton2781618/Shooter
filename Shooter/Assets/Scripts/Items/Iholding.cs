using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//сущность предмет
public interface Iholding
{
    //использовать
    void Use();

    //поднять
    void Take();

    //выбросить
    void Throw();

    void Sway();

}
