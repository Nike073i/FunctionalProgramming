open System.IO
open System.Text.RegularExpressions
open System.Text

let getSumDigitInDegree (n : int) =
    2.0 ** n |> bigint |> string|> Seq.map(string >> System.Int32.Parse) |> Seq.sum

let lab1() =
    let inputVal = System.Console.ReadLine()
    let intVal = System.Int32.Parse(inputVal)
    let result = getSumDigitInDegree(intVal)
    printf "%d"  result

let getMostFrequentLetter(text : string) =
    let letters = [|'а' .. 'я'|]
    let string2arr = text.ToLower().ToCharArray()
    let count pred = Array.filter pred >> Array.length
    letters |> Array.map(fun ch -> ch, count ((=)ch) string2arr) 
            |> Array.maxBy (fun (_,n) -> n)

let getPopularLetter(path : string) = 
    let text = File.ReadAllText(path, Encoding.UTF8)
    let words = Regex.Matches(text, "(\w+-\w+)|(\w+)") |> Seq.map(fun matchWord -> matchWord.Value) 
    let every3rd (tuple : int * string) = 
        match fst tuple%3 with
        | 0 -> Some(snd tuple)
        | _ -> None
    let every3rdWords = words |> Seq.indexed 
                              |> Seq.choose every3rd
    String.concat "" every3rdWords |> getMostFrequentLetter
    
let lab2() =
    let popularLetter = getPopularLetter(@"T:\Study\VisualStudioRepos\FunctionalProgramming\FunctionalProgramming\master_text.txt")  
    let letter = fst popularLetter
    let count = snd popularLetter
    printf "Символ %c является самым частым. Он встретился %d раз(а)" letter count

let arrayManipulation (a : array<int>) =
    printf "Начальные данные : %A \n" a
    for i = Array.length a - 2 downto 0 do
        Array.set a i (a[i] + a[i+1])
    printf "Результат : %A \n" a
    0

let lab3() = 
    let a1 = [|3;2;1;0;4|]
    let test_a1 = arrayManipulation a1
    let a2 = [|7;2;9;10;3;4|] // [35 28 26 17 7 4]
    arrayManipulation a2


//let lab5()=
//    let filePaths = [|"path1"; "path2"; "path3"|]
//    let res = Array.Parallel.iter getPopularLetter filePaths
//    0


[<EntryPoint>]
let main(args : string[]) = 
    lab3()