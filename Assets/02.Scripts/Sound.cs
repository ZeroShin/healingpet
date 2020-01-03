using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    void Start()
    {
        //씬변환이 되어도 살아있도록(계속해서 소리가 나도록)
        DontDestroyOnLoad(transform.gameObject);        
    }

    private void Update()
    {
        // 끄는것을 확인한다.
        if (Input.GetKey(KeyCode.Escape))
        {
            //현재 스테이터스를 저장하고 종료한다.
            PlayerPrefs.SetFloat("Hungry", SceneMgr.hungry_bar.GetComponent<Slider>().value);
            PlayerPrefs.SetFloat("Health", SceneMgr.health_bar.GetComponent<Slider>().value);
            PlayerPrefs.SetFloat("Realation", SceneMgr.realationship_bar.GetComponent<Slider>().value);
            Application.Quit();
        }
    }

}
