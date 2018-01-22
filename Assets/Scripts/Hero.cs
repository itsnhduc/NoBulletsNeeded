using UnityEngine;

public abstract class Hero : MonoBehaviour {

    void FixedUpdate()
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

        if (firstAbilityKey) this.Ability1();
        if (secondAbilityKey) this.Ability2();
        if (ultimateKey) this.Ultimate();
    }

    // Movements behaviors
    protected abstract void Jump();
    protected abstract void Move(bool isLeft);

    // Abilities behaviors
    protected abstract void Ability1(bool mouseLeft = false);
    protected abstract void Ability2(bool mouseLeft = false);
    protected abstract void Ultimate(bool mouseLeft = false);
}