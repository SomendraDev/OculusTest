using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtRotation : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------------------------------------------

    #region references

    [SerializeField]
    Transform target;
    Vector3 lookPos = new Vector3();

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

    #region logic

    // Update is called once per frame
    void Update()
    {
        lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
    }

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------
}
