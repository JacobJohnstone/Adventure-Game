using UnityEngine;

public class grabHealthPot : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //Instantiate text +5hp
            if(MainManager.instance.currentHP + 5 > MainManager.instance.maxHP)
            {
                MainManager.instance.currentHP = MainManager.instance.maxHP;
            } else
            {
                MainManager.instance.currentHP += 5;
            }

            Destroy(this.gameObject);
        }
    }

}
