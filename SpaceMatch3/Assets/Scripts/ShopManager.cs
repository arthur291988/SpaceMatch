using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [SerializeField]
    private RectTransform shopPanelRectTransform;
    [SerializeField]
    private Button buyNoAdsButton;
    [SerializeField]
    private Button shopOpenButton;
    private bool shoPanelIsRollingIn;
    private bool shoPanelIsRollingOut;
    private bool shoPanelIsOpen;
    private Vector2 shopPanelCloseMoveToPos;
    private Vector2 shopPanelOpenMoveToPos;
    private Vector2 shopPanelClosePos;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        shopPanelCloseMoveToPos = new Vector2(1600, 0);
        shopPanelClosePos = new Vector2(1500, 0);
        shopPanelOpenMoveToPos= new Vector2(-100, 0);
        shopPanelRectTransform = GetComponent<RectTransform>();
    }


    public void openCloseShopPanel() {
        if (shoPanelIsOpen)
        {
            shoPanelIsRollingOut = true;
            shopOpenButton.interactable = false;
            buyNoAdsButton.interactable = false;
        }
        else
        {
            shoPanelIsRollingIn = true;
            shopOpenButton.interactable = false;
        }

        AudioManager.Instance.connectionVoice();
    }


    private void Update()
    {
        if (shoPanelIsRollingOut) {
            shopPanelRectTransform.anchoredPosition = Vector2.Lerp(shopPanelRectTransform.anchoredPosition, shopPanelCloseMoveToPos, 0.25f);
            if (Mathf.Abs(shopPanelRectTransform.anchoredPosition.x) > 1400f) {
                shoPanelIsRollingOut = false;
                shopPanelRectTransform.anchoredPosition = shopPanelClosePos;
                buyNoAdsButton.interactable = true;
                shoPanelIsOpen = false;
                shopOpenButton.interactable = true;
            }
        }
        if (shoPanelIsRollingIn)
        {
            shopPanelRectTransform.anchoredPosition = Vector2.Lerp(shopPanelRectTransform.anchoredPosition, shopPanelOpenMoveToPos, 0.25f);
            if (Mathf.Abs(shopPanelRectTransform.anchoredPosition.x) < 20f)
            {
                shoPanelIsRollingIn = false;
                shopPanelRectTransform.anchoredPosition = Vector2.zero;
                shoPanelIsOpen = true;
                shopOpenButton.interactable = true;
            }
        }
    }
}
