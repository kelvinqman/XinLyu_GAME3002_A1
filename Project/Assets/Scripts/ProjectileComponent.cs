using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileComponent : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vInitialVelocity = Vector3.zero;

    [SerializeField]
    private Vector3 m_vInitialVel;

    private Rigidbody m_rb = null;
    private GameObject m_landingDisplay = null;
    private bool m_bIsGrounded = true;
    private UI m_interface = null;
    private void Start()
    {
        m_interface = GetComponent<UI>();
        m_rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rb, "We've got a problem! Rigidbody is not attached!");
        CreateLandingDisplay();
        if(m_interface!=null)
        {
            m_interface.OnRequestUpdateUI(m_rb.velocity.x, m_rb.velocity.y, m_rb.velocity.z);
        }
    }
    private void Update()
    {
        UpdateLandingPosition();
        if(m_interface!=null)
        {
            m_interface.OnRequestUpdateUI(m_rb.velocity.x,m_rb.velocity.y,m_rb.velocity.z);
        }
    }
    private void CreateLandingDisplay()
    {
        m_landingDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        m_landingDisplay.transform.position = Vector3.zero;
        m_landingDisplay.transform.localScale = new Vector3(1f, 0.1f, 1f);
        m_landingDisplay.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        m_landingDisplay.GetComponent<Renderer>().material.color = Color.blue;
        m_landingDisplay.GetComponent<Collider>().enabled = false;
    }
    private void UpdateLandingPosition()
    {
        if (m_landingDisplay != null & m_bIsGrounded)
        {
            m_landingDisplay.transform.position = GetLandingPosition();
        }
    }
    private Vector3 GetLandingPosition()
    {
        Vector3 vFlatVel = m_vInitialVelocity;
        return transform.position + vFlatVel;
    }

    #region CALLBACKS
    public void OnLaunchProjectile()
    {
        if (!m_bIsGrounded)
        {
            return;
        }
        m_landingDisplay.transform.position = GetLandingPosition();
        m_bIsGrounded = false;
        transform.LookAt(m_landingDisplay.transform.position, Vector3.up);


        float fMaxHeight = m_landingDisplay.transform.position.y;
        float fRange = ((m_landingDisplay.transform.position.z - m_rb.transform.position.z) * 2);
        float fTheTa = Mathf.Atan((4 * fMaxHeight) / (fRange));
        float fInitVelMag = Mathf.Sqrt((2 * Mathf.Abs(Physics.gravity.y) * fMaxHeight)) / Mathf.Sin(fTheTa);
        m_vInitialVel.y = fInitVelMag * Mathf.Sin(fTheTa);
        m_vInitialVel.z = fInitVelMag * Mathf.Cos(fTheTa);


        //x/v.x=z/v.z
        //v.x=(x/(z/2))*v.z
        float fX = m_landingDisplay.transform.position.x - m_rb.transform.position.x;
        m_vInitialVel.x = (fX / (fRange / 2)) * m_vInitialVel.z;


        m_rb.velocity = m_vInitialVel;
    }
    public void OnMoveForward(float fDelta)
    {
        m_vInitialVelocity.z += fDelta;
    }
    public void OnMoveBackward(float fDelta)
    {
        m_vInitialVelocity.z -= fDelta;
    }
    public void OnMoveRight(float fDelta)
    {
        m_vInitialVelocity.x += fDelta;
    }
    public void OnMoveLeft(float fDelta)
    {
        m_vInitialVelocity.x -= fDelta;
    }
    public void OnMoveUp(float fDelta)
    {
        m_vInitialVelocity.y += fDelta;
    }
    public void OnMoveDown(float fDelta)
    {
        m_vInitialVelocity.y -= fDelta;
    }
    #endregion
}
