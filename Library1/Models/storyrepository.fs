namespace Library1.Repositories

open Microsoft.FSharp.Data.TypeProviders
open System.Data.Entity
open Library1.Models
open System.Linq 

type FsMvcAppEntities() =
    inherit DbContext("FsMvcAppExample")
    do Database.SetInitializer(new CreateDatabaseIfNotExists<FsMvcAppEntities>())
    [<DefaultValue()>] val mutable stories : IDbSet<Story>
    member x.Strories with get() = x.stories and set v = x.stories <- v

type StoriesRepository() =
    
    member x.GetAll () =
        use context = new FsMvcAppEntities()
        query { for g in context.stories do
                select g }
        |> Seq.toList
    

    member x.GetTop rowcount  =
        use context = new FsMvcAppEntities()
        query { for g in context.stories do
                take rowcount }
        |> Seq.toList


    member x.GetByID ID  =
        let mutable theStory = null
        use context = new FsMvcAppEntities()
        query { for g in context.stories do
                where (g.id = ID) }
        |> Seq.exactlyOne