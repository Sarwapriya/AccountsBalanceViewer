import { LogLevel, Configuration, BrowserCacheLocation } from '@azure/msal-browser';

const isIE = window.navigator.userAgent.indexOf("MSIE ") > -1 || window.navigator.userAgent.indexOf("Trident/") > -1;
 
export const b2cPolicies = {
     names: {
         signUpSignIn: "B2C_1_SignUp_SignIn"
     },
     authorities: {
         signUpSignIn: {
             authority: "https://accountbalanceviewer.b2clogin.com/accountbalanceviewer.onmicrosoft.com/oauth2/v2.0/authorize?p=B2C_1_SignUp_SignIn&client_id=a8a5260b-8414-4bad-8704-1c83380c3f4d&nonce=defaultNonce&redirect_uri=https%3A%2F%2Fjwt.ms&scope=openid&response_type=code&prompt=login",
         }
     },
     authorityDomain: "deac6d4c-ac2a-4b5d-8e46-5f69dfb28e7b.b2clogin.com"
 };
 
 
export const msalConfig: Configuration = {
     auth: {
         clientId: '2af0aee7-087d-4ea5-a0c8-3302d04d5257',
         authority: "https://login.microsoftonline.com/48a02b7e-d1b8-4bcb-9608-a852da6e05ca",
         knownAuthorities: [b2cPolicies.authorityDomain],
         redirectUri: 'http://localhost:4200', 
     },
     cache: {
         cacheLocation: BrowserCacheLocation.LocalStorage,
         storeAuthStateInCookie: isIE, 
     },
     system: {
         loggerOptions: {
            loggerCallback: (logLevel, message, containsPii) => {
                console.log(message);
             },
             logLevel: LogLevel.Verbose,
             piiLoggingEnabled: false
         }
     }
 }

export const protectedResources = {
  todoListApi: {
    endpoint: "http://localhost:5000/api/todolist",
    scopes: ["https://accountbalanceviewer.onmicrosoft.com/a8a5260b-8414-4bad-8704-1c83380c3f4d/read"]
  },
}
export const loginRequest = {
  scopes: []
};