module Scripts

    /// Get all files under a given folder
    open System.IO
    
    let rec allFilesUnder baseFolder = 
        seq {
            yield! Directory.GetFiles(baseFolder)
            for subDir in Directory.GetDirectories(baseFolder) do
                yield! allFilesUnder subDir 
            }
        
    /// Active Pattern for determining file extension
    let (|EndsWith|_|) extension (file : string) = 
        if file.EndsWith(extension) 
        then Some() 
        else None
    
    /// Shell executing a program
    open System.Diagnostics
    
    let shellExecute program args =
        let startInfo = new ProcessStartInfo()
        startInfo.FileName <- program
        startInfo.Arguments <- args
        startInfo.UseShellExecute <- true
    
        let proc = Process.Start(startInfo)
        proc.WaitForExit()
        ()
    
        // Launches all .fs and .fsi files under the current folder in Notepad
    open System
    
    allFilesUnder Environment.CurrentDirectory
    |> Seq.filter (function 
                | EndsWith ".fs" _
                | EndsWith ".fsi" _
                    -> true
                | _ -> false)
    |> Seq.iter (shellExecute "Notepad.exe")
    
    
    
    