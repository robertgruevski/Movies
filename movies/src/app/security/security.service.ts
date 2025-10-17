import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { AuthenticationResponseDTO, UserCredentialsDTO } from './security.models';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SecurityService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/users';
  private readonly keyToken = 'token';
  private readonly keyExpiration = 'token-expiration';

  register(credentials: UserCredentialsDTO): Observable<AuthenticationResponseDTO> {
    return this.http
      .post<AuthenticationResponseDTO>(`${this.baseUrl}/register`, credentials)
      .pipe(tap((authenticationResponse) => this.storeToken(authenticationResponse)));
  }

  login(credentials: UserCredentialsDTO): Observable<AuthenticationResponseDTO> {
    return this.http
      .post<AuthenticationResponseDTO>(`${this.baseUrl}/login`, credentials)
      .pipe(tap((authenticationResponse) => this.storeToken(authenticationResponse)));
  }

  storeToken(authenticationResponse: AuthenticationResponseDTO) {
    localStorage.setItem(this.keyToken, authenticationResponse.token);
    localStorage.setItem(this.keyExpiration, authenticationResponse.expiration.toString());
  }

  isLoggedIn(): boolean {
    const token = this.getJWTToken();

    if (!token) return false;

    const expiration = localStorage.getItem(this.keyExpiration)!;
    const expirationDate = new Date(expiration);

    if (expirationDate <= new Date()) return false;

    return true;
  }

  getJWTClaim(field: string): string {
    const token = this.getJWTToken();
    if (!token) return '';
    const dataToken = JSON.parse(atob(token.split('.')[1]));
    return dataToken[field];
  }

  logout() {
    localStorage.removeItem(this.keyToken);
    localStorage.removeItem(this.keyExpiration);
  }

  getJWTToken(): string | null {
    return localStorage.getItem(this.keyToken);
  }

  getRole(): string {
    return '';
  }
}
