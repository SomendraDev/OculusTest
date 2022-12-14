using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropSphereUI : MonoBehaviour
{
    public static DropSphereUI instance;

    //---------------------------------------------------------------------------------------------------------------------------------

    #region references

    [SerializeField]
    GameObject content;

    [SerializeField]
    Button boxAButton, boxBButton, boxCButton;

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

    #region logic

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        boxAButton.onClick.AddListener(() =>
        {
            StartCoroutine(GameManager.instance.BoxSelected(1));
        });

        boxBButton.onClick.AddListener(() =>
        {
            StartCoroutine(GameManager.instance.BoxSelected(2));
        });

        boxCButton.onClick.AddListener(() =>
        {
            StartCoroutine(GameManager.instance.BoxSelected(3));
        });
    }

    public void TurnOnContent()
    {
        content.SetActive(true);
    }

    public void TurnOffContent()
    {
        content.SetActive(false);
    }

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

}
