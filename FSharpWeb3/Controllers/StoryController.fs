namespace FSharpWeb3.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Web
open System.Web.Mvc
open System.Web.Mvc.Ajax
open Library1.Repositories
open Library1.Models

[<HandleError>]
type StoryController(repository : StoriesRepository) =
    inherit Controller()
    new() = new StoryController(StoriesRepository())
    member this.Index (id) =
        repository.GetByID(id)
        |> this.View

    member this.Edit (id) =
        repository.GetByID(id)
        |> this.View

    

    [<HttpPost>]
    member this.Create (newStory : Story) : ActionResult =
        match base.ModelState.IsValid with
        | false ->
            upcast this.View newStory
        // … Code to persist the data will be added later
        | true -> //upcast base.RedirectToAction("Index")
            let mutable theStory = repository.GetByID(newStory.id)
            let mutable p = new Story()
            match theStory with
                | null -> 
                
                    p.title <- newStory.title
                    p.body <- newStory.body
                    repository.Add(p)
                    p = p
                | _ -> 
                    repository.Save(newStory)
                    theStory = theStory
        

            upcast base.RedirectToRoute("home")