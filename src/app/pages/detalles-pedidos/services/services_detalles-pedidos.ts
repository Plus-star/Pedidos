import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { detallepedido } from '../models/models_detalles-pedidos'

@Injectable({ providedIn: 'root'})
export class ApiService {
  private base = 'http://localhost:3000/api';

  constructor(private http: HttpClient) {}

  getdetalles(idpedido:number): Observable<detallepedido[]> {
    return this.http.get<detallepedido[]> (`${this.base}/detalles/pedido/${idpedido}`);
  }
  creardetalle(d: detallepedido): Observable<any> {
    return this.http.post(`${this.base}/detalles`, d);
  }
  eliminardetalle(id:number): Observable<any> {
    return this.http.delete(`${this.base}/detalles/${id}`);
  }
}