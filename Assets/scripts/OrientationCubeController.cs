using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationCubeController : MonoBehaviour
{
    //Aktualizacja pozycji i rotacji
    public void UpdateOrientation(Transform rootBP, Transform target)
    {
        var dirVector = target.position - transform.position;
        dirVector.y = 0; //wyzerowanie kierunku na osi Y. Zadzia�a tylko na r�wnych, p�askich powierzchniach.
        var lookRot =
            dirVector == Vector3.zero
                ? Quaternion.identity
                : Quaternion.LookRotation(dirVector); //ustawia rotacj� w kierunku celu

        //Aktualizacji pozycji i rotacji sze�cianu orientacji
        transform.SetPositionAndRotation(rootBP.position, lookRot);
    }
}
