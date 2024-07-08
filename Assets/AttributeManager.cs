using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AttributeManager : MonoBehaviour
{
    //attributes track which keys you have collected
    static public int MAGIC = 16;
    static public int INTELLIGENCE = 8;
    static public int CHARISMA = 4;
    static public int FLY = 2;
    static public int INVISIBLE = 1;
    static public int GOLD = 32;

    public Text attributeDisplay;
    public int attributes = 0;

    private void OnTriggerEnter(Collider other)
    {
        //Keys (add value to attributes + destroy key gameobject)
        if(other.gameObject.tag == "MAGIC")
        {
            attributes |= MAGIC;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "INTELLIGENCE")
        {
            attributes |= INTELLIGENCE;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "CHARISMA")
        {
            attributes |= CHARISMA;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "FLY")
        {
            attributes |= FLY;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "INVISIBLE")
        {
            attributes |= INVISIBLE;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "ANTIMAGIC")
        {
            attributes &= ~MAGIC;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "REMOVE")
        {
            attributes &= ~ (INTELLIGENCE | MAGIC);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "ADD")
        {
            attributes |= (INTELLIGENCE | MAGIC | CHARISMA);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "RESET")
        {
            attributes = 0;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "GOLD")
        {
            attributes |= GOLD;
            Destroy(other.gameObject);
        }
        //Doors (remove key value from attributes, then disable solid collider)
        else if (other.gameObject.tag == "MAGIC_DOOR")
        {
            if ((attributes & MAGIC) == MAGIC)
            {
                attributes &= ~MAGIC;
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
            else if (CheckGold())
            {
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
                return;
            }
        }
        else if (other.gameObject.tag == "INTELLIGENCE_DOOR")
        {
            if ((attributes & INTELLIGENCE) == INTELLIGENCE)
            {
                attributes &= ~INTELLIGENCE;
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
            else if (CheckGold())
            {
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
                return;
            }
        }
        else if (other.gameObject.tag == "CHARISMA_DOOR")
        {
            if ((attributes & CHARISMA) == CHARISMA)
            {
                attributes &= ~CHARISMA;
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
            else if (CheckGold())
            {
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
                return;
            }
        }
        else if (other.gameObject.tag == "FLY_DOOR")
        {
            if ((attributes & FLY) == FLY)
            {
                attributes &= ~FLY;
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
            else if (CheckGold())
            {
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
                return;
            }
        }
        else if (other.gameObject.tag == "INVISIBLE_DOOR")
        {
            if ((attributes & INVISIBLE) == INVISIBLE)
            {
                attributes &= ~INVISIBLE;
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
            else if (CheckGold())
            {
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
                return;
            }
        }
        //Double door (needs both fly and charisma keys OR gold key)
        else if (other.gameObject.tag == "FLY_CHARISMA_DOOR")
        {
            if ((attributes & (FLY + CHARISMA)) == (FLY + CHARISMA))
            {
                attributes &= ~(FLY + CHARISMA);
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
            }
            else if (CheckGold())
            {
                other.transform.parent.GetComponent<BoxCollider>().enabled = false;
                return;
            }
        }
    }
    //reenables door collider after exiting
    private void OnTriggerExit(Collider other)
    {
        if(other.transform.parent.GetComponent<BoxCollider>() != null)
        {
            other.transform.parent.GetComponent<BoxCollider>().enabled = true;
        }
    }
    //Checks if you have the gold key + removes key
    private bool CheckGold()
    {
        if ((attributes & GOLD) == GOLD)
        {
            attributes &= ~GOLD;
            return true;
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
        attributeDisplay.transform.position = screenPoint + new Vector3(0,-50,0);
        attributeDisplay.text = Convert.ToString(attributes, 2).PadLeft(8, '0');
    }
       
}
