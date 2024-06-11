using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Demo.PostProcess
{
    public class HitEffect : MonoBehaviour
    {
        public Volume volume;
        public Vignette vignette;

        /**
         ����ɫ�ı����ᵼ�º���ʧЧ
         */

        void Start()
        {
            volume = GetComponent<Volume>();
            volume.profile.TryGet<Vignette>(out vignette);
            vignette.active = false;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(TakeDamage());
            }
        }

        private IEnumerator TakeDamage()
        {
            var intensity = 0.4f;
            
            vignette.active = true;
            vignette.intensity.Override(intensity);

            Debug.Log(vignette.active);

            yield return new WaitForSeconds(0.4f);

            while(intensity > 0)
            {
                intensity -= 0.01f;

                if(intensity < 0)intensity = 0;
                vignette.intensity.Override(intensity);

                yield return new WaitForSeconds(0.1f);
            }

            vignette.active = false;
            yield break;
        }
    }
}