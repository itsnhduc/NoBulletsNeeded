using System.Collections;
using UnityEngine;

public abstract class Hero : MonoBehaviour
{

    void Update()
    {
        // Movements input
        bool jumpKey = Input.GetKey(KeyCode.W);
        bool leftKey = Input.GetKey(KeyCode.A);
        bool rightKey = Input.GetKey(KeyCode.D);

        if (jumpKey) this.Jump();
        if (leftKey || rightKey) this.Move(leftKey);

        // Abilities input
        bool firstAbilityKey = Input.GetKeyDown(KeyCode.E);
        bool secondAbilityKey = Input.GetKeyDown(KeyCode.LeftShift);
        bool ultimateKey = Input.GetKeyDown(KeyCode.Q);

        if (firstAbilityKey) StartCoroutine(Ability1());
        if (secondAbilityKey) StartCoroutine(Ability2());
        if (ultimateKey) StartCoroutine(Ultimate());
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
    public abstract float GetUltCharge();
}