
namespace Powerups
{
    public abstract class PassiveAbility : BaseAbility
    {
        /* *** IMPORTANT ****
         * All passive abilities should derive from this class instead of BaseAbility.
         * There is no extra code yet, but it provides necessary seperation between 
         * actives and passives for the ability manager. Base abilities are not added to
         * the player. 
        */
    }
}