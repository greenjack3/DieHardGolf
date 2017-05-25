using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class Controls : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; 
        public Transform m_Cam;                  
        private Vector3 m_CamForward;             
        private Vector3 m_Move;
        private bool m_Jump;                      
        public string player;

        private void Start()
        {
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown(player + "Jump");
            }
        }


        private void FixedUpdate()
        {
            float h = Input.GetAxis(player + "Horizontal");
            float v = Input.GetAxis(player + "Vertical");
            bool crouch = false;

            if (m_Cam != null)
            {
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v * m_CamForward + h * m_Cam.right;
            }
            else
            {
                m_Move = v * Vector3.forward + h * Vector3.right;
            }

            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
