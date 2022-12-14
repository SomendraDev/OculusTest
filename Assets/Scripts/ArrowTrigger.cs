using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrigger : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------------------------------------------

    #region references

    [SerializeField]
    GameObject arrowGameobject;

    [SerializeField]
    int instructionId;

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

    #region logic

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.ArrowTriggered(instructionId);
        arrowGameobject.SetActive(false);
        gameObject.SetActive(false);
    }

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------
}
