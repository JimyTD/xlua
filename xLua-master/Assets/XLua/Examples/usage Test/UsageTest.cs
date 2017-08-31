using System;
using UnityEngine;
using XLua;




public class UsageTest : MonoBehaviour {

    // Use this for initialization
    TextAsset luaScript;
    LuaTable scriptEnv;
    Action rotate;
    Action move;
    void Start () {
        LuaEnv luaenv = new LuaEnv();
        scriptEnv = luaenv.NewTable();
        scriptEnv.Set("self", this);
        luaenv.DoString(luaScript.text, "lua",scriptEnv);
        scriptEnv.Get("rotate", out rotate);
        scriptEnv.Get("move", out move);

        luaenv.Dispose();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
