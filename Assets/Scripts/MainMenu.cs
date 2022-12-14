using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;
    //---------------------------------------------------------------------------------------------------------------------------------

    #region references

    [SerializeField]
    TextMeshProUGUI headingText;

    [SerializeField]
    GameObject contentSection, inventorySection, weaponSection, instrumentsSection, sphereGameobject;

    [SerializeField]
    Button backButton, weaponButton, instrumentButton;


    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

    #region logic

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            TurnOnInventory();
        });

        weaponButton.onClick.AddListener(() =>
        {
            TurnOnWeapons();
        });

        instrumentButton.onClick.AddListener(() =>
        {
            TurnOnInstruments();
        });

        headingText.text = "Inventory";
    }

    public void TurnOnInventory()
    {
        inventorySection.SetActive(true);
        weaponSection.SetActive(false);
        instrumentsSection.SetActive(false);
        backButton.gameObject.SetActive(false);
        headingText.text = "Inventory";
    }

    private void TurnOnWeapons()
    {
        inventorySection.SetActive(false);
        weaponSection.SetActive(true);
        backButton.gameObject.SetActive(true);
        headingText.text = "Weapons";
    }

    private void TurnOnInstruments()
    {
        inventorySection.SetActive(false);
        instrumentsSection.SetActive(true);
        backButton.gameObject.SetActive(true);
        headingText.text = "Instruments";

        sphereGameobject.SetActive(GameManager.instance.isSphereCollected);
        GameManager.instance.UserCheckedSphere();
    }

    public void TurnOnMainMenu()
    {
        contentSection.SetActive(true);
    }

    public void TurnOffMainMenu()
    {
        contentSection.SetActive(false);
    }

    public void UpdateSphereGameobject()
    {
        sphereGameobject.SetActive(GameManager.instance.isSphereCollected);
    }

    #endregion

    //---------------------------------------------------------------------------------------------------------------------------------

}
