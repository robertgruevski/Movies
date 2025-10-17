import { inject, Injectable } from '@angular/core';
import { ICRUDService } from '../shared/interfaces/ICRUDService';
import { TheaterCreationDTO, TheaterDTO } from './theaters.models';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginationDTO } from '../shared/models/PaginationDTO';
import { environment } from '../../environments/environment';
import { buildQueryParams } from '../shared/functions/buildQueryParams';

@Injectable({
  providedIn: 'root',
})
export class TheatersService implements ICRUDService<TheaterDTO, TheaterCreationDTO> {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/theaters';

  getPaginated(pagination: PaginationDTO): Observable<HttpResponse<TheaterDTO[]>> {
    let queryParams = buildQueryParams(pagination);
    return this.http.get<TheaterDTO[]>(this.baseUrl, { params: queryParams, observe: 'response' });
  }
  getById(id: number): Observable<TheaterDTO> {
    return this.http.get<TheaterDTO>(`${this.baseUrl}/${id}`);
  }
  create(entity: TheaterCreationDTO): Observable<any> {
    return this.http.post(this.baseUrl, entity);
  }
  update(id: number, entity: TheaterCreationDTO): Observable<any> {
    return this.http.put(`${this.baseUrl}/${id}`, entity);
  }
  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
