using System;
using UnityEngine;
using XLua;



[LuaCallCSharp]
public class UsageTest : MonoBehaviour {

    // Use this for initialization
    public TextAsset luaScript;
    public LuaTable scriptEnv;
    public Action rotate;
    public Action move;
    private int speed = 0;
    float x=721/200, y=86/200, z=-433/200;
    public void OnMove()
    {
        x += 10;
    }
    public void OnSpeed()
    {
        speed += 10;
    }
    public int cubeType;
    internal  static LuaEnv luaenv = new LuaEnv();
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 
    void Awake () {
        //新建luaEnv:lua全局环境对象；scriptEnv:关联对象
       
        scriptEnv = luaenv.NewTable();

        //这里是设置元表 指定了索引为Global属性 Global属性在luaenv构造时被设置 
        LuaTable meta = luaenv.NewTable();
        meta.Set("__index", luaenv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();//设置完元表是可以销毁的

      

        //执行lua脚本。如果要进行关联，必须先行进行dostring,否则空指针
        luaenv.DoString(luaScript.text, "luaPart", scriptEnv);


        //进行关联,放在dostring后面 使得函数先被声明 
        scriptEnv.Set("self", this);
        scriptEnv.Set("x", x);
        scriptEnv.Set("y", y);
        scriptEnv.Set("z", z);
        scriptEnv.Set("speed", speed);
        scriptEnv.Get("rotate", out rotate);
        scriptEnv.Get("move", out move);

        

     
    }
	
	// Update is called once per frame
	void Update () {
        if(cubeType==0)
        {
            rotate();
            if (Time.time - LuaBehaviour.lastGCTime > GCInterval)//旋转的计时功能
            {
                luaenv.Tick();
                LuaBehaviour.lastGCTime = Time.time;
            }
        }
        if(cubeType==1)
        {
            move();
        }
    }
}
