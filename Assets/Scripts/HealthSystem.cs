using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class HealthSystem : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnHPChanged))]
    [SerializeField] private int HP { get; set; }

    [Networked(OnChanged = nameof(OnStateChanged))]
    public bool isDead { get; set; }

    private bool isInitialized = false;
    private const float hpRegTime = 5;
    private const float hpRegTime2 = 0.15f;
    private const int startingHP = 100;

    [SerializeField] private GameObject onHitImage;
    [SerializeField] private TMP_Text healthTxt;
    [SerializeField] private GameObject jar;
    List<Coroutine> coroutines;
    private Morph morph;
    
    void Awake()
    {
        morph = GetComponent<Morph>();
    }
    private void Start()
    {
        coroutines=new List<Coroutine>();
        HP = startingHP;
        isDead = false;

        isInitialized = true;
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
        onHitImage.SetActive(true);

        yield return new WaitForSeconds(.2f);

        if (!isDead)
        {
            onHitImage.SetActive(false);
        }
    }
    IEnumerator HealthRegeneration(float FirstWaiting,float ForWainting)
    {
        yield return new WaitForSeconds(FirstWaiting);
        while(!isDead && HP < startingHP)
            {
                HP += 1;
                yield return new WaitForSeconds(ForWainting);
            }
    }
    [Rpc]//TOIMPROVE: source & target
    public void RPC_OnTakeDamage()
    {
        if (isDead || morph.index!=-1 ){return;}

        HP--;
        for(int i=0;i< coroutines.Count;i++)
            StopCoroutine(coroutines[i]);
        
        coroutines.Clear();
        
        coroutines.Add( StartCoroutine(HealthRegeneration(hpRegTime,hpRegTime2)));

        Debug.Log($"{transform.name} took damage got {HP} left");
        if (HP <= 0)
        {
            isDead = true;
            Debug.Log($"{transform.name} died");
            GetComponent<CharacterController>().enabled=false;
            jar.SetActive(true); 
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

        if (newHP < oldHP) { _changed.Behaviour.OnHPReduced(); }
    }

    private void OnHPReduced()
    {
        if (!isInitialized) { return; }

        StartCoroutine(OnHit());
    }

    private void DebugHP() { healthTxt.text = HP.ToString(); }

    private static void OnStateChanged(Changed<HealthSystem> _changed)
    {

    }
}