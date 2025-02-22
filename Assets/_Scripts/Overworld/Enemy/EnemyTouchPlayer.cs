using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyTouchPlayer : MonoBehaviour
{
    [SerializeField]
    EnemyPatrol enemyPatrol;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            enemyPatrol.HasTouchPlayer = true;
            collision.gameObject.TryGetComponent<PlayerOverworldController>(out var controller);
            if (controller == null)
            {
                collision.gameObject.TryGetComponent<FollowPlayer>(out var followPlayer);
                followPlayer.player.enabled = false;
            }
            else
            {
                controller.enabled = false;
            }
            GoToCombat();
        }
    }

    //Pour Si jamais on veut lancer un animation ou pas et mettre des effets
    public async void GoToCombat()
    {
        await Task.Delay(3000);
        SceneManager.LoadScene("SceneMatéo");
    }
}
