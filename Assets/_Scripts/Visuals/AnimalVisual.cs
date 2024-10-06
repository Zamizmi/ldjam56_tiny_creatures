using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalVisual : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite eatSprite;
    [SerializeField] private Sprite sleepSprite;
    [SerializeField] private GameObject happyIcons;
    [SerializeField] private GameObject middleIcons;

    public void SetEatMode(bool isGood = true)
    {
        image.sprite = eatSprite;
        var icons = isGood ? happyIcons : middleIcons;
        SpawnIcons(icons);
    }

    public void SetIdleMode()
    {
        image.sprite = idleSprite;
    }
    public void SetSleepMode()
    {
        image.sprite = sleepSprite;
    }

    private void SpawnIcons(GameObject toSpawn)
    {
        Instantiate(toSpawn, transform.position, Quaternion.identity);
    }
}
