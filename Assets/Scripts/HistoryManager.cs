﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryManager : MonoBehaviour
{
    public GameObject historySet;
    public GameObject itemFrame;
    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        historySet.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHistory(List<Item> target)
    {
        SetHistoryDestroy();

        target.Reverse();

        for (int i = 0; i < target.Count;)
        {
            Item item = ItemDatabase.instance.findItemByName(target[i].koName);

            if (item == null)
            {
                target.RemoveAt(i);
                continue;
            }

            if (target[i] == null)
            {
                break;
            }

            GameObject go = Instantiate(itemFrame);

            go.transform.SetParent(content.transform);
            go.GetComponent<ItemFrame>().SetItemWithBaseSetting(ItemDatabase.instance.makeItem(item), ++i);
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            CanvasResolutionManager.instance.SetResolution(go.GetComponent<RectTransform>());

            // 타이틀 제목
/*            GameObject title = go.transform.GetChild(0).gameObject;
            title.GetComponent<Text>().text = ItemDatabase.instance.questDB[GameManager.instance.playerData.startQuest[i]].questTitle;*/
        }

        target.Reverse();
    }

    private void SetHistoryDestroy()
    {
        if (content.transform.childCount == 0)
        {
            return;
        }

        for (int i = content.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(content.transform.GetChild(i).gameObject);
        }
    }

    public void ButtonHistory()
    {
        SoundManager.instance.PlayOneShotEffectSound(1);
        historySet.SetActive(true);

        int index = BannerManager.instance.onBannerIndex;

        switch (index)
        {
            case 0:
                SetHistory(GameManager.instance.playerData.noelleHistory);
                break;
            case 1:
                SetHistory(GameManager.instance.playerData.characterHistory);
                break;
            case 2:
                SetHistory(GameManager.instance.playerData.weaponHistory);
                break;
            case 3:
                SetHistory(GameManager.instance.playerData.normalHistory);
                break;
        }
    }

    public void OffHistory()
    {
        SoundManager.instance.PlayOneShotEffectSound(3);
        historySet.SetActive(false);
    }
}
