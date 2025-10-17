import { HttpClient, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { ActorAutoCompleteDTO, ActorCreationDTO, ActorDTO } from './actors.models';
import { PaginationDTO } from '../shared/models/PaginationDTO';
import { Observable } from 'rxjs';
import { buildQueryParams } from '../shared/functions/buildQueryParams';
import { ICRUDService } from '../shared/interfaces/ICRUDService';

@Injectable({
  providedIn: 'root',
})
export class ActorsService implements ICRUDService<ActorDTO, ActorCreationDTO> {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl + '/actors';

  public getPaginated(pagination: PaginationDTO): Observable<HttpResponse<ActorDTO[]>> {
    let queryParams = buildQueryParams(pagination);
    return this.http.get<ActorDTO[]>(this.baseUrl, { params: queryParams, observe: 'response' });
  }

  public getById(id: number): Observable<ActorDTO> {
    return this.http.get<ActorDTO>(`${this.baseUrl}/${id}`);
  }

  public getByName(name: string): Observable<ActorAutoCompleteDTO[]> {
    return this.http.get<ActorAutoCompleteDTO[]>(`${this.baseUrl}/${name}`);
  }

  public create(actor: ActorCreationDTO) {
    const formData = this.buildFormData(actor);
    return this.http.post(this.baseUrl, formData);
  }

  public update(id: number, actor: ActorCreationDTO) {
    const formData = this.buildFormData(actor);
    return this.http.put(`${this.baseUrl}/${id}`, formData);
  }

  public delete(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  private buildFormData(actor: ActorCreationDTO): FormData {
    const formData = new FormData();

    formData.append('name', actor.name);
    formData.append('dateOfBirth', actor.dateOfBirth.toISOString().split('T')[0]);

    if (actor.picture) {
      formData.append('picture', actor.picture);
    }

    return formData;
  }
}
