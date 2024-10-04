using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
        else
        {
            Debug.LogWarning("G PAS TON/TA " + gameObject.name);
        }
    }

    public void SetBool(string parameterName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameterName, value);
        }
        else
        {
            Debug.LogWarning("G PAS TON/TA " + gameObject.name);
        }
    }

    public void SetTrigger(string parameterName)
    {
        if (animator != null)
        {
            animator.SetTrigger(parameterName);
        }
        else
        {
            Debug.LogWarning("G PAS TON/TA " + gameObject.name);
        }
    }

    public void SetFloat(string parameterName, float value)
    {
        if (animator != null)
        {
            animator.SetFloat(parameterName, value);
        }
        else
        {
            Debug.LogWarning("G PAS TON/TA " + gameObject.name);
        }
    }


}
