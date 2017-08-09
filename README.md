# Login with Embedded Lock

This example is based on Auth0's example [here](https://github.com/auth0-samples/auth0-aspnetcore-mvc-samples/tree/master/Samples/embedded-lock)

> **Please note** that Auth0 recommends that you use the hosted version of Lock, rather than the embedded version.

## Background

When using the normal OIDC middleware, when a user wants to log in and the middleware is called, the user will be redirected to the Auth0 website to sign in using the hosted version of Lock. This may not be the user experience you are looking for. You may for example want to embed Lock inside your application so it has more of the look-and-feel of your own application. In this instance, you can use both Lock and the OIDC middleware together, but it requires a bit of extra work on your side.

Normally when the OIDC middleware initiates the 1st leg of the authentication, it will send along information contained in `state` and `nonce` parameters. After the user has authenticated and Auth0 redirects back to the redirect URL inside your application, in will pass back this `state` and `nonce` parameters. The OIDC middleware is going to pick up that callback to the redirect URL because it will need to exchange the `code` for an `access_token`. It will, however, validate the `state` and `nonce` parameters to protect against CSRF.

This poses a problem. When you embed Lock in your application, the OIDC middleware is not initiating the 1st leg of the OAuth flow. Instead, the embedded Lock widget is initiating that first step.

You will therefore need to construct correct `state` and `nonce` parameters (as if the OIDC middleware did it so that it can validate it correctly), and then be sure to specify the `state` and `nonce` parameters on Lock so that Auth0 can send back the correct values for these parameters after the user has authenticated.
