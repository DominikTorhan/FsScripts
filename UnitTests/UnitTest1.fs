namespace Tests

open NUnit.Framework

[<TestClass>]
type TestClass () =

    [<SetUp>]
    member this.Setup () =
        ()

    //[<Test>]
    //member this.Test1 () =
    //    Assert.Pass()
          
    [<Test>]
    member this.Test2 () =
        let str = "abc sdf" 
        let strings = str.Split " " 
        Assert.AreEqual(2, strings.Length)
