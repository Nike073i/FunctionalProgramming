﻿module Graphics

open System.Drawing
type Rule = char * char list
type Grammar = Rule list
let FindSubst c (gr:Grammar) = 
   match List.tryFind (fun (x,S) -> x=c) gr with
     | Some(x,S) -> S
     | None -> [c]
let Apply (gr:Grammar) L =
   List.collect (fun c -> FindSubst c gr) L
let rec NApply n gr L = 
   if n>0 then Apply gr (NApply (n-1) gr L)
   else L
let TurtleBitmapVisualizer n delta cmd =
    let W,H = 1600,1600
    let b = new Bitmap(W,H)
    let g = Graphics.FromImage(b)
    let pen = new Pen(Color.Black)
    let NewCoord (x:float) (y:float) phi =
       let nx = x+n*cos(phi)
       let ny = y+n*sin(phi)
       (nx,ny,phi)
    let ProcessCommand x y phi = function
     | 'f' -> NewCoord x y phi
     | '+' -> (x,y,phi+delta)
     | '-' -> (x,y,phi-delta)
     | 'F' -> 
         let (nx,ny,phi) = NewCoord x y phi
         g.DrawLine(pen,(float32)x,(float32)y,(float32)nx,(float32)ny)
         // printfn "Drawing (%A,%A) -> (%A,%A) [phi=%A]" x y nx ny phi
         (nx,ny,phi)
     | _ -> (x,y,phi)     
    let rec draw x y phi = function
     | [] -> ()
     | h::t ->
         let (nx,ny,nphi) = ProcessCommand x y phi h
         draw nx ny nphi t
    draw (float(W)/2.0) (float(H)/2.0) 0. cmd
    b

let toString (xs:char list) =
    let sb = System.Text.StringBuilder(xs.Length)
    xs |> List.iter (sb.Append >> ignore)
    sb.ToString()

let str (s:string) = s.ToCharArray() |> List.ofArray

