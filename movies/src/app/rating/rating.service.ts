import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class RatingService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/ratings';

  rate(movieId: number, rate: number) {
    return this.http.post(this.baseUrl, { movieId, rate });
  }
}
