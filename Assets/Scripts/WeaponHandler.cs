using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Security.Cryptography.X509Certificates;

public class WeaponHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnFireChanged))]
    public bool isFiring{ get; set; }
    [Networked(OnChanged = nameof(OnFireCurrentChanged))]
    public bool isFiringCurrent2{ get; set; }
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GunMode gunMode;
    private float timebetweenFire=0.10f;
    private float lastTimeFired = 0;
    private float hitDistance = 100;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))//to do network
            gunMode.SwapMode();
    }
    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData _networkInputData))
        {
            if (true/*_networkInputData.isFirePressed/*/ && gunMode.fireMode)
            {
                Fire(_networkInputData.aimForwardVector);
                isFiringCurrent2=true;
            }
            if(!_networkInputData.isFirePressed/*true*/ && gunMode.fireMode)
            {
                isFiringCurrent2=false;
            }
            if (_networkInputData.isFirePressed && !gunMode.fireMode)
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
        Runner.LagCompensation.Raycast(aimPoint.position, _aimForwardVector, 100, Object.InputAuthority, out var hitInfo, targetLayerMask, HitOptions.IncludePhysX); //TODO: MN


        if(hitInfo.Distance > 0) { hitDistance = hitInfo.Distance; }

        if(hitInfo.Hitbox != null)
        {
            Debug.Log($"{Time.time} {transform.name} hit hitbox {hitInfo.Hitbox.transform.root.name}");

            hitInfo.Hitbox.transform.root.GetComponent<Morph>().RPC_UnMorph();
        }
    }
    private void Fire(Vector3 _aimForwardVector)
    {

        if(Time.time - lastTimeFired < timebetweenFire)//TODO: MN
        {
            return;
        }

        StartCoroutine(FireFX());

        Runner.LagCompensation.Raycast(aimPoint.position, _aimForwardVector, 100, Object.InputAuthority, out var hitInfo, targetLayerMask, HitOptions.IncludePhysX); //TODO: MN


        bool isHitOtherPlayer = false;

        if(hitInfo.Distance > 0) { hitDistance = hitInfo.Distance; }

        if(hitInfo.Hitbox != null)
        {
            Debug.Log(hitInfo.Hitbox.transform.root.name);

            if (Object.HasStateAuthority && hitInfo.Hitbox.transform.root.GetComponent<Morph>()?.index==-1)
            {
                hitInfo.Hitbox.transform.root.GetComponent<HealthSystem>().RPC_OnTakeDamage();
                StartCoroutine(HitFX());
            }
            isHitOtherPlayer = true;
        }
        else if(hitInfo.Collider != null)
        {
            Debug.Log(hitInfo.Collider.transform.name);
            //Debug.Log($"{Time.time} {transform.name} hit PhysX collider {hitInfo.Collider.transform.name}");
        }

        if (isHitOtherPlayer)
        {
            Debug.DrawRay(aimPoint.position, _aimForwardVector * hitDistance, Color.red, 1);
        }
        else
        {
            Debug.DrawRay(aimPoint.position, _aimForwardVector * hitDistance, Color.green, 1);
        }

        lastTimeFired = Time.time;
    }
    private IEnumerator HitFX()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(.2f);
        hitMarker.SetActive(false);
    }

    private IEnumerator FireFX()
    {
        isFiring = true;
        fireParticleSystem.Play();
        yield return new WaitForSeconds(.09f);//TOIMPROVE: define this
        isFiring = false;
        //isfiring=true; to do
    }
    private static void OnFireChanged(Changed<WeaponHandler> _changed)
    {
        bool isFiringCurrent = _changed.Behaviour.isFiring;

        _changed.LoadOld();

        bool isFiringOld = _changed.Behaviour.isFiring;
        
        
        //_changed.LoadNew();// WoW  łeb mi się naprawił
        
        
        if (isFiringCurrent && !isFiringOld)
        {
            _changed.Behaviour.OnFireRemote();
            //RotationSpeed()//ViewVerticalSpeed() to do 
        }

    }
    
    private static void OnFireCurrentChanged(Changed<WeaponHandler> _changed)
    {
        bool buff = _changed.Behaviour.isFiringCurrent2;

        _changed.LoadOld();

        bool buffOld = _changed.Behaviour.isFiringCurrent2;

        if (buff && !buffOld)
        {
            Debug.Log("onfire");
        }
        else if(!buff && buffOld)
        {
            Debug.Log("onfire2");
        }
        else
            Debug.Log("else");

    }

    private void onfire()
    {
        Debug.Log("onfire");   
    }
    private void onfire2()
    {
        Debug.Log("onfire2");
    }

    private void OnFireRemote()
    {
        if (Object.HasInputAuthority)
        {
            fireParticleSystem.Play();
            Debug.Log("!");
        }
    }
}