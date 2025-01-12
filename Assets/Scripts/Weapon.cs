using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* base class for all weapons in the game */

//[CreateAssetMenu(menuName = "Weapon")]
public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float attackPower;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        //if (player == null)
           // Debug.Log("Player null");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            //player collects weapon; the weapon disappears.

            //place weapon in robot model's hands
            //transform.position = collision.transform.position;
            collision.GetComponent<Player>().shotgun.gameObject.SetActive(true);
            collision.GetComponent<Player>().weaponPickedUp = true;
            //player.weaponPickedUp = true;
            Debug.Log("touched player");
            Destroy(gameObject);    //should not actually destroy this. Want to avoid GC as much as possible.
        }
    }
}
