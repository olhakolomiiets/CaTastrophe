using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog2 : MonoBehaviour

{
    private float dogSpeed;
    public float angrySpeed;
    public float chillSpeed;
    Transform player;
    private Animator anim;
    private Dictionary<Type, DogState> behaviorsMap;
    private DogState behaviorCurrent;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.InitBehaviours();
        this.SetBehaviorByDefault();
    }
    private void InitBehaviours()
    {
        behaviorsMap = new Dictionary<Type, DogState>();
        this.behaviorsMap[typeof(DogIdleState)] = new DogIdleState();
        // this.behaviorsMap[typeof(DogAngryState)] = new DogAngryState(angrySpeed, Transform player, Animator anim);
        this.behaviorsMap[typeof(DogPatrolState)] = new DogPatrolState();
    }
    private void SetBehavior(DogState newBehavior)
    {
        if (this.behaviorCurrent != null)
            this.behaviorCurrent.Exit();
        this.behaviorCurrent = newBehavior;
        this.behaviorCurrent.Enter();
    }
    private void SetBehaviorByDefault()
    {
        this.SetBehaviorIdle();
    }
    private DogState GetBehavior<T>() where T : DogState
    {
        var type = typeof(T);
        return this.behaviorsMap[type];
    }
    private void Update()
    {
        if (this.behaviorCurrent != null)
            this.behaviorCurrent.Update();
    }
    public void SetBehaviorAngry()
    {
        var behavior = this.GetBehavior<DogAngryState>();
        this.SetBehavior(behavior);
    }
    public void SetBehaviorIdle()
    {
        var behavior = this.GetBehavior<DogIdleState>();
        this.SetBehavior(behavior);
    }
    public void SetBehaviorPatrol()
    {
        var behavior = this.GetBehavior<DogPatrolState>();
        this.SetBehavior(behavior);
    }
}