using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction_Pet : MonoBehaviour
{
    Slider r_slider;

    private void Start()
    {
        r_slider = SceneMgr.realationship_bar.GetComponent<Slider>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("닿음.");
        //닿으면 쉬게하기 - 애니메이션 변경
        if(other.tag == "Pet")
        {            
            Pet_on_Plane.spawnedObject.GetComponent<Animator>().SetInteger("animation", 5);
        }        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Pet")
        {
            //만지고 있으면 친밀도가 상승
            if (r_slider.value < 100.0f)
            {
                r_slider.value += Time.deltaTime * 4;
                PlayerPrefs.SetFloat("Realation", r_slider.value);
            }
        }
    }
}
