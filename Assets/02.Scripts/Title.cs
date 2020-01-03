using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ui사용하기
using UnityEngine.UI;
//씬전환
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public Text start_text;
    public AsyncOperation _load;
    public GameObject fade;

    public void ChangeScene()
    {
        StartCoroutine("Fade");              
        // 버튼을 누르면 메인으로 넘어가도록 만듦
        //SceneManager.LoadScene(1);
        //비동기로딩?
        //_load = SceneManager.LoadSceneAsync(1);
        //StartCoroutine("Loading");
    }
    //검게 만들어서 넘어가도록 구현
    IEnumerator Fade()
    {
        fade.SetActive(true);
        while (fade.GetComponent<Image>().color.a < 1.0f)
        {
            fade.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, fade.GetComponent<Image>().color.a + Time.deltaTime * 1.5f);
            yield return null;
        }
        //yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(1);
    }
    //사용하지 않음
    //IEnumerator Loading()
    //{
    //    _load.allowSceneActivation = false;
    //    yield return new WaitForSeconds(0.3f);
    //    _load.allowSceneActivation = true;
    //}

    private void Start()
    {
        //깜박이게 만들기
        StartCoroutine("Blink_Off");
    }

    //알파값 상승 후 하강 부름
    IEnumerator Blink_On()
    {        
        while (start_text.color.a < 1.0f)
        {
            start_text.color = new Color(start_text.color.r, start_text.color.g, start_text.color.b, start_text.color.a + (Time.deltaTime));
            yield return null;
        }
        StartCoroutine("Blink_Off");
    }

    //알파값 하강 후 상승 부름
    IEnumerator Blink_Off()
    {
        while (start_text.color.a > 0.0f)
        {
            start_text.color = new Color(start_text.color.r, start_text.color.g, start_text.color.b, start_text.color.a - (Time.deltaTime));
            yield return null;
        }
        StartCoroutine("Blink_On");
    }


}
