using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using Stella.GameLogic.Character;
using UnityEngine;

namespace Stella.GameLogic
{
    [DefaultExecutionOrder(-999)]
    public class CameraController : MonoBehaviour
    {
        public static CameraController Instance { get; private set; }

        [SerializeField, Required] private CinemachineVirtualCamera virtualCam = null;
        [SerializeField, Required] private CinemachineTargetGroup targetGroup = null;
        private CinemachineBasicMultiChannelPerlin noiseModule = null;
        private CinemachineFramingTransposer transposer = null;

        private CinemachineBrain coreLogic = null;

        private List<CinemachineTargetGroup.Target> targetList = new List<CinemachineTargetGroup.Target>();
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            // cam = GetComponentInChildren<Camera>();
            var comps = virtualCam.GetComponentPipeline();
            foreach (var comp in comps)
            {
                if (comp is CinemachineBasicMultiChannelPerlin noise)
                    noiseModule = noise;
                else if (comp is CinemachineFramingTransposer transposer)
                    this.transposer = transposer;
            }

            noiseModule.m_AmplitudeGain = 0f;
            noiseModule.m_FrequencyGain = 12f;
        }

        public void SetCharacter(CharacterBase character)
        {
            // TODO
            var camTarget = character.CamTarget;
            var target = new CinemachineTargetGroup.Target();
            target.target = camTarget;
            target.weight = 3;
            targetList.Add(target);
            targetGroup.m_Targets = targetList.ToArray();
        }

        public void Shake(float power)
        {
            if (shakeRoutine != null)
            {
                StopCoroutine(shakeRoutine);
                shakeRoutine = null;
            }

            shakeRoutine = StartCoroutine(_Shake(power));
        }

        private Coroutine shakeRoutine = null;
        private IEnumerator _Shake(float power)
        {
            noiseModule.m_AmplitudeGain = power;
            while (noiseModule.m_AmplitudeGain > float.Epsilon)
            {
                yield return null;
                noiseModule.m_AmplitudeGain -= noiseModule.m_AmplitudeGain * Time.deltaTime * 5f;
            }
            noiseModule.m_AmplitudeGain = 0f;
        }
        
        public void SetCameraScreenXY(float screenX, float screenY)
        {
            if (transposer == null)
            {
                return;
            }

            transposer.m_ScreenX = screenX;
            transposer.m_ScreenY = screenY;
        }

        private void LateUpdate()
        {
            
        }
    }
}
