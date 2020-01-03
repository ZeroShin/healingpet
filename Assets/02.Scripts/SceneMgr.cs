using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour
{
    //프로그래스 바 선언.
    public GameObject hungry;
    public GameObject health;
    public GameObject realationship;

    //프로그래스 바 다른곳에 쓰기 위해 스태틱.
    public static GameObject hungry_bar = null;
    public static GameObject health_bar = null;
    public static GameObject realationship_bar = null;

    //씬 페이드
    public GameObject fade;

    //샤워 컨트롤
    public GameObject _shower;
    private bool shower_control;

    //먹이 컨트롤
    public GameObject _feed;
    private bool feed_control;

    //캔버스, 스크린샷
    public GameObject canvas;
    public ScreenShot shot;

    // AR 카메라
    private Camera _arCamera;

    //자이로 확인하기
    //public Text tx;
    private void Awake()
    {
        //자이로센서 사용하기.
        Input.gyro.enabled = true;
    }

    private void Start()
    {
        StartCoroutine("Fade");
        _arCamera = Camera.main.GetComponent<Camera>();
        hungry_bar = hungry;
        health_bar = health;
        realationship_bar = realationship;
        shower_control = false;
        feed_control = false;
    }

    private void Update()
    {
        //상시 떨어지는 수치
        if (hungry_bar.GetComponent<Slider>().value > 0)
        {
            hungry_bar.GetComponent<Slider>().value -= Time.deltaTime / 2;
        }
        if (health_bar.GetComponent<Slider>().value > 0)
        {
            health_bar.GetComponent<Slider>().value -= Time.deltaTime / 2;
        }
        if (realationship_bar.GetComponent<Slider>().value > 0)
        {
            realationship_bar.GetComponent<Slider>().value -= Time.deltaTime / 2;
        }
        //tx.text = Input.gyro.rotationRate.ToString();
        //핸드폰을 앞으로 내린 경우(샤워기 틈)
        //절대값 사용으로 1 이상 벌어지면 하게 만듦
        if (Mathf.Abs(Input.gyro.rotationRate.x) > 1.5f)
        {
            if (shower_control == false)
            {
                _shower.SetActive(true);
                shower_control = true;
            }
            else if (shower_control == true)
            {
                //샤워 아니면 제자리.
                Pet_on_Plane.spawnedObject.GetComponent<Animator>().SetInteger("animation", 0);
                _shower.SetActive(false);
                shower_control = false;
            }
        }
        //핸드폰 좌우로 흔든 경우(먹이주기)
        //절대값 사용으로 1 이상 벌어지면 하게 만듦
        if (Mathf.Abs(Input.gyro.rotationRate.z) > 1.5f)
        {
            //먹이 놓기, 먹이 먹기.
            if (feed_control == false)
            {
                Pet_on_Plane.spawnedObject.GetComponent<Animator>().SetInteger("animation", 4);
                feed_control = true;                
            }
            else if (feed_control == true)
            {
                Pet_on_Plane.spawnedObject.GetComponent<Animator>().SetInteger("animation", 0);
                feed_control = false;
            }
        }
        //먹는 애니메이션 중이라면
        if(Pet_on_Plane.spawnedObject.GetComponent<Animator>().GetInteger("animation") == 4)
        {
            //먹고 있으면 포만상승
            if (hungry_bar.GetComponent<Slider>().value < 100.0f)
            {
                hungry_bar.GetComponent<Slider>().value += Time.deltaTime * 4;
                PlayerPrefs.SetFloat("Realation", hungry_bar.GetComponent<Slider>().value);
            }
        }
    }

    //검은화면에서 점점 밝아지게
    IEnumerator Fade()
    {
        while (fade.GetComponent<Image>().color.a > 0.0f)
        {
            fade.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, fade.GetComponent<Image>().color.a - Time.deltaTime * 1.5f);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        fade.SetActive(false);
    }
    //버튼용 샤워모드 보존.
    //public void Shower_Mode()
    //{
    //    if (shower_control == false)
    //    {
    //        _shower.SetActive(true);
    //        shower_control = true;
    //    }
    //    else if (shower_control == true)
    //    {
    //        _shower.SetActive(false);
    //        shower_control = false;
    //    }
    //}

    // 스크린 샷 버튼을 클릭
    public void OnScreenShotButtonClick()
    {
        // 카메라에서 AR 클라우드, 플래인 효과 레이어를 마스킹 함
        // 캐릭터를 제외한 다른 UI들은 없애고 찍으려고 하는 것이다. 레이어가 AR인것을 빼고 찍으려고 하는것.
        //^=는 XOR연산
        _arCamera.cullingMask ^= 1 << LayerMask.NameToLayer("AR");

        // 캔버스 비활 성화
        canvas.SetActive(false);

        // 스크린 샷 수행
        shot.SaveScreenshot();

        // 스크린 캡쳐 완료
        Invoke("OnScreenCaptureCompleted", 0.3f);
    }

    // 스크린 캡쳐 완료 이벤트
    public void OnScreenCaptureCompleted()
    {
        canvas.SetActive(true);

        // 카메라에서 AR 클라우드, 플래인 효과 레이어를 마스킹 함
        //정확하게 이해하고 갈 수 있도록 하자.
        _arCamera.cullingMask ^= 1 << LayerMask.NameToLayer("AR");
    }

}
