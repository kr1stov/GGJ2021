using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoScroll : MonoBehaviour
{
    public GameObject infoText;
    public Image fadeToBlack;
    public float fadeDuration;
    public float textDuration;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(ShowAndEndGame());
    }

    private IEnumerator ShowAndEndGame()
    {
        infoText.SetActive(true);
        yield return new WaitForSeconds(textDuration);
        
        Color tempColor = fadeToBlack.color;
        tempColor.a = 0;
        float t = 0;

        while (tempColor.a < 1)
        {
            t += Time.deltaTime;
            tempColor.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeToBlack.color = tempColor;
            yield return null;
        }
        
        SceneManager.LoadScene("End");
    }
}
