namespace Library1.Repositories

//open Microsoft.FSharp.Data.TypeProviders
open System.Data.Entity
open Library1.Models
open System.Linq 
open System


type FsMvcAppEntities() =
    inherit DbContext("FsMvcAppExample")
    do Database.SetInitializer(new CreateDatabaseIfNotExists<FsMvcAppEntities>())
    
    [<DefaultValue()>] val mutable stories : IDbSet<Story>
    member x.Strories with get() = x.stories and set v = x.stories <- v

type StoriesRepository() =
    member x.Save theStory =
         use context = new FsMvcAppEntities()
//         let Story =
//            query { for g in context.stories do
//                    where (g.id = theStory.id) }
//
//            |> Seq.exactlyOne
        

        // context.Database.Log <- System.Diagnostics.Debug.WriteLine(s) <| s  //This line

         context.Entry(theStory).State <- EntityState.Modified

         let recordsAffected = context.SaveChanges()
         printfn "%d" recordsAffected  // will print 5
    
    member x.Add theStory =
         use context = new FsMvcAppEntities()
         context.stories.Add(theStory)
         context.SaveChanges()
             
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
        let Stories = 
            query { for g in context.stories do
                    where (g.id = ID) }
            |> Seq.toArray

        match Stories.Length with
        | 0 -> null
        | _ -> Stories.FirstOrDefault()