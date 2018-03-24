using UnityEngine;

public class SlowMovement : Effect
{
    /// <summary>
    /// Amount to slow the player by as a decimal
    /// </summary>
    public float SlowAmount;

    #region Public Methods

    // Default constructor
    public SlowMovement()
    {
        SlowAmount = 0.3f;
        Lifetime = 5f;
        Name = "Slow Movement";
    }
    public SlowMovement(float slowAmount, float lifetime)
    {
        SlowAmount = slowAmount;
        Lifetime = lifetime;

        Name = "Slow Movement";
    }

    /// <summary>
    /// Copy constructor, used to copy abilities to a player
    /// </summary>
    /// <param name="jango"></param>
    public SlowMovement(SlowMovement jango)
    {
        SlowAmount = jango.SlowAmount;
        Debug.Log(jango + "cloned.");

        Lifetime = jango.Lifetime;
        TickTime = jango.TickTime;
        Debug.Log("Lifetime: " + jango.Lifetime + " | " + Lifetime);
        Debug.Log("Ticktime: " + jango.TickTime + " | " + TickTime);

        Name = "Slow Movement";
    }

    /// <summary>
    /// Copies the effect to a target. Must transfer all values over
    /// a copy constructor helps with this.
    /// </summary>
    /// <param name="target"></param>
    public override void ApplyEffect(GameObject target)
    {
        // Ref the player stat class
        PlayerStats ps = target.GetComponent<PlayerStats>();
        if (!ps)
        {
            Debug.LogError("No Player Stats class in target: " + target.name);
            return;
        }

        // Copy this effect to the target. A copy constructor helps with this
        SlowMovement slow = new SlowMovement(this);

        // The copy is added to the player
        ps.AddEffect(slow);
        // ps.AddEffect(this) // WRONG - will add the original not a copy
    }

    /// <summary>
    /// Slows the player when the ability is added to a player
    /// </summary>
    public override void Activate()
    {
        // Slow the target
        AddSlow();

        base.Activate();
    }

    public override void RemoveEffect()
    {
        ReverseSlow();
        // Call base to remove effect from player
        base.RemoveEffect();
    }

    #endregion Public Methods

    #region Private Methods

    // Private functions to add and reverse the slow effect
    private void AddSlow()
    {
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        if (ps == null)
        {
            Debug.LogError("Stats script not found. Unable to slow target");
            return;
        }
        if (SlowAmount >= 1)
        {
            ps.AddSpeedMultipler(Name, 1 / SlowAmount);
        }
        else ps.AddSpeedMultipler(Name, 1);
    }

    private void ReverseSlow()
    {
        // Reverse effect
        PlayerStats ps = Owner.GetComponent<PlayerStats>();
        if (ps == null)
        {
            Debug.LogError("Stats script not found. Unable to reverse slow");
            return;
        }
        float factor = 1 / SlowAmount; // Inverse of the slow percent
        if (factor < 100 && factor > 0.01)
            ps.RemoveSpeedMultiplier(Name);
    }

    #endregion Private Methods
}