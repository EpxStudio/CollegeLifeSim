using UnityEngine;
using System.Collections;

public class FountainScript : Entity
{
	public override void OnAwake()
	{
		base.OnAwake();

		isSolid = true;  // the npc is a solid object. player can't move through it

		if (Input.GetKey ("y")) 
		{
			int chance = Random.Range (1, 4);
			ChatController.Show ("Your wish was granted");

			if (chance == 1) {
				ChatController.Show ("Beware of shitty roommates, hide your food");
			} else if (chance == 2) {
				ChatController.Show ("Spend your money wisely, your parents will not give you more");
			} else if (chance == 3) {
				ChatController.Show ("If looking for an easier play through; limit semester hours"); 
			}

		}

		else 
		{
			ChatController.Show("Too Bad");
		}

	}

	public override void OnUpdate()
	{
		base.OnUpdate();
	}

	public override bool OnCollisionSolid(Entity other)
	{

		ChatController.Show ("Would you like to make a wish, y or n?");

			
		return true;

	}

}
