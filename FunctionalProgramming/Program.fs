open System
[<EntryPoint>]
let main (args: string[]) = 
    if args.Length <> 2 then
        failwith "Error: expected 2 arguments"
    let greeting, thing = args[0], args[1]
    let timeOfDay = DateTime.Now.ToString("hh:mm tt")
    printf "%s, %s at %s" greeting thing timeOfDay
    0