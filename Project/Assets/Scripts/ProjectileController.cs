using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float m_fInputDeltaVal = 0.1f;

    private ProjectileComponent m_projectile = null;

    void Start()
    {
        m_projectile = GetComponent<ProjectileComponent>();
        Assert.IsNotNull(m_projectile, "We've got a problem! ProjectileComponent is not attached");
    }
    void Update()
    {
        HandleUserInput();
    }
    private void HandleUserInput()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            m_projectile.OnLaunchProjectile();
        }
        if (Input.GetKey(KeyCode.W))
        {
            m_projectile.OnMoveForward(m_fInputDeltaVal);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_projectile.OnMoveBackward(m_fInputDeltaVal);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_projectile.OnMoveRight(m_fInputDeltaVal);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_projectile.OnMoveLeft(m_fInputDeltaVal);
        }
        if (Input.GetKey(KeyCode.E))
        {
            m_projectile.OnMoveUp(m_fInputDeltaVal);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            m_projectile.OnMoveDown(m_fInputDeltaVal);
        }
    }
}
