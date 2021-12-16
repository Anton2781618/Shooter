using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//сущность предмет
public interface Iholding
{
    //использовать
    void Use();

    //выбросить
    void Throw();
    
    //отдать
    void GiveAway();

    //ударить
    void Hit();

    //модифицировать
    void Modify();

    //осмотреть
    void Inspect();
}
