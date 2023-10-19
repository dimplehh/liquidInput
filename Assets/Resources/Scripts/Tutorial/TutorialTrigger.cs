using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] string humanKey;
    [SerializeField] string slimeKey;
    [SerializeField] bool isWater;
    [SerializeField] bool isRope;
    GameObject tutorialBox;
    SpriteRenderer square;
    Text keyText, normalText;
    KeyCode realHumanKey, realSlimeKey;
    bool turnOnTutorial = false;

    private void Start()
    {
        tutorialBox = this.transform.GetChild(0).gameObject;
        square = tutorialBox.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        keyText = tutorialBox.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        normalText = tutorialBox.transform.GetChild(1).GetChild(1).GetComponent<Text>();
        if(humanKey != "")realHumanKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), humanKey);
        if(slimeKey != "")realSlimeKey = (KeyCode)Enum.Parse(typeof(KeyCode), slimeKey);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if ((GameManager.instance.player.GetComponent<Player>().isSlime == false && humanKey != "")
            || (GameManager.instance.player.GetComponent<Player>().isSlime == true && slimeKey != ""))
            {
                if (GameManager.instance.player.GetComponent<Player>().isSlime == true) keyText.text = slimeKey;
                else keyText.text = humanKey;
                StartCoroutine("FadeIn");
            }
        }
    }

    IEnumerator FadeIn()
    {
        turnOnTutorial = true;
        for (float a = 0; a <= 1.0f;)
        {
            a += 0.2f;
            keyText.color = new Color(0, 1, 1, a);
            normalText.color = new Color(1,1,1, a);
            if (a <= 0.8f) square.color = new Color(0, 0, 0, a);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.activeSelf == true && collision.gameObject.CompareTag("Player") && keyText.color.a > 0)
            StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        turnOnTutorial = false;
        for (float a = keyText.color.a; a >= 0f;)
        {
            a -= 0.2f;
            keyText.color = new Color(0,1, 1, a);
            normalText.color = new Color(1,1,1, a);
            square.color = new Color(0,0,0, a);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.player.GetComponent<Player>().isSlime == true) keyText.text = slimeKey;
            else if (GameManager.instance.player.GetComponent<Player>().attached == true) keyText.text = "¡ê";
            else keyText.text = humanKey;
            if(turnOnTutorial == true)
            {
                if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && keyText.text == "¡ê")
                        this.gameObject.SetActive(false);
                else if ((Input.GetKey(realHumanKey) && keyText.text == humanKey)
                    || (Input.GetKey(realSlimeKey) && keyText.text == slimeKey))
                    if ((isWater == false && isRope == false) || isWater == true && keyText.text == slimeKey)
                        this.gameObject.SetActive(false);
            }
        }
    }
}
