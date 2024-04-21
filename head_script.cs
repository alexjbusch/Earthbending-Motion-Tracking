using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class head_script : MonoBehaviour
{
    public Material default_material;
    public Material red;

    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr= GetComponent<MeshRenderer>();
        default_material = mr.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.GetComponent<rock_script>() != null) { 
            rock_script rs = other.GetComponent<rock_script>();
            if (!rs.is_fragment) { 
                if (rs.team == "player2")
                {
                    StartCoroutine(TurnHeadRed());
                }
            }
        }
    }

    public IEnumerator TurnHeadRed()
    {
        mr.material = red;
        yield return new WaitForSeconds(1);
        mr.material = default_material;
    }
}
