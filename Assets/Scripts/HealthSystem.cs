using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class HealthSystem : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnHPChanged))]
    private int HP { get; set; }
    [Networked(OnChanged = nameof(OnStateChanged))]
    public bool isDead { get; set; }
    [SerializeField] private float timeToRegeneration=5f;
    [SerializeField] private float speedRegeneration=1f;
    [SerializeField] int MaxHP;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private GameObject jar;
    [SerializeField] private GameObject body;
    List<Coroutine> coroutines;
    private bool isInitialized = false;
    private PlayerHUD HUD;
    private CaptureHandler captureHandler;
    private void Awake()
    {
        HUD = GetComponent<PlayerHUD>();
        captureHandler = GetComponent<CaptureHandler>();
        
        coroutines=new List<Coroutine>();
        HP = MaxHP;
        isDead = false;
        isInitialized = true;
    }

    private void Update()
    {
        jar.SetActive(isDead && !captureHandler.isCarried);////////////
    }

    private IEnumerator OnHit()
    {
        //if (Object.HasInputAuthority)
        //{
            
        //}
        //else { yield return null; }
        // if(isDead)
        // {
         
        // }
        HUD.ToggleOnHitImage(true);

        yield return new WaitForSeconds(.2f);

        if (!isDead)
        {
            HUD.ToggleOnHitImage(false);
        }
        else
        {
            HUD.ToggleMiniGame(true);
        }
    }
    // IEnumerator HealthRegeneration(float FirstWaiting,float ForWainting)
    // {
    //     yield return new WaitForSeconds(FirstWaiting);
    //     while(!isDead && HP <= MaxHP)
    //     {
    //         HP += 1;
    //         yield return new WaitForSeconds(ForWainting);
    //     }
    //     if(HP>MaxHP)HP=MaxHP;
    // }
    [Rpc]//TOIMPROVE: source & target
    public void RPC_OnTakeDamage()
    {
        HP--;
        // for(int i=0;i< coroutines.Count;i++)
        //     StopCoroutine(coroutines[i]);
        
        // coroutines.Clear();
        
        // coroutines.Add( StartCoroutine(HealthRegeneration(timeToRegeneration,speedRegeneration)));

        Debug.Log($"{transform.name} took damage got {HP} left");
        if (HP <= 0)
        {
            Debug.Log($"{transform.name} died");
            isDead = true;
        }
        if (isDead)
        {
            GetComponent<CharacterController>().enabled = false;
            
            body.SetActive(false);
            captureHandler.isFree = false;
            return;
        }
    }
    private void Damage()
    {
        //yield return new WaitForSeconds(0.2f);
    }

    private static void OnHPChanged(Changed<HealthSystem> _changed)
    {
        _changed.Behaviour.DebugHP();
        int newHP = _changed.Behaviour.HP;
        _changed.LoadOld();
        int oldHP = _changed.Behaviour.HP;

        if (newHP < oldHP) { _changed.Behaviour.OnHpReduced(); }
    }

    private void OnHpReduced()
    {
        if (!isInitialized) { return; }

        StartCoroutine(OnHit());
    }

    private void DebugHP() { healthTxt.text = HP.ToString(); }

    private static void OnStateChanged(Changed<HealthSystem> _changed)
    {

    }

    public void Restore()
    {
        HP = MaxHP;
        isDead = false;
        GetComponent<CharacterController>().enabled = true;
        body.SetActive(true);
        HUD.ToggleCrosshair(true);
        HUD.ToggleOnHitImage(false);
    }
}