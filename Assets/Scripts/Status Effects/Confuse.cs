using UnityEngine;

public class Confuse : Effect
{
    private PlayerController pc = new PlayerController();

    public Confuse()
    {
    }
    public Confuse(float lifetime)
    {
        Lifetime = lifetime;
    }

    public override void Activate()
    {
        Debug.Log("Inverting Controls");
        pc.InvertX = true;
        pc.InvertY = true;
    }

    public override void ApplyEffect(GameObject target)
    {
        // Ref the player stat class
        PlayerStats ps = target.GetComponent<PlayerStats>();
        if (!ps)
        {
            Debug.LogError("No Player Stats class in target: " + target);
            return;
        }

        // Apply the effect
        Confuse confused = new Confuse(Lifetime);
        ps.AddEffect(confused);
    }

    public override void RemoveEffect()
    {
        pc.InvertX = false;
        pc.InvertY = false;

        base.RemoveEffect();
    }
}