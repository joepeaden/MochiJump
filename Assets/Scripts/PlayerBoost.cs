using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoost : MonoBehaviour
{
    [SerializeField]
    private const int BOOST_METER_CAPACITY = 5;

    [SerializeField]
    private float boostThrust = 800f;
    
    private int boostMeter = 0;

    private PlayerVisuals visuals;

    public int BoostMeter
    {
        get => boostMeter;
        set
        {
            // don't let boost meter fill beyond capacity
            if (value != 0 && boostMeter >= BOOST_METER_CAPACITY)
                return;
            boostMeter = value;

            // update visuals for player shader
            visuals?.UpdatePowerupVisual(boostMeter, BOOST_METER_CAPACITY);
        }
    }

    private void Start()
    {
        visuals = GetComponent<PlayerVisuals>();
    }

    public void Boost()
    {
        // only launch if boost meter is full
        if(boostMeter != BOOST_METER_CAPACITY)
        {
            return;
        }

        // same that the platforms give
        float thrust = boostThrust;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * thrust);

        BoostMeter = 0;
    }

}
