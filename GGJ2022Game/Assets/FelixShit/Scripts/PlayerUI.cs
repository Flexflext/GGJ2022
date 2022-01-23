using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image doubleJumpImageUP;
    [SerializeField] private TMP_Text doubleJumpTextUP;

    [SerializeField] private Image trapImageUP;
    [SerializeField] private TMP_Text trapTextUP;


    [SerializeField] private Image doubleJumpImageDown;
    [SerializeField] private TMP_Text doubleJumpTextDown;

    [SerializeField] private Image trapImageDown;
    [SerializeField] private TMP_Text trapTextDown;

    public void ChangeTimerOfTrapUp(float _cur, float _max)
    {
        if (_cur < _max)
        {
            if (!trapTextUP.gameObject.activeSelf)
            {
                trapTextUP.gameObject.SetActive(true);
                trapImageUP.color = new Color(trapImageUP.color.r, trapImageUP.color.g, trapImageUP.color.b, 0.5f);
            }

            trapTextUP.text = ((int)(_max - _cur)).ToString();

        }
        else
        {
            if (trapTextUP.gameObject.activeSelf)
            {
                trapTextUP.gameObject.SetActive(false);
                trapImageUP.color = new Color(trapImageUP.color.r, trapImageUP.color.g, trapImageUP.color.b, 1);
            }
        }
    }

    public void ChangeTimerOfTrapDown(float _cur, float _max)
    {
        if (_cur < _max)
        {
            if (!trapTextDown.gameObject.activeSelf)
            {
                trapTextDown.gameObject.SetActive(true);
                trapImageDown.color = new Color(trapImageDown.color.r, trapImageDown.color.g, trapImageDown.color.b, 0.5f);
            }

            trapTextDown.text = ((int)(_max - _cur)).ToString();

        }
        else
        {
            if (trapTextDown.gameObject.activeSelf)
            {
                trapTextDown.gameObject.SetActive(false);
                trapImageDown.color = new Color(trapImageDown.color.r, trapImageDown.color.g, trapImageDown.color.b, 1);
            }
        }
    }

    public void ChangeTimerOfDoubleJumpUp(float _cur, float _max)
    {
        if (_cur < _max)
        {
            if (!doubleJumpTextUP.gameObject.activeSelf)
            {
                doubleJumpTextUP.gameObject.SetActive(true);
                doubleJumpImageUP.color = new Color(doubleJumpImageUP.color.r, doubleJumpImageUP.color.g, doubleJumpImageUP.color.b, 0.5f);

            }

            doubleJumpTextUP.text = ((int)(_max - _cur)).ToString();
        }
        else
        {
            if (doubleJumpTextUP.gameObject.activeSelf)
            {
                doubleJumpTextUP.gameObject.SetActive(false);
                doubleJumpImageUP.color = new Color(doubleJumpImageUP.color.r, doubleJumpImageUP.color.g, doubleJumpImageUP.color.b, 1);
            }
        }
    }

    public void ChangeTimerOfDoubleJumpDown(float _cur, float _max)
    {

        if (_cur < _max)
        {
            if (!doubleJumpTextDown.gameObject.activeSelf)
            {
                doubleJumpTextDown.gameObject.SetActive(true);
                doubleJumpImageDown.color = new Color(doubleJumpImageDown.color.r, doubleJumpImageDown.color.g, doubleJumpImageDown.color.b, 0.5f);
            }

            doubleJumpTextDown.text = ((int)(_max - _cur)).ToString();

        }
        else
        {
            if (doubleJumpTextDown.gameObject.activeSelf)
            {
                doubleJumpTextDown.gameObject.SetActive(false);
                doubleJumpImageDown.color = new Color(doubleJumpImageDown.color.r, doubleJumpImageDown.color.g, doubleJumpImageDown.color.b, 1);
            }
        }
    }

}
