///summary
/*
Developers and Contributors: Ian Cahoon

Information
   Name: Snipe
   Type: Active
   Effect: Zoom in for AbilityTime(float), for the duration of the ability, your basic projectiles become hitscan
           Lower fire rate, higher damage, and a cool lightning zappy zap effect
   Tier: Rare
   Description: Basic fire turns into hitscan lightning bolts with increase damage but decreased fire rate

///summary

public class Snipe : Ability
{
   float abilityTime;
   bool onCooldown = false, CurrentlyActive = false;
   PlayerShoot pShoot;

   public override void OnAbilityAdd()
   {
       // Set name
       Name = "Snipe";
       Debug.Log(Name + " Added");

       // Add new shoot function to delegate
       pShoot = GetComponent<PlayerShoot>();
       if (pShoot)
       {
           Debug.Log("Snipe Added to Shoot Delegate");
           pShoot.shoot += OnShoot;
       }
   }
   public override void OnUpdate()
   {
   }
   public override void OnAbilityRemove()
   {
       // Remove shoot delegate
       if (pShoot)
       {
           pShoot.shoot -= OnShoot;
       }
       pShoot = null;

       // Call base function
       base.OnAbilityRemove();
   }
   public void OnShoot()
   {
       if (onCooldown)
       {
           return;
       }
       //Debug.Log("Raycast Shot");
       GameObject rayOrigin = GameObject.Find("Player/Gun"); //Needs to be changed to local player when networked
       Vector3 mp = Input.mousePosition;
       mp.z = 10;
       Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(mp);
       Vector3 targetVector = mouseLocation;

       Ray snipeBolt = Camera.main.ScreenPointToRay(Input.mousePosition);

       RaycastHit endPoint;

       if (Physics.Raycast(snipeBolt, out endPoint))
       {
           Debug.Log(endPoint.transform.gameObject.name);
           targetVector = endPoint.point;
       }
       //Debug.DrawRay(rayOrigin.transform.position, mouseLocation - rayOrigin.transform.position, Color.red, 5.0f);

       StartCoroutine(VisualizeRaycast(rayOrigin, targetVector));
   }
   IEnumerator VisualizeRaycast(GameObject Origin, Vector3 targetLocation)
   {
       onCooldown = true;

       LineRenderer snipeLaser = Origin.GetComponent<LineRenderer>();
       snipeLaser.SetPosition(0, Origin.transform.position);
       snipeLaser.SetPosition(1, targetLocation);

       snipeLaser.enabled = true;
       yield return new WaitForSeconds(.5f);
       snipeLaser.enabled = false;

       onCooldown = false;
   }
}
*/