using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseScene 
{
    //Save all resource before Dispose
    public void DisposeScene();
    //Load all resource during init
    public void SetUp();
}
