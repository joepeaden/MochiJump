using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBoost : MonoBehaviour
{
    [SerializeField]
    private const int BOOST_METER_CAPACITY = 5;

    [SerializeField]
    private float boostThrust = 800f;
    
    private int boostMeter = 0;

    private PlayerVisuals visuals;
    
    private Image boostButtonRenderer;

    [SerializeField]
    // for filling the boost meter for debugging
    private bool infiniteBoost;

    [SerializeField]
    private GameObject boostBurst;

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

            // update boost button shader
            // i feel pretty bad about this implementation. but whatever. 2 beers in, les go
            boostButtonRenderer.material.SetFloat("_BoostMeter", ((float) boostMeter )/ ((float) BOOST_METER_CAPACITY));
        }
    }

    private void Start()
    {
        visuals = GetComponent<PlayerVisuals>();
        boostButtonRenderer = GameObject.FindGameObjectWithTag("BoostButton").GetComponent<Image>();
    }

    public void Boost()
    {
        // only launch if boost meter is full
        if(!infiniteBoost && boostMeter != BOOST_METER_CAPACITY)
        {
            return;
        }

        // same that the platforms give
        float thrust = boostThrust;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * thrust);

        Instantiate(boostBurst, transform.position, Quaternion.identity);

        BoostMeter = 0;
    }

}
