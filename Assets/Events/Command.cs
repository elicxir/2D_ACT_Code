using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command : EventCommand
{
    [SerializeField] EventCommand NextCommand;
}
