using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AR Foundation사용위한 코드
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
//Ui사용하기
using UnityEngine.UI;

public class Pet_on_Plane : MonoBehaviour
{
    //터치여부
    public static bool isTouched = false;

    //설치타입
    public static int type = 0;

    //레이캐스트 충돌 목록 - 터치가 여러개가 되면 리스트로 들어오게 된다.(정보전달이 리스트로 옮겨짐)
    public static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    //설치할 프리펩. 배열상에서 다른 모델을 가져 올 수 있도록 하기 위해서
    public GameObject[] _placePrefabs;

    // AR 레이캐스트 매니저 참조
    private ARRaycastManager _arRaycastMgr;

    // 생성된 오브젝트 참조, 로테이션에서 스태틱을 가져와서 사용함.
    public static GameObject spawnedObject = null;

    //선택패널 활성화 시킴
    public GameObject _canvas;
    public GameObject _resetBtn;
    public GameObject _shotBtn;
    public GameObject _Status_UI;
    private bool canvas_check;

    //애니메이터 컨트롤
    private Animator anim;
    private int anim_Control;

    void Awake()
    {
        _arRaycastMgr = GetComponent<ARRaycastManager>();
        canvas_check = true;
    }

    private void Start()
    {

    }
    //이미 생성한 오브젝트를 파괴
    public void DestroySpawnObject()
    {
        Destroy(spawnedObject);
        spawnedObject = null;
    }
    // Update is called once per frame
    void Update()
    {
        // 터치가 입력을 확인한다.
        if (Input.touchCount == 0)
        {
            return;
        }

        if (Pet_on_Plane.isTouched)
        {
            return;
        }

        // 첫번째 터치 이벽값을 추출함
        Touch touch = Input.GetTouch(0);

        //선택 캔버스가 있다면 터치를 막는다.
        if(canvas_check == true)
        {
            return;
        }

        // 트래킹된 Plane 폴리곤 영역 이내 터치 레이의 충돌이 일어났다면
        if (_arRaycastMgr.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            // 가장 가까운 첫번째 터치점의 Transform을 참조함
            Pose hitPose = hits[0].pose;

            // 이미 로드된 오브젝트가 없다면
            if (spawnedObject == null)
            {
                // 선택된 타입의 오브젝트를 생성함
                spawnedObject = Instantiate(_placePrefabs[type], hitPose.position, Quaternion.Euler(0, 180.0f, 0));
                //애니메이터 가져옴
                anim = spawnedObject.GetComponent<Animator>();
                //캐릭터 스테이터스 보여주기
                _Status_UI.SetActive(true);
            }
            else
            {
                //있으면 해당 오브젝트를 이동한다.
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }

    public void Select_Animal(int i)
    {
        // 동물 변경
        type = i;
        _canvas.SetActive(false);        
        _resetBtn.SetActive(true);   
        _shotBtn.SetActive(true);   

        StartCoroutine("Canvas_delay");
    }

    IEnumerator Canvas_delay()
    {
        yield return new WaitForSeconds(0.2f);
        //캔버스 비활성
        canvas_check = false;
    }
    public void Reset_Animal()
    {
        // 이미 로드된 오브젝트가 있으면 오브젝트 삭제
        if (spawnedObject)
        {
            DestroySpawnObject();            
        }

        //없으면 캔버스만 활성화            
        _canvas.SetActive(true);
        _resetBtn.SetActive(false);
        _shotBtn.SetActive(false);
        _Status_UI.SetActive(false);

        //캔버스 활성
        canvas_check = true;
    }
    
}
