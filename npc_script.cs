using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc_script : MonoBehaviour
{
    public float rock_lift_speed;
    public float lift_height;
    private bool is_earthbending;
    private Vector3 rock_spawn_position;
    private GameObject rock;
    


    void Start()
    {
        StartCoroutine(shoot_rock());
    }

    // Update is called once per frame
    void Update()
    {
        if (is_earthbending)
        {
            if (rock.transform.position.y < transform.position.y + lift_height)
            {
                rock.transform.Translate(0,rock_lift_speed*Time.deltaTime,0);
            } else
            {
                is_earthbending = false;
                FireRock();
                StartCoroutine(shoot_rock());
            }
        }
    }

    public IEnumerator shoot_rock()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        SpawnRock("rock2");
        is_earthbending = true;

    }

    private void SpawnRock(string rock_type)
    {
        rock = Instantiate(Resources.Load(rock_type)) as GameObject;
        rock.GetComponent<rock_script>().team = "player2";
        //Vector3 hand_position = body.instances[(int)Landmark.LEFT_WRIST].transform.position;
        //rock_spawn_position = new Vector3(hand_position.x, ground.transform.position.y, hand_position.z + 4);
        rock_spawn_position = transform.position + new Vector3(Random.Range(-20,20), 0, 4);
        rock.transform.position = rock_spawn_position;        
    }

    private void FireRock()
    {


        rock_script rs = rock.GetComponent<rock_script>();
        /*
        if (rs.target == null)
        {
            Destroy(rock);
            return;
        }
        */
        rs.target_location = new Vector3(rs.target.transform.position.x,
                                            rs.target.transform.position.y+4,
                                            rs.target.transform.position.z);
        rs.fired = true;

        rock.GetComponent<Rigidbody>().AddForce((rs.target.transform.position - rock.transform.position).normalized * rs.speed, ForceMode.Impulse);
        //rock.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -100), ForceMode.Impulse);
    }

}
