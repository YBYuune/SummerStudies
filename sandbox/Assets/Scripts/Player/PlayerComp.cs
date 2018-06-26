using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComp : MonoBehaviour {

    public const short c_ATTACKING = 0x0001;
    public const short c_MOUNTED   = 0x0002;

    public short playerFlags = 0x0;
}
