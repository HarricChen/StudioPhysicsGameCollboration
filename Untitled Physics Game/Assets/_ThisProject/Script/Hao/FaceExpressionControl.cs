using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceExpressionControl : MonoBehaviour
{
    public HingeForce hingeForce1;
    public HingeForce hingeForce2;
    public Rigidbody2D hinge1Rb2D;
    public Rigidbody2D hinge2Rb2D;

    public float angularVelocity1;
    public float angularVelocity2;

    public float angularVelocityDizzy;

    public GameObject dizzyFace;

    public Animator eyeAnimator;
    public Animator eyeAnimator2;


    private void Update()
    {
        HardFace();
        DizzyFace();
        
    }

    void HardFace()
    {
        if (hingeForce1.footHold == true || hingeForce2.footHold == true)
            {
                if(angularVelocity1 < angularVelocityDizzy && angularVelocity2 < angularVelocityDizzy)
                {
                    eyeAnimator.SetBool("IsHold", true);
                    eyeAnimator2.SetBool("IsHold", true);
                }

            }

        else
        {
            eyeAnimator.SetBool("IsHold", false);
            eyeAnimator2.SetBool("IsHold", false);
        }


    }

    void DizzyFace()
    {
        angularVelocity1 = Mathf.Abs(hinge1Rb2D.angularVelocity);
        angularVelocity2 = Mathf.Abs(hinge2Rb2D.angularVelocity);

        if (angularVelocity1 >= angularVelocityDizzy || angularVelocity2 >= angularVelocityDizzy)
        {
            dizzyFace.SetActive(true);
        }

        else
        {
            dizzyFace.SetActive(false);
        }
    }

}
