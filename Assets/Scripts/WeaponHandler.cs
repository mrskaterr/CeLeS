using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Security.Cryptography.X509Certificates;

public class WeaponHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnFireChanged))]
    public bool isFiring{ get; set; }
    [Networked]
    public bool isCurrentFiring{ get; set; }
    private float FiringTime{ get; set; }
    [SerializeField] private ParticleSystem fireParticleSystem;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GunMode gunMode;
    private float timebetweenFire=0.1f;
    private float lastTimeFired = 0;
    private float hitDistance = 100;
    void Start()
    {
        isCurrentFiring=false;
        FiringTime = Time.time;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(1))//to do network
            gunMode.SwapMode();
    }
    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData _networkInputData))
        {
            if (/*_networkInputData.isFirePressed*/true && gunMode.fireMode)
            {
                Fire(_networkInputData.aimForwardVector);
                isFiring = true;
            }
            else if(!_networkInputData.isFirePressed && gunMode.fireMode)
            {
                isFiring=false;
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
        if(!FiringCurrent && FiringOld)
        {
            _changed.Behaviour.FiringTime=Time.time;
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