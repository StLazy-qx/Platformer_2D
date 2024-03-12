using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 10;

    private int _currentHealth;
    private Animator _animator;

    public Transform CurrentPosition => transform;

    public UnityAction<int,int> HealthChange;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _currentHealth = _health;
        HealthChange?.Invoke(_health, _currentHealth);
    }

    public void AddDamage(int damage)
    {
        _currentHealth -= damage;

        //if (_currentHealth <= 0)
            //Died();


    }

    //private void Died()
    //{
    //    //анимация смерти
    //    _animator.SetBool()

    //        //пауза игры
    //}
}
