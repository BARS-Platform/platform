namespace Platform.Fodels.Models

open System
open Platform.Fodels.Interfaces

type [<AllowNullLiteral>] User() =
    interface IPlatformModel with
        member this.Id
            with get () = this.id
            and set (value) = this.id <- value

    public new(login: string, password: string, email: string) as newUser =
        User()
        then
            newUser.UpdateLogin login
            newUser.UpdatePassword password
            newUser.UpdateEmail email

    [<DefaultValue>]
    val mutable private id: int

    [<DefaultValue>]
    val mutable private login: string
    member this.Login
        with get () = this.login
        and set (value) = this.login <- value

    [<DefaultValue>]
    val mutable private email: string
    member this.Email
        with get () = this.email
        and set (value) = this.email <- value

    [<DefaultValue>]
    val mutable private pass: string
    member this.Password
        with get () = this.pass
        and set value = this.pass <- value

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

    member this.UpdateEmail(email: string) =
        if System.String.IsNullOrEmpty email then
            raise (new ArgumentException("Parameter must be set.", nameof email))

        if email.IndexOf('@') = -1 || email.IndexOf('.') = -1 then
            raise (new ArgumentException(System.String.Format ("Parameter {0} must be of valid format: ***@***.**", nameof email)))

        this.email <- email
