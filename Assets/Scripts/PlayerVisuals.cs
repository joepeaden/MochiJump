using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code here manages visuals related to the player, such as setting 
// shader variables

public class PlayerVisuals : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void UpdatePowerupVisual(float boostMeter, float boostMeterCapacity)
    {
        // man I have gotten so much better at coding this year. Hell yeah!
        // prob still suck tho
        // jk nah I'm awesome

        float value = boostMeter / boostMeterCapacity;
        rend.material.SetFloat("_PowerupEffectMultiplier", value);
    }


}
