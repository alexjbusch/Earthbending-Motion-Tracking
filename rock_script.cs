using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock_script : MonoBehaviour
{
    public string team;
    public bool fired;
    public GameObject target;
    public Vector3 target_location;
    public float speed;

    private GameObject child_rock;
    private Rigidbody rb;

    public bool is_fragment;
    // Start is called before the first frame update
    void Start()
    {
        child_rock = GetComponentInChildren<Transform>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) {
            if (team == "player1")
            {
                target = GameObject.FindGameObjectWithTag("player2");
            }
            else if (team == "player2")
            {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("player1")) { 
                    if (g.GetComponent<head_script>() != null) {
                        target = g;
                    }
                }                
            }
        }

        if (fired)
        {
            //child_rock.transform.localEulerAngles += new Vector3(1, .2f, .5f);
            
            
        }
    }

    public IEnumerator Despawn(float seconds)
    {
        if (is_fragment)
        {
            yield return new WaitForSeconds(seconds);
            if (gameObject)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {



        if (is_fragment)
        {
            return;
        }
        else if (!is_fragment)
        {
            if (other.name.Contains("wall"))
            {
                Debug.Log(gameObject.tag + "   " + other.tag);
            }
            if (other.tag != team && (other.tag == "player1" || other.tag == "player2"))
            {

                if (other.gameObject.name == "LEFT_SHOULDER" || other.gameObject.name == "RIGHT_SHOULDER")
                {
                    if (team == "player2")
                    {
                        head_script hs = FindObjectOfType<head_script>();
                        hs.StartCoroutine(hs.TurnHeadRed());
                    }
                }

                for (int i = 0; i < 4; i++)
                {
                    GameObject fragment = Instantiate(gameObject);

                    rock_script fragment_rs = fragment.GetComponent<rock_script>();
                    fragment_rs.is_fragment = true;
                    fragment_rs.StartCoroutine(Despawn(4));
                    fragment.name = "fragment";
                    fragment.transform.localScale = new Vector3(.5f, .5f, .5f);
                    Rigidbody fragment_rb = fragment.GetComponent<Rigidbody>();
                    Vector3 oppositeDirection = -fragment_rb.velocity;
                    fragment_rb.velocity = Vector3.zero;
                    fragment_rb.AddForce(new Vector3 (Random.Range(-1000,1000), Random.Range(-1000, 1000), Random.Range(-1000, 1000)) + oppositeDirection);
                    fragment_rb.useGravity = true;
                
                    i++;
                }
                Destroy(gameObject);
            }
            
        }
    }
}
