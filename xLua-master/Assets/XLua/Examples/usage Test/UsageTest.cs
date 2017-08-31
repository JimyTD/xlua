using System;
using UnityEngine;
using XLua;




public class UsageTest : MonoBehaviour {

    // Use this for initialization
    public TextAsset luaScript;
    public LuaTable scriptEnv;
    public Action rotate;
    public Action move;
    void Start () {
        //新建luaEnv:lua全局环境对象；scriptEnv:关联对象
        LuaEnv luaenv = new LuaEnv();
        scriptEnv = luaenv.NewTable();

        //这里是设置元表 指定了索引为Global属性 Global属性在luaenv构造时被设置 
        LuaTable meta = luaenv.NewTable();
        meta.Set("__index", luaenv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();//设置完元表是可以销毁的

        //进行关联
        scriptEnv.Set("self", this);
        scriptEnv.Get("rotate", out rotate);
        scriptEnv.Get("move", out move);

        //执行lua脚本
        luaenv.DoString(luaScript.text, "lua", scriptEnv);



        luaenv.Dispose();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
