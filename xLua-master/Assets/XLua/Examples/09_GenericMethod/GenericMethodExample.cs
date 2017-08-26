using UnityEngine;
using XLua;

//类方法的说明，说明了哪些泛型方法可以生成lua方法

public class GenericMethodExample : MonoBehaviour
{
    //首先连接了两个子类，目的是可以使用：运算符将自己作为static函数的首个参数
    //之后测试了各个类型的泛型方法，并且输出了泛型实现的接口。
    //泛型函数要有要求，这个foo.cs有说
    private const string script = @"
        local foo1 = CS.Foo1Child()
        local foo2 = CS.Foo2Child()

        local obj = CS.UnityEngine.GameObject()
        foo1:PlainExtension()
        foo1:Extension1()
        foo1:Extension2(obj) -- overload1
        foo1:Extension2(foo2) -- overload2
        
        local foo = CS.Foo()
        foo:Test1(foo1)
        foo:Test2(foo1,foo2,obj)
";
    private LuaEnv env;

    private void Start()
    {
        env = new LuaEnv();
        env.DoString(script);
    }

    private void Update()
    {
        if (env != null)
            env.Tick();
    }

    private void OnDestroy()
    {
        env.Dispose();
    }
}