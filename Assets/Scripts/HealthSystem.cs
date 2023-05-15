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

    private const int startingHP = 5;

    [SerializeField] private GameObject onHitImage;
    [SerializeField] private TMP_Text healthTxt;

    private void Start()
    {

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

        onHitImage.SetActive(true);

        yield return new WaitForSeconds(.2f);

        if (!isDead)
        {
            onHitImage.SetActive(false);
        }
    }
    IEnumerator RegHP(float FirstWaiting,float ForWainting)
    {
        yield return new WaitForSeconds(FirstWaiting);
        Debug.Log("5");
            while ( !isDead && HP < startingHP )
            {
                Debug.Log("1");
                HP += 1;
                yield return new WaitForSeconds(ForWainting);
            }
    } 
    [Rpc]//TOIMPROVE: source & target
    public void RPC_OnTakeDamage()
    {
        if (isDead) { return; }
        HP--;
        StopAllCoroutines();
        StartCoroutine(RegHP(5f,1f));
        Debug.Log($"{transform.name} took damage got {HP} left");

        if (HP <= 0)
        {
            Debug.Log($"{transform.name} died");
            isDead = true;
        }
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