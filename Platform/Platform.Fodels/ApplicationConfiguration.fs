namespace Platform.Fodels

open Microsoft.Extensions.Configuration
open System

type ApplicationConfiguration() =
    let envrConString = Environment.GetEnvironmentVariable("CONN_STRING")
    let envr = System.String.IsNullOrEmpty(envrConString)
    member __.Configuration = (new ConfigurationBuilder()).AddJsonFile("appconfig.json").Build()
    member __.Database = __.Configuration.GetSection("Database")
    member __.ConnectionString =
        if envr then __.Database.Item("ConnectionString")
        else Environment.GetEnvironmentVariable("CONN_STRING")
