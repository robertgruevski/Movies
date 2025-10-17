import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import {
  LandingDTO,
  MovieCreationDTO,
  MovieDTO,
  MoviesPostGetDTO,
  MoviesPutGetDTO,
} from './movies.models';

@Injectable({
  providedIn: 'root',
})
export class MoviesService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/movies';

  public postGet(): Observable<MoviesPostGetDTO> {
    return this.http.get<MoviesPostGetDTO>(`${this.baseUrl}/postget`);
  }

  public getLanding(): Observable<LandingDTO> {
    return this.http.get<LandingDTO>(`${this.baseUrl}/landing`);
  }

  public getById(id: number): Observable<MovieDTO> {
    return this.http.get<MovieDTO>(`${this.baseUrl}/${id}`);
  }

  public filter(values: any): Observable<HttpResponse<MovieDTO[]>> {
    const params = new HttpParams({ fromObject: values });
    return this.http.get<MovieDTO[]>(`${this.baseUrl}/filter`, { params, observe: 'response' });
  }

  public create(movie: MovieCreationDTO) {
    const formData = this.buildFormData(movie);
    return this.http.post(this.baseUrl, formData);
  }

  public putGet(id: number): Observable<MoviesPutGetDTO> {
    return this.http.get<MoviesPutGetDTO>(`${this.baseUrl}/putget/${id}`);
  }

  public update(id: number, movie: MovieCreationDTO) {
    const formData = this.buildFormData(movie);
    return this.http.put(`${this.baseUrl}/${id}`, formData);
  }

  public delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  private buildFormData(movie: MovieCreationDTO): FormData {
    const formData = new FormData();
    formData.append('title', movie.title);

    if (movie.releaseDate) {
      formData.append('releaseDate', movie.releaseDate.toISOString().split('T')[0]);
    }

    if (movie.poster) {
      formData.append('poster', movie.poster);
    }

    if (movie.trailer) {
      formData.append('trailer', movie.trailer);
    }

    formData.append('genresIds', JSON.stringify(movie.genresIds));
    formData.append('theatersIds', JSON.stringify(movie.theatersIds));
    formData.append('actors', JSON.stringify(movie.actors));

    return formData;
  }
}
