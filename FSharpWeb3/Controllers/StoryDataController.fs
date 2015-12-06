namespace FSharpWeb3.Controllers
open System
open System.Collections.Generic
open System.Linq
open System.Net.Http
open System.Web.Http
open Library1.Repositories
open Library1.Models

/// Retrieves values.
[<RoutePrefix("api2/Story")>]
type StoryDataController(repository : StoriesRepository) =
    inherit ApiController()
    new() = new StoryDataController(StoriesRepository())

    

    /// Gets all values.
    [<Route("")>]
    member x.Get() = repository.GetTop(6)


    /// Gets the value with index id.
    [<Route("{id:int}")>]
    member x.Get(id : int) : IHttpActionResult =
//        if id > values.Length - 1 then
//            x.BadRequest() :> _
//        else x.Ok(values.[id]) :> _
        let result = repository.GetPage(id)
        x.Ok(result) :> _