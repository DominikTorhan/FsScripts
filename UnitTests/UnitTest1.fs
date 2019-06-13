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
         
    [<Test>]
    member this.Test3 () =
        let str = "abc 
        sdf" 
        let strings = str.Split "\n" 
        Assert.AreEqual(2, strings.Length)
 
    [<Test>] 
    member this.Test4 ()=
        let seqA = seq ["ab"; "ab"; "bb";] 
        let seqB = seqA |> Seq.groupBy (fun s-> s)
        let len = Seq.length seqB
        Assert.AreEqual(2, len)