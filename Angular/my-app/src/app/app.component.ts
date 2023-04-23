import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Inject } from '@angular/core';
import { MsalService, MsalBroadcastService, MSAL_GUARD_CONFIG, MsalGuardConfiguration } from '@azure/msal-angular';
import { InteractionStatus, RedirectRequest } from '@azure/msal-browser';
import { Subject } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  title = 'my-app';
  loginDisplay = false;
  private readonly _destroying$ = new Subject<void>();

  constructor(@Inject(MSAL_GUARD_CONFIG) private msalGuardConfig: MsalGuardConfiguration, private broadcastService: MsalBroadcastService, private authService: MsalService, private http: HttpClient, @Inject('API') private baseUrl: string) { }

  email: string = '';
  name?: string = '';
  private readonly TOKEN_KEY = 'token';
  id_token?: string | undefined;
  ngOnInit() {

    this.broadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None),
        takeUntil(this._destroying$)
      )
      .subscribe(() => {
        this.setLoginDisplay();
      })
  }

  login() {
    if (this.msalGuardConfig.authRequest) {
      this.authService.loginRedirect({ ...this.msalGuardConfig.authRequest } as RedirectRequest);
    } else {
      this.authService.loginRedirect();
    }
  }

  logout() {
    this.authService.logoutRedirect({
      //postLogoutRedirectUri: 'http://localhost:4200'
      //postLogoutRedirectUri: 'https://proud-sand-058d45900.3.azurestaticapps.net/view'
      postLogoutRedirectUri: environment.postLogoutUrl
    });
  }

  setLoginDisplay() {
    console.log(this.authService.instance.getAllAccounts());
    this.email = this.authService.instance.getAllAccounts()[0].username;
    this.name = this.authService.instance.getAllAccounts()[0].name;
    this.id_token = this.authService.instance.getAllAccounts()[0].idToken;
    const url = `${this.baseUrl}api/user/get-user?Email=${this.email}&Name=${this.name}`;
    this.http.get<any[]>(url).subscribe(
    );
    this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
  }

  ngOnDestroy(): void {
    this._destroying$.next(undefined);
    this._destroying$.complete();
  }
  /* Changes end here. */
}
