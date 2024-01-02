using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using System;

public class WeaponHandler : NetworkBehaviour
{ 
    [Networked(OnChanged = nameof(OnFireChanged))]
    [HideInInspector]public bool isFiring{ get; set; }
    [SerializeField] AudioClip vacuumAudioClip;
    [SerializeField] AudioClip unmorphAudioClip;
    [Space]
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GunMode gunMode;
    [SerializeField] private int ammoMaxCount;
    [SerializeField] private float timeToReload;
    private int ammoCurrentCount;
    [SerializeField] TMP_Text ammoCountTxt;
    private AudioHandler audioHandler;
    private float timebetweenFire=0.1f;
    private float timebetweenUnmoprh=0.1f;
    private float lastTimeFired = 0;
    private float lastTimeUnmorph = 0;
    private Coroutine  reloadCoroutine;
    private bool isReloading=false;
    void Start()
    {
        audioHandler = GetComponent<AudioHandler>();
        ammoCurrentCount=ammoMaxCount;
        ammoCountTxt.text=ammoCurrentCount.ToString();
    }
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            isReloading=true;
            reloadCoroutine = StartCoroutine(Reload());
        }       
        if(Input.GetKeyUp(KeyCode.R))
        {
            isReloading=false;
            StopCoroutine(reloadCoroutine);
        }
        if(!isReloading && !isFiring  && Input.GetMouseButtonDown(1))
        {
            gunMode.RPC_SwapMode();
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(timeToReload);
        ammoCurrentCount=ammoMaxCount;
        ammoCountTxt.text=ammoCurrentCount.ToString();
    }


    public override void FixedUpdateNetwork()
    {
        if(isReloading)
            return;
        if(GetInput(out NetworkInputData _networkInputData))
        {
            if (_networkInputData.isFirePressed && gunMode.isVacuumMode)
            {
                isFiring = true;
                Fire(_networkInputData.aimForwardVector);
                audioHandler.PlayClip(vacuumAudioClip);
            }
            else if(!_networkInputData.isFirePressed && gunMode.isVacuumMode)
            {
                isFiring=false;
                audioHandler.StopClip(vacuumAudioClip);
            }
            if (_networkInputData.isFirePressed && !gunMode.isVacuumMode)
            {
                UnMorph(_networkInputData.aimForwardVector);
                audioHandler.PlayClip(unmorphAudioClip);
            }
            else if (!_networkInputData.isFirePressed && !gunMode.isVacuumMode)
            {
                audioHandler.StopClip(unmorphAudioClip);
            }
        }
    }

    private void UnMorph(Vector3 _aimForwardVector)
    {
        List<LagCompensatedHit> hitInfo2=new List<LagCompensatedHit>();
        if(Time.time - lastTimeUnmorph < timebetweenUnmoprh)//TODO: MN
        {
            return;
        }
        //Runner.LagCompensation.Raycast(aimPoint.position, _aimForwardVector, 100, Object.InputAuthority, out var hitInfo, targetLayerMask, HitOptions.IncludePhysX); //TODO: MN
        // float hitDistance = 0;
        // if(hitInfo.Distance > 0) { hitDistance = hitInfo.Distance; }

        // if(hitInfo.Hitbox != null)
        // {
        //     Debug.Log($"{Time.time} {transform.name} hit hitbox {hitInfo.Hitbox.transform.root.name}");

        //     hitInfo.Hitbox.transform.root.GetComponent<Morph>().RPC_UnMorph();
        // }
        // Runner.LagCompensation.OverlapBox(aimPoint.position,
        //                                     new Vector3(1,1,1),
        //                                     Quaternion.Euler(_aimForwardVector),
        //                                     Object.InputAuthority,
        //                                     hitInfo2,
        //                                     targetLayerMask,
        //                                     HitOptions.IncludePhysX);
        
        Runner.LagCompensation.OverlapSphere(aimPoint.position + _aimForwardVector,1,Object.InputAuthority,hitInfo2,targetLayerMask,HitOptions.IncludePhysX); 
   
        for(int i=0;i<hitInfo2.Count;i++)
        {
            if(hitInfo2[i].Hitbox != null )
            {
                Debug.Log($" {transform.name} hit hitbox {hitInfo2[i].Hitbox.transform.root.name} distance {hitInfo2[i].Distance}");
                hitInfo2[i].Hitbox.transform.root.GetComponent<Morph>()?.RPC_UnMorph();
            }
            if(hitInfo2[i].Collider != null)
            {
                Debug.Log(hitInfo2[i].Collider.transform.name);
                //Debug.Log($"{Time.time} {transform.name} hit PhysX collider {hitInfo.Collider.transform.name}");
            }
        } 
        lastTimeUnmorph = Time.time;
    }
    private void Fire(Vector3 _aimForwardVector)
    {
        if(ammoCurrentCount<=0 )
        {
            isFiring=false;
            ammoCountTxt.text=ammoCurrentCount.ToString();
            return;
        }
        if(Time.time - lastTimeFired < timebetweenFire)//TODO: MN
        {
            return;
        }

        StartCoroutine(FireFX());

        Runner.LagCompensation.Raycast(aimPoint.position, _aimForwardVector, 100, Object.InputAuthority, out var hitInfo, targetLayerMask, HitOptions.IncludePhysX); //TODO: MN
        float hitDistance = 0;

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
        ammoCurrentCount--;
        ammoCountTxt.text=ammoCurrentCount.ToString();
        lastTimeFired = Time.time;
    }
    private IEnumerator HitFX()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(timebetweenFire);
        hitMarker.SetActive(false);
    }

    private IEnumerator FireFX()
    {
        fireParticleSystem.Play();
        yield return new WaitForSeconds(timebetweenFire);//TOIMPROVE: define this
    }
    private static void OnFireChanged(Changed<WeaponHandler> _changed)
    {
        bool FiringCurrent = _changed.Behaviour.isFiring;

        _changed.LoadOld();

        bool FiringOld = _changed.Behaviour.isFiring;
        
        if (FiringCurrent && !FiringOld)
        {
            _changed.Behaviour.OnFireRemote();
            
        }

    }
    private void OnFireRemote()
    {
        if (!Object.HasInputAuthority)
        {
            fireParticleSystem.Play();
            Debug.Log("!");
        }
    }
}