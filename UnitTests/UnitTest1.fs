namespace Tests

open NUnit.Framework
open System

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
        let str = "abc  sdf  " 
        let strings = str.Split([|' '|], StringSplitOptions.RemoveEmptyEntries)
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
 
    [<Test>] 
    member this.Test5 ()=
        let seqA = seq ["ab"; "ab"; "bb";] 
        let len = seqA |> Seq.distinct |> Seq.length
        Assert.AreEqual(2, len)

    [<Test>] 
    member this.Test6 () =
        let str = "1line \n 2line\n\n4line" 
        let seq = str.Split "\n" |> Array.toSeq 
        let seq' = seq |> Seq.filter (fun s -> s <> "")
        Assert.AreEqual(3, seq' |> Seq.length)
 
 
    [<Test>] 
    member this.Test7 () =
        let str = " 1line \n 2line   \n\n4line" 
        let seq = str.Split "\n" |> Array.toSeq 
        let seq' = seq |> Seq.filter (fun s -> s <> "") |> Seq.map (fun s -> s.Trim() + " -") 
        let strNew = seq' |> String.concat " " 
        Assert.AreEqual("1line - 2line - 4line -", strNew)
         
    //[<Test>] 
    //member this.Test8 () =
    //    let str = "AAA, bb. BB-aaa    \n bb Aaa bb 1234065(bb) "