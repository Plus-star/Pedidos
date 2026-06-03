import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { pedido } from '../models/models_pedidos'

@Injectable({ providedIn: 'root'})
export class ApiService {
  private base = 'http://localhost:3000/api';

  constructor(private http: HttpClient) {}


  getpedidos(): Observable<pedido[]> {
    return this.http.get<pedido[]>(`${this.base}/pedidos`);
  }
  getpedidosporcliente(id_cliente: number): Observable<pedido[]>{
    return this.http.get<pedido[]>(`${this.base}/pedidos/cliente/${id_cliente}`)
  }
  crearpedido(p: pedido): Observable<any> {
    return this.http.post(`${this.base}/pedidos`, p);
  }
  actualizarpedido(id:number, p:pedido): Observable<any> {
    return this.http.put(`${this.base}/pedidos/${id}`, p);
  } 
  eliminarpedido(id:number): Observable<any> {
    return this.http.delete(`${this.base}/pedidos/${id}`);
  }
}