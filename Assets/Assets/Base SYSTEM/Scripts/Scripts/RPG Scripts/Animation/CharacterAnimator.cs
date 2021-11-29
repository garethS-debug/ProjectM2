using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    #region ListOf Movement
    //Movement
    NavMeshAgent agent;
    protected Animator animator;
    const float locomotionAnimSmoothtime = 0.1f;
    //Movement
    #endregion

    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;
    protected AnimationClip[] currentAttackAnimSet;
    public AnimationClip[] defaultAttackAnimSet;
    public AnimationClip replaceableAttackAnim;

    // Use this for initialization
    protected virtual void Start ()
    {
        #region ListOf Movement
        //Movement
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        //Movement
        #endregion
        combat = GetComponent<CharacterCombat>();
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;
        currentAttackAnimSet = defaultAttackAnimSet;

        //combat.OnAttack += OnAttack;

    }

    // Update is called once per frame
    protected virtual void Update () 
    {
        #region ListOf Movement
        //Movement
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimSmoothtime, Time.deltaTime);
        //Movement
        #endregion

        //animator.SetBool("inCombat", combat.InCombat);

    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);//random attack animation in index
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
