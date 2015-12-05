namespace FSharpWeb3.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax
open Library1.Repositories


[<HandleError>]
type HomeController(repository : StoriesRepository) =
    inherit Controller()
    new() = new HomeController(StoriesRepository())
    member this.Index () =
        repository.GetTop(6)
        |> this.View

    member this.Edit (id) =
        repository.GetByID(id)
        |> this.View