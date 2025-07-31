using UnityEngine;

public class grabSpellBook : MonoBehaviour
{
    public GameObject spellBookCanvas;
    public Transform textLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Instantiate text "You learned fireball!"
            Instantiate(spellBookCanvas, textLocation);
            MainManager.instance.foundSpellBook = true;
            Destroy(this.gameObject);
        }
    }

}
