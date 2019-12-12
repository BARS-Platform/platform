namespace Platform.Fodels.Models
open System

type Email private () =

    [<DefaultValue>]
    val mutable private localPart: string

    [<DefaultValue>]
    val mutable private domain: string

    [<DefaultValue>]
    val mutable private locale: string

    static member CreateEmail(email: string): Result<Email, ArgumentException> =
            match email with
            | null -> Error(new ArgumentException("Parameter " + nameof (email) + " cannot be empty."))
            | "" -> Error(new ArgumentException("Parameter " + nameof (email) + " cannot be empty."))
            | _ -> (
                    let indexOfAtSign = email.LastIndexOf('@')
                    if (indexOfAtSign = -1) then Error(new ArgumentException("Email address must contain @ symbol."))
                    else if indexOfAtSign > 64 then Error(new ArgumentException("Part before @ cannot contain more that 64 symbols."))
                    else (
                        let localPart = email.Substring(0, indexOfAtSign).Replace(" ", "")
                        if System.String.IsNullOrEmpty(localPart) then Error(new ArgumentException("Local part cannot be empty."))
                        else (
                            let lastIndexOfDot = email.LastIndexOf('.')
                            if lastIndexOfDot = -1 then Error(new ArgumentException("Email address must contain . symbol."))
                            else (
                                let domain = email.Substring(indexOfAtSign + 1, lastIndexOfDot - indexOfAtSign - 1).Replace(" ", "")
                                if System.String.IsNullOrEmpty(domain) then Error(new ArgumentException("Domain cannot be empty."))
                                else (
                                    let locale = email.Substring(lastIndexOfDot + 1).Replace(" ", "")
                                    if System.String.IsNullOrEmpty(locale) then Error(new ArgumentException("Domain locale cannot be empty."))
                                    else Ok(new Email(localPart, domain, locale))
                                    )
                               )
                            )
                        )
                   )

    private new(localPart: string, domain: string, domainLocale: string) as newEmail =
        Email()
        then
            newEmail.localPart <- localPart
            newEmail.domain <- domain
            newEmail.locale <- domainLocale

    override __.ToString() =
        __.localPart + "@" + __.domain + "." + __.locale
