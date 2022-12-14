using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //---------------------------------------------------------------------------------------------------------------------------------

    #region references

    [SerializeField]
    Transform playerSphereCollectionPosition, playerOvrTransform, sphereInitialPosition;

    [SerializeField]
    GameObject ovrContollerGameobject, ovrControllerHandGamobject;


    [SerializeField]
    List<Transform> chestsTopPartTransform;

    [SerializeField]
    List<Transform> sphereBallDropPositions;

    [SerializeField]
    List<GameObject> arrowGameobjects, triggerGameobjects;

    [SerializeField]
    GameObject chestsParentGameobject;

    [SerializeField]
    GameObject sphereToCollectGameobject, sphereToCollectCanvasGameobject, spherePrefab;

    [SerializeField]
    float chestAngleToRotate, chestRotateTime;

    public bool isSphereCollected = false;

    [Header("Effects")]
    [SerializeField]
    BoxEffect boxAEffect;

    [SerializeField]
    BoxEffect boxBEffect;


    [Header("Sounds")]
    [SerializeField]
    AudioSource boxAudioSource;
    
    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

    #region logic

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ovrContollerGameobject.SetActive(false);
        ovrControllerHandGamobject.SetActive(true);
        chestsParentGameobject.SetActive(false);
        TurnOffAllArrowsAndTriggers();
        TurnOnArrowAndTrigger(0);
        //StartCoroutine(ShowChests());

    }

    public void ArrowTriggered(int id)
    {
        switch (id)
        {
            case 1:
                sphereToCollectGameobject.gameObject.SetActive(true);
                break;
            case 3:
                StartCoroutine(ShowChests());
                break;
            default:
                break;
        }
    }

    public void SphereCollected()
    {
        ovrContollerGameobject.SetActive(true);
        ovrControllerHandGamobject.SetActive(false);
        isSphereCollected = true;
        sphereToCollectCanvasGameobject.SetActive(false);
        StartCoroutine(UnactiveSphereAfterSomeTime());
    }

    IEnumerator UnactiveSphereAfterSomeTime()
    {
        yield return new WaitForSeconds(0.2f);
        sphereToCollectGameobject.SetActive(false);
        sphereToCollectGameobject.transform.SetPositionAndRotation(sphereInitialPosition.position, sphereInitialPosition.rotation);
        sphereToCollectCanvasGameobject.SetActive(true);
        MainMenu.instance.TurnOnMainMenu();
    }


    public void UserCheckedSphere()
    {
        TurnOnArrowAndTrigger(1);
    }

    private IEnumerator ShowChests()
    {
        Vector3 rotateVec3 = new Vector3(0, 0, chestAngleToRotate);
        chestsParentGameobject.SetActive(true);
        foreach (var item in chestsTopPartTransform)
        {
            item.DOLocalRotate(Vector3.zero, 0);
        }

        foreach (var item in chestsTopPartTransform)
        {
            item.DOLocalRotate(rotateVec3, chestRotateTime);
        }

        yield return new WaitForSeconds(chestRotateTime);

        DropSphereUI.instance.TurnOnContent();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Box A => 1, Box B => 2, Box C => 3</param>
    public IEnumerator BoxSelected(int id)
    {
        GameObject instantiatedSphere = Instantiate(spherePrefab);
        instantiatedSphere.transform.SetPositionAndRotation(sphereBallDropPositions[id - 1].position, sphereBallDropPositions[id - 1].rotation);
        yield return new WaitForSeconds(0.5f);
        Destroy(instantiatedSphere);

        boxAudioSource.Play();
        switch (id)
        {
            case 1:
                boxAEffect.canvasGameobject.transform.DOMove(boxAEffect.canvasInitialPosition.position, 0);
                boxAEffect.canvasGameobject.SetActive(true);
                boxAEffect.canvasGameobject.transform.DOMove(boxAEffect.canvasEndPosition.position, 1);
                boxAEffect.particleSystem.gameObject.SetActive(true);
                boxAEffect.particleSystem.Play();
                break;

            case 2:
                boxBEffect.canvasGameobject.transform.DOMove(boxBEffect.canvasInitialPosition.position, 0);
                boxBEffect.canvasGameobject.SetActive(true);
                boxBEffect.canvasGameobject.transform.DOMove(boxBEffect.canvasEndPosition.position, 1);
                boxBEffect.particleSystem.gameObject.SetActive(true);
                boxBEffect.particleSystem.Play();
                break;

            case 3:
                isSphereCollected = false;
                sphereToCollectGameobject.SetActive(true);
                break;
            default:
                break;
        }

        yield return new WaitForSeconds(2);
        boxAEffect.canvasGameobject.SetActive(false);
        boxBEffect.canvasGameobject.SetActive(false);
        boxAEffect.particleSystem.gameObject.SetActive(false);
        boxBEffect.particleSystem.gameObject.SetActive(false);

        chestsParentGameobject.SetActive(false);
        DropSphereUI.instance.TurnOffContent();
        MainMenu.instance.TurnOnInventory();
        MainMenu.instance.TurnOffMainMenu();
        MainMenu.instance.UpdateSphereGameobject();

        if (id == 3)
        {
            playerOvrTransform.gameObject.SetActive(false);
            playerOvrTransform.SetPositionAndRotation(playerSphereCollectionPosition.position, playerSphereCollectionPosition.rotation);
            playerOvrTransform.gameObject.SetActive(true);
            ovrContollerGameobject.SetActive(false);
            ovrControllerHandGamobject.SetActive(true);
        }
    }

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

    #region helper functions

    private void TurnOffAllArrowsAndTriggers()
    {
        foreach (var item in arrowGameobjects)
        {
            item.SetActive(false);
        }

        foreach (var item in triggerGameobjects)
        {
            item.SetActive(false);
        }
    }

    private void TurnOnArrowAndTrigger(int index)
    {
        arrowGameobjects[index].SetActive(true);
        triggerGameobjects[index].SetActive(true);
    }

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------
}


[Serializable]
public class BoxEffect
{
    public GameObject canvasGameobject;
    public Transform canvasInitialPosition, canvasEndPosition;
    public ParticleSystem particleSystem;
}