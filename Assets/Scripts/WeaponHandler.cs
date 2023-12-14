using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class WeaponHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnFireChanged))]
    [HideInInspector] public bool isFiring { get; set; }

    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GunMode gunMode;
    [SerializeField] int damage=1;
    [SerializeField] float timeBetweenFire=.1f;
    [SerializeField] float timeBetweenUnMorph=1f;
    private float lastFiredTime = 0;
    private float lastUnMorphTime = 0;
    private float hitDistance = 100;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            gunMode.SwapMode();

    }
    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData _networkInputData))
        {
            if (_networkInputData.isFirePressed && gunMode.isFireMode())
            {
                Fire(_networkInputData.aimForwardVector);
            }
            if (_networkInputData.isFirePressed && !gunMode.isFireMode())
            {
                UnMorph(_networkInputData.aimForwardVector);
            }
        }
    }

    private void UnMorph(Vector3 _aimForwardVector)
    {

        // if(Time.time - lastTimeFired < .15f)//TODO: MN
        // {
        //     return;
        // }
        Runner.LagCompensation.Raycast(aimPoint.position, _aimForwardVector, hitDistance, Object.InputAuthority, out var hitInfo, targetLayerMask, HitOptions.IncludePhysX); //TODO: MN

        if(hitInfo.Distance > 0) { hitDistance = hitInfo.Distance; }

        if(hitInfo.Hitbox != null)
        {
            Debug.Log($"{Time.time} {transform.name} hit hitbox {hitInfo.Hitbox.transform.root.name}");

            hitInfo.Hitbox.transform.root.GetComponent<Morph>().RPC_UnMorph();
        }

        // lastUnMorphTime = Time.time;
    }
    private void Fire(Vector3 _aimForwardVector)
    {
        if(Time.time - lastFiredTime < timeBetweenFire)//TODO: MN
        {
            return;
        }

        StartCoroutine(FireFX());

        Runner.LagCompensation.Raycast(aimPoint.position, _aimForwardVector, hitDistance, Object.InputAuthority, out var hitInfo, targetLayerMask, HitOptions.IncludePhysX); //TODO: MN

        bool isHitOtherPlayer = false;

        if(hitInfo.Distance > 0) { hitDistance = hitInfo.Distance; }

        if(hitInfo.Hitbox != null)
        {
            Debug.Log($"{Time.time} {transform.name} hit hitbox {hitInfo.Hitbox.transform.root.name}");
            if (Object.HasStateAuthority && hitInfo.Hitbox.transform.root.GetComponent<Morph>()?.index==-1)
            {
                hitInfo.Hitbox.transform.root.GetComponent<HealthSystem>().RPC_OnTakeDamage();
                StartCoroutine(HitFX());
            }
            isHitOtherPlayer = true;
        }
        else if(hitInfo.Collider != null)
        {
            Debug.Log($"{Time.time} {transform.name} hit PhysX collider {hitInfo.Collider.transform.name}");
        }

        if (isHitOtherPlayer)
        {
            Debug.DrawRay(aimPoint.position, _aimForwardVector * hitDistance, Color.red, 1);
        }
        else
        {
            Debug.DrawRay(aimPoint.position, _aimForwardVector * hitDistance, Color.green, 1);
        }
        
        
        lastFiredTime = Time.time;
    }

    private IEnumerator FireFX()
    {
        isFiring = true;
        fireParticleSystem.Play();
        yield return new WaitForSeconds(timeBetweenFire);
        isFiring = false;
    }

    private IEnumerator HitFX()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(timeBetweenFire);
        hitMarker.SetActive(false);
    }

    private static void OnFireChanged(Changed<WeaponHandler> _changed)
    {
        bool isFiringCurrent = _changed.Behaviour.isFiring;

        _changed.LoadOld();

        bool isFiringOld = _changed.Behaviour.isFiring;

        if (isFiringCurrent && !isFiringOld)
        {
            _changed.Behaviour.OnFireRemote();
        }
    }

    private void OnFireRemote()
    {
        if (!Object.HasInputAuthority)
        {
            fireParticleSystem.Play();
        }
    }
}