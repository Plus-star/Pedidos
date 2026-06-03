import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { cliente } from '../models/models_clientes'
import { environment } from '../../../../environments/environment';

@Injectable({ 
  providedIn: 'root'
})
export class ClientesService {

  constructor(private http: HttpClient) { }


  getClientes(): Observable<cliente[]> {
    debugger;
    return this.http.get<cliente[]>(environment.apiUrl + '/Clientes');
  }

  /*getCliente(id: number): Observable<cliente[]> {
    return this.http.get<cliente[]>(`${this.url}/${id}`);
  } 

  saveCliente(request: CrearActualizarClienteRequest): Observable<number> {
    return this.http.post<number>(this.url, request);
  }

  updateCliente(request: CrearActualizarClienteRequest): Observable<boolean> {
    return this.http.post<boolean>(`${this.url}/Actualizar`, request);
  }

  deleteCliente(id: number): Observable<boolean> {
    return this.http.post<boolean>(`${this.url}/Eliminar?id=${id}`, {});
  }
    */

}