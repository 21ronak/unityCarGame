using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;


namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    [System.Serializable]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
		public Transform head;
		public SteamVR_TrackedObject leftHand;
		public SteamVR_TrackedObject rightHand;
		 
			
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            //Debug.Log((int)leftHand.index);
            //Debug.Log((int)rightHand.index);
           var lDevice = SteamVR_Controller.Input((int)leftHand.index);
        var rDevice = SteamVR_Controller.Input((int)rightHand.index);
        var accel = lDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger) || rDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger);

        //var lTrigger = lDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger);
        //var rTrigger = rDevice.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger);
        //var lTrigger = lDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
       //var rTrigger = rDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);

        var lTrigger = lDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
        var rTrigger = rDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
        Vector2 rrTrigger = rDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        float gas =  rrTrigger.x;
        //Debug.Log(rrTrigger);
        Vector2 llTrigger = lDevice.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        float brake = llTrigger.x  ;

        if (gas > 0)
        {
           // gas = 1;
        }
        if (brake > 0)
        {
            brake = -brake;
        }

        Vector3 leftDir = leftHand.transform.position - head.position;
        Vector3 rightDir = rightHand.transform.position - head.position;
        Vector3 dir = leftDir + rightDir;

        float steer = steeringAngle();

        //Debug.Log(res);

        //Debug.Log(accel);
        //Debug.Log(rDevice);
        //Debug.Log(leftHand.transform);
        //Debug.Log(rTrigger);

       // transform.position += (dir * .1f);
        
        float vv = 0;
        if (rTrigger)
        {
            vv = 1;
            //Debug.Log(rTrigger);
            SteamVR_Controller.Input((int)leftHand.index).TriggerHapticPulse(500);
        }
        else if(lTrigger)
        {
            vv = -1;
        }

            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            //m_Car.Move(h, v, v, handbrake,res);
            //m_Car.Move(res, acc, deacc, deacc);
            m_Car.Move(steer, gas, brake, brake, steer);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

        public float steeringAngle()
        {
            Vector3 rightCont = rightHand.transform.position;
            Vector3 leftCont = leftHand.transform.position;

            Vector2 rightAxis = new Vector2(rightCont.x, rightCont.y);
            Vector2 leftAxis = new Vector2(leftCont.x, leftCont.y);
            Vector2 difference = rightAxis - leftAxis;
            float sign = (rightAxis.y < leftAxis.y) ? 1.0f : -1.0f;
            float res = Vector2.Angle(Vector2.right, difference) * sign;
            return res;
        }
    }
}
