//
// Rain Maker (c) 2016 Digital Ruby, LLC
// http://www.digitalruby.com
//

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace DigitalRuby.RainMaker
{
    public class RainCollision : MonoBehaviour
    {
        private static readonly Color32 color = new Color32(255, 255, 255, 255);
        private readonly List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        public ParticleSystem RainExplosion;
        public ParticleSystem RainParticleSystem;
        //체크함수
        bool limit_check = false;
        Slider h_slider;

        private void Start()
        {
            //청결상태 증가
            h_slider = SceneMgr.health_bar.GetComponent<Slider>();
        }

        private void Update()
        {

        }

        private void Emit(ParticleSystem p, ref Vector3 pos)
        {
            int count = UnityEngine.Random.Range(2, 5);
            while (count != 0)
            {
                float yVelocity = UnityEngine.Random.Range(1.0f, 3.0f);
                float zVelocity = UnityEngine.Random.Range(-2.0f, 2.0f);
                float xVelocity = UnityEngine.Random.Range(-2.0f, 2.0f);
                const float lifetime = 0.75f;// UnityEngine.Random.Range(0.25f, 0.75f);
                float size = UnityEngine.Random.Range(0.05f, 0.1f);
                ParticleSystem.EmitParams param = new ParticleSystem.EmitParams();
                param.position = pos;
                param.velocity = new Vector3(xVelocity, yVelocity, zVelocity);
                param.startLifetime = lifetime;
                param.startSize = size;
                param.startColor = color;
                p.Emit(param, 1);
                count--;
            }
        }

        private void OnParticleCollision(GameObject obj)
        {
            if (RainExplosion != null && RainParticleSystem != null)
            {
                int count = RainParticleSystem.GetCollisionEvents(obj, collisionEvents);
                for (int i = 0; i < count; i++)
                {
                    ParticleCollisionEvent evt = collisionEvents[i];
                    Vector3 pos = evt.intersection;
                    Emit(RainExplosion, ref pos);
                }
            }
            if(obj.tag == "Pet")
            {                
                if (h_slider.value < 100.0f)
                {
                    h_slider.value += Time.deltaTime * 4;
                    PlayerPrefs.SetFloat("Health", h_slider.value);
                }
                Pet_on_Plane.spawnedObject.GetComponent<Animator>().SetInteger("animation", 3);
                
                if (Pet_on_Plane.spawnedObject.transform.eulerAngles.y > 210.0f && limit_check == true)
                {
                    limit_check = false;
                }
                if(Pet_on_Plane.spawnedObject.transform.eulerAngles.y < 150.0f && limit_check == false)
                {
                    limit_check = true;
                }
                if (limit_check == true)
                {
                    Pet_on_Plane.spawnedObject.transform.eulerAngles = new Vector3(0, Pet_on_Plane.spawnedObject.transform.eulerAngles.y + 1.0f, 0);
                }
                if (limit_check == false)
                {
                    Pet_on_Plane.spawnedObject.transform.eulerAngles = new Vector3(0, Pet_on_Plane.spawnedObject.transform.eulerAngles.y - 1.0f, 0);
                }
            }
        }

    }
}