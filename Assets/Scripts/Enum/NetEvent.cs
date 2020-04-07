using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetEvent {
    //Common
    //Server
    public const int ALL_REQUEST = 101;
    public const int SINGLE_REQUEST = 102;
    //Client
    public const int REQUEST_MOVE = 301;
    public const int REQUEST_ATTACK = 302;
    public const int REGISTER_CLIENT = 303;
}
