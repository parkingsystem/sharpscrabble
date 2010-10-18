﻿module Scrabble.Core.Config

open System
open Scrabble.Core.Squares

type Coordinate(x:int, y:int) = 
    member this.X with get() = x
    member this.Y with get() = y
    interface IComparable with  
         member this.CompareTo(o) = 
            let other = o :?> Coordinate
            let compareX = this.X.CompareTo(other.X)
            if compareX = 0 then
                this.Y.CompareTo(other.Y)
            else
                compareX
    override this.GetHashCode() =
        this.X.GetHashCode() + this.Y.GetHashCode()
    override this.Equals(o) =
        match o with
        | :? Coordinate as other -> this.X = other.X && this.Y = other.Y
        | _ -> false

type ScrabbleConfig() = 
    //I'm intentionally leaving out the 2 blank tiles for now.
    static member LetterQuantity : Map<char, int> = Map.ofList [ ('A', 9) ; ('B', 2) ; ('C', 2) ; ('D', 4) ; ('E', 12) ; ('F', 2) ; ('G', 3) ; ('H', 2) ; ('I', 9) ; ('J', 1) ; ('K', 1) ; ('L', 4) ; ('M', 2) ; ('N', 6) ; ('O', 8) ; ('P', 2) ; ('Q', 1) ; ('R', 6) ; ('S', 4) ; ('T', 6) ; ('U', 4) ; ('V', 2) ; ('W', 2) ; ('X', 1) ; ('Y', 2) ; ('Z', 1) ]
    static member MaxTiles : int = 7
    static member BoardLength : int = 15
    static member BoardLayout(c:Coordinate) =  // I hope I didn't fuck this up. This is left handed coordinates btw. Top left is (0, 0)
        match c.X, c.Y with
        | (0, 0) | (0, 7) | (0, 14) | (7, 0) | (14, 0) | (7, 14) | (14, 7) | (14, 14) -> TripleWordSquare() :> Square //wow I really have to "downcast" this to its base type? freakin lame.

        | (5, 1) | (9, 1) | (1, 5) | (5, 5) | (9, 5) | (13, 5) 
        | (1, 9) | (5, 9) | (9, 9) | (13, 9) | (5, 13) | (9, 13)  -> TripleLetterSquare() :> Square

        | (1, 1) | (2, 2) | (3, 3) | (4, 4) | (10, 4) | (11, 3) | (12, 2) | (13, 1) 
        | (1, 1) | (1, 1) | (1, 1) | (1, 1) | (1, 1) | (1, 1) | (1, 1) | (1, 1) -> DoubleWordSquare() :> Square

        | (3, 0) | (11, 0) | (6, 2) | (8, 2) | (0, 3) | (7, 3) | (14, 3) | (2, 6) | (6, 6) | (8, 6) | (12, 6) | (2, 7) | (11, 7)
        | (2, 8) | (6, 8) | (8, 8) | (12, 8) | (0, 11) | (7, 11) | (14, 11) | (6, 12) | (8, 12) | (3, 14) | (11, 14) -> DoubleLetterSquare() :> Square

        | (7, 7) -> StartSquare() :> Square

        | _ -> NormalSquare() :> Square