using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using Dragon;
using Stats;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Util;
using Random = UnityEngine.Random;

public class DragonController : MonoBehaviour
{
    public ParticleSystem fireBreathEffect;
    private Animator anim;
    int IdleSimple;
    int IdleAgressive;
    int IdleRestless;
    int Walk;
    int BattleStance;
    int Bite;
    int Drakaris;
    int FlyingFWD;
    int FlyingAttack;
    int Hover;
    int Lands;
    int TakeOff;
    int Die;
  
    public DragonConfig dragonConfig;
    private bool seenPlayer = false;
    public enum DragonState
    {
        IdleSimple,
        IdleAgressive,
        IdleRestless,
        Walk,
        BattleStance,
        Bite,
        Drakaris,
        FlyingFWD,
        FlyingAttack,
        Hover,
        Lands,
        TakeOff,
        Die,
        Aggressive,
        Attack
    }

    private Transform player;
    private DragonState currentState;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<Player.Player>().transform;
        anim = GetComponent<Animator>();
        currentState = DragonState.IdleAgressive;
        IdleSimple = Animator.StringToHash("IdleSimple");
        IdleAgressive = Animator.StringToHash("IdleAgressive");
        IdleRestless = Animator.StringToHash("IdleRestless");
        Walk = Animator.StringToHash("Walk");
        BattleStance = Animator.StringToHash("BattleStance");
        Bite = Animator.StringToHash("Bite");
        Drakaris = Animator.StringToHash("Drakaris");
        FlyingFWD = Animator.StringToHash("FlyingFWD");
        FlyingAttack = Animator.StringToHash("FlyingAttack");
        Hover = Animator.StringToHash("Hover");
        Lands = Animator.StringToHash("Lands");
        TakeOff = Animator.StringToHash("TakeOff");
        Die = Animator.StringToHash("Die");

    }
    void ResetAnimationParameters()
    {
        foreach (DragonState state in Enum.GetValues(typeof(DragonState)))
        {
            anim.SetBool(state.ToString(), false);
        }
        anim.SetBool(IdleSimple,true);
    }


    void Update()
    {
        ResetAnimationParameters();
        

        Debug.Log("Current state is " + currentState);
        switch (currentState)
        {
            case DragonState.IdleSimple:
                anim.SetBool(IdleSimple, true);
                break;
            case DragonState.IdleAgressive:
                anim.SetBool(IdleAgressive, true);
                if (PlayerIsDetected())
                {
                    currentState = DragonState.Aggressive;
                    seenPlayer = true;
                }
                break;
            case DragonState.IdleRestless:
                anim.SetBool(IdleRestless, true);
                break;
            case DragonState.Walk:
                anim.SetBool(Walk, true);
                break;
            case DragonState.BattleStance:
                anim.SetBool(BattleStance, true);
                break;
            case DragonState.Bite:
                BitePlayer();
                break;
            case DragonState.Drakaris: //fire breath.
                FireBreath();
                
                break;
            case DragonState.FlyingFWD:
                anim.SetBool(FlyingFWD, true);
                ChasePlayer();
                break;
            case DragonState.Aggressive:
                anim.SetBool(BattleStance, true);
                StartCoroutine(PlayTakeOffAnimation());
                currentState = DragonState.Attack;
                break;
            case DragonState.FlyingAttack:
                anim.SetBool(FlyingAttack, true);
                break;
            case DragonState.Hover:
                anim.SetBool(Hover, true);
                break;
            case DragonState.Lands:
                anim.SetBool(Lands, true);
                break;
            case DragonState.TakeOff:
                anim.SetBool(TakeOff, true);
                break;
            case DragonState.Die:
                anim.SetBool(Die, true);
                break;
            case DragonState.Attack:
               AttackPlayer();
                break;
        }
      
            }
    bool PlayerIsDetected()
    {
        if (seenPlayer) return true;
        Vector3 playerDirection = player.position - transform.position;
        float angle = Vector3.Angle(playerDirection, transform.forward);

        return Vector3.Distance(transform.position, player.position) < dragonConfig.detectionRange &&
               angle < dragonConfig.fovAngle * 0.5f;
    }
    //sets the current attack state
    private void AttackPlayer()
    {
        seenPlayer = true;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < dragonConfig.meleeRange)
        {
            // Player is within melee range
            currentState = DragonState.Bite;
        }
        else if(distanceToPlayer < dragonConfig.shootingRange)
        {
            // Player is in shooting range
            currentState = DragonState.Drakaris;
        }
        else
        {
            currentState = DragonState.FlyingFWD;
            ChasePlayer();
        }
    }
    IEnumerator PlayTakeOffAnimation()
    {
        // Play take off animation with a delay
        yield return new WaitForSeconds(1.5f);
        anim.SetBool(TakeOff, true);
    }

    public void ChasePlayer()
    {
        // Called in the update
        currentState = DragonState.FlyingFWD;
        // Ensure the player reference is not null
        if (player != null)
        {
            // Set the current state to FlyingFWD
            currentState = DragonState.FlyingFWD;

            // Direction to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, dragonConfig.rotationSpeed*Time.deltaTime);
              

         
            transform.Translate(transform.forward * dragonConfig.movementSpeed*Time.deltaTime);
        }

        currentState = DragonState.Attack;

    }

    private void FireBreath()
    {
        anim.SetBool(Drakaris, true);
        fireBreathEffect.Play();
        if (player.TryGetComponent(out StatsManager sm))
        {
            sm.TakeDamage(dragonConfig.shootingDamage * Time.deltaTime);
        }
        AttackPlayer();
    }
    private void BitePlayer()
    {
        
        anim.SetBool(Bite, true);
        if (player.TryGetComponent(out StatsManager sm))
        {
            sm.TakeDamage(dragonConfig.meleeDamage* Time.deltaTime);
        }
        // one in ten chance to transition to drakraris
        if (Random.Range(0, 10) == 4)
        {
            FireBreath();
        }
        AttackPlayer();
    }

    public void OnKilled()
    {
        Debug.Log("Playing Death Anim");
        currentState = DragonState.Die;
        LevelManager.Instance.ChangeSceneDirect("WinScene");
        ExportToCSV.Instance.Record();
    }
    
}
    

