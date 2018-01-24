using System.Collections;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{
    private int _playerNumber = 0;

    private PlayerInput _input;

    void Update()
    {
        // Movements input
        bool jumpKey = Input.GetKey(_input.up);
        bool leftKey = Input.GetKey(_input.left);
        bool rightKey = Input.GetKey(_input.right);

        if (jumpKey) Jump();
        if (leftKey || rightKey) Move(leftKey);

        // Abilities input
        bool firstAbilityKey = Input.GetKeyDown(_input.a);
        bool secondAbilityKey = Input.GetKeyDown(_input.b);
        bool ultimateKey = Input.GetKeyDown(_input.c);

        if (firstAbilityKey) StartCoroutine(Ability1());
        if (secondAbilityKey) StartCoroutine(Ability2());
        if (ultimateKey) StartCoroutine(Ultimate());
    }

    public void SetPlayer(int playerNumber)
    {
        _playerNumber = playerNumber;
        _input = InputConfig.GetPlayerInput(_playerNumber);
    }

    // Movements behaviors
    protected abstract void Jump();
    protected abstract void Move(bool isLeft);

    // Abilities behaviors
    protected abstract IEnumerator Ability1();
    protected abstract IEnumerator Ability2();
    protected abstract IEnumerator Ultimate();
    protected abstract IEnumerator StartCooldown(int ability);
    protected abstract IEnumerator StartPassiveUltGen();
    public abstract void GainUltCharge(float amount);
    public abstract float GetUltCharge();
}