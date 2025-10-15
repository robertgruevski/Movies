import { inject, Injectable } from '@angular/core';
import { GenreCreationDTO, GenreDTO } from './genres.models';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { PaginationDTO } from '../shared/models/PaginationDTO';
import { buildQueryParams } from '../shared/functions/buildQueryParams';

@Injectable({
  providedIn: 'root',
})
export class GenresService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;

  public getPaginated(pagination: PaginationDTO): Observable<HttpResponse<GenreDTO[]>> {
    let queryParams = buildQueryParams(pagination);
    return this.http.get<GenreDTO[]>(this.baseUrl + '/genres', {
      params: queryParams,
      observe: 'response',
    });
  }

  public getById(id: number): Observable<GenreDTO>{
    return this.http.get<GenreDTO>(`${this.baseUrl}/genres/${id}`);
  }

  public create(genre: GenreCreationDTO) {
    return this.http.post(this.baseUrl + '/genres', genre);
  }

  public update(id: number, genre: GenreCreationDTO) {
    return this.http.put(`${this.baseUrl}/genres/${id}`, genre);
  }

  public delete(id: number) {
    return this.http.delete(`${this.baseUrl}/genres/${id}`)
  }
}
