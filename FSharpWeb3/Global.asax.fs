namespace FSharpWeb3

open System
open System.Net.Http
open System.Web
open System.Web.Http
open System.Web.Mvc
open System.Web.Routing
open System.Web.Optimization

type BundleConfig() =
    static member RegisterBundles (bundles:BundleCollection) =

        //const string ANGULAR_APP_ROOT = "~/Client/scripts/app/"
        //const string VIRTUAL_BUNDLE_PATH = ANGULAR_APP_ROOT + "main.js"


        bundles.Add(ScriptBundle("~/bundles/jquery").Include([|"~/Scripts/jquery-{version}.js"|]))
        bundles.Add(ScriptBundle("~/bundles/angular")
            .Include([|"~/Scripts/Angular/angular.js"; "~/Scripts/Angular/angular-resource.js"; "~/Scripts/Angular/angular-route.js"
            ; "~/Scripts/Angular/angular-cookies.js"
            |])

            .IncludeDirectory("~/Scripts/app", "*.js", true)
        )
        

        // Use the development version of Modernizr to develop with and learn from. Then, when you're
        // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(ScriptBundle("~/bundles/modernizr").Include([|"~/Scripts/modernizr-*"|]))

        bundles.Add(ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"))

        bundles.Add(StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css"))

/// Route for ASP.NET MVC applications
type Route = { 
    controller : string
    action : string
    id : UrlParameter }

type HttpRoute = {
    controller : string
    id : RouteParameter }

type Global() =
    inherit System.Web.HttpApplication() 
    
    let mutable timeBegin = System.DateTime.Now
    let mutable timeEnd = System.DateTime.Now

    static member RegisterWebApi(config: HttpConfiguration) =
        // Configure routing
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            "DefaultApi", // Route name
            "api/{controller}/{id}", // URL with parameters
            { controller = "{controller}"; id = RouteParameter.Optional } // Parameter defaults
        ) |> ignore

        // Configure serialization
        config.Formatters.XmlFormatter.UseXmlSerializer <- true
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver <- Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()

        // Additional Web API settings

    static member RegisterFilters(filters: GlobalFilterCollection) =
        filters.Add(new HandleErrorAttribute())



    static member RegisterRoutes(routes:RouteCollection) =
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.MapRoute(
            "Default", // Route name
            "{controller}/{action}/{id}", // URL with parameters
            { controller = "Home"; action = "Index"; id = UrlParameter.Optional } // Parameter defaults
        ) |> ignore

    member x.Application_Start() =
        AreaRegistration.RegisterAllAreas()
        GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
        Global.RegisterFilters(GlobalFilters.Filters)
        Global.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles BundleTable.Bundles


    member x.Application_BeginRequest() =
        //let context = base.Context
        //let response = context.Response
        //response.Write("Page viewed")
        //response.End()
        timeBegin <- System.DateTime.Now
    
    member x.ReplyBuild( y : int, z : string) = 
        match y with
        | 500 -> ""
        | _ ->
            match z with
            | "application/json; charset=utf-8" -> ""
            | _ -> "Time begin: " + timeBegin.ToString("HH.mm.ss.ffffff") +  " Time End: " + timeEnd.ToString("HH.mm.ss.ffffff") 

    member x.Application_EndRequest() =
        let context = base.Context
        let response = context.Response
        timeEnd <- System.DateTime.Now
        
        response.Write(x.ReplyBuild(response.StatusCode, response.ContentType))
        //response.Write("Time begin: " + timeBegin.ToString("hh.mm.ss.ffffff") +  " Time End: " + timeEnd.ToString("hh.mm.ss.ffffff"))
        response.End()
        