namespace Platform.Fodels.Models

open System
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] User() =
    member this.Id
        with get () = this.id
        and set (value) = this.id <- value

    interface IEntityBase with
        member this.Id
            with get () = this.Id
            and set (value) = this.Id <- value

    public new(login: string, password: string, email: string) as newUser =
        User()
        then
            newUser.UpdateLogin login
            newUser.UpdatePassword password
            match newUser.UpdateEmail email with
            | Error err -> raise err
            | Ok _ -> ()

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private login: string
    member this.Login
        with get () = this.login
        and private set (value) = this.login <- value

    [<DefaultValue>]
    val mutable private email: string
    member this.Email
        with get () = this.email
        and private set (value) = this.email <- value

    [<DefaultValue>]
    val mutable private pass: string
    member this.Password
        with get () = this.pass
        and private set (value) = this.pass <- value

    member this.UpdateLogin(login: string) =
        if System.String.IsNullOrEmpty login then
            raise (new ArgumentException("Parameter must be set.", nameof login))
        else
            this.login <- login

    member this.UpdatePassword(password: string) =
        if System.String.IsNullOrEmpty password then
            raise (new ArgumentException("Parameter must be set.", nameof password))

        if password.IndexOf('.') = -1 then
            raise (new ArgumentException("Invalid password format. Ensure that you have hashed this password"))

        this.pass <- password

    member this.UpdateEmail(email: Email) =
        this.email <- email.ToString()

    member this.UpdateEmail(emailString: string): Result<bool, ArgumentException> =
            match Email.CreateEmail emailString with
            | Ok email -> (
                              this.email <- email.ToString()
                              Ok true
                          )
            | Error err -> Error err
