# AspNetCore_BlazorServer_Sample_Application
This Repo consists of basic sample for Blazor Server Application with Advance features implemented, this is Blazor Server side app implementation using sessionstorage

Authentication Flow


  -> Login 
  
    -> token generate store in session 
    
      -> BlazorAuthenticationStateProvider set AuthenticationState taken token from session 
      
        -> Validation through Authorize Attribute.

Features implemented in this Blazor Server sample application listed below.
1. Username, Password based Authentication.
2. Component
3. TwoWayBinding,
4. Bind,
5. Parameter, EventCallback, Attributes
6. IJSRuntime, JSInvokable, IJSObjectReference
7. DotNet, DotNetObjectReference
8. @Ref, ModelPopup
9. Routing, NavagationManager, NavLink, RouteParamter
10. EditForm, DataAnnotationsValidator, ValidationMessage, Bind-Value
11. Role Based Routing and Views
12. AuthenticationStateProvider
13. Dynamic Import js library and execute the method from C#
