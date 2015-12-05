namespace Library1.Models
open System.ComponentModel.DataAnnotations

type Story () = 
    [<Key>] member val id = 1 with get, set
    [<Required>] member val title = "" with get, set
    [<Required>] member val body = "" with get, set