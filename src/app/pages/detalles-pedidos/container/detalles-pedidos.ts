import { Component, inject, OnInit, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { detallepedido } from '../models/models_detalles-pedidos';


@Component({
  selector: 'app-detalles',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './detalles-pedidos.html',
  styleUrls: ['./detalles-pedidos.scss'],
})


export class Detalles implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly platformId = inject(PLATFORM_ID);

  detalles: detallepedido[] = [];
  editando: boolean = false;
  editandoId: number | null = null;


  form: detallepedido = {
    id_detalle: 0,
    id_pedido: 0,
    producto: '',
    cantidad: 0,
    precio: 0
  };


  idPedido: number = 0;
  idCliente: number = 0;
  mensaje = '';

  ngOnInit() {
    this.route.queryParams.subscribe((params: Params) => {

      this.idPedido = +params['pedido'];

      if (this.isBrowser()) {

        const data = localStorage.getItem('pedidos');

        const pedidos = data ? JSON.parse(data) : [];

        const pedido = pedidos.find((p: any) => p.id_pedido === this.idPedido);

        if (pedido) {
          this.idCliente = pedido.id_cliente; 
        }
      }

      this.cargar();
    });
  }


  private isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }


  cargar() {
    if (!this.isBrowser()) return;

    const data = localStorage.getItem('detalles');
    const all: detallepedido[] = data ? JSON.parse(data) : [];

    this.detalles = all.filter(d => d.id_pedido === this.idPedido);
  }


  guardar() {
    // Validaciones
    if (!this.form.producto || !this.form.producto.trim()) {
      this.mensaje = 'El producto es requerido';
      return;
    }
    if (this.form.producto.trim().length < 2) {
      this.mensaje = 'El producto debe tener al menos 2 caracteres';
      return;
    }
    if (this.form.cantidad === null || this.form.cantidad === undefined || this.form.cantidad <= 0) {
      this.mensaje = 'La cantidad debe ser un número positivo';
      return;
    }
    if (!Number.isInteger(this.form.cantidad)) {
      this.mensaje = 'La cantidad debe ser un número entero';
      return;
    }
    if (this.form.precio === null || this.form.precio === undefined || this.form.precio <= 0) {
      this.mensaje = 'El precio debe ser un número positivo';
      return;
    }

    // Verificar que el pedido exista
    const pedidosData = localStorage.getItem('pedidos');
    const pedidos = pedidosData ? JSON.parse(pedidosData) : [];
    const pedidoExiste = pedidos.some((p: any) => p.id_pedido === this.idPedido);
    if (!pedidoExiste) {
      this.mensaje = 'El pedido no existe';
      return;
    }

    const data = localStorage.getItem('detalles');
    const all: detallepedido[] = data ? JSON.parse(data) : [];

    const ids = all.map(d => d.id_detalle).filter(id => typeof id === 'number' && !isNaN(id));
    const nuevoId = ids.length ? Math.max(...ids) + 1 : 1;

    const nuevo: detallepedido = {
      ...this.form,
      id_detalle: nuevoId,
      id_pedido: this.idPedido
    };


    all.push(nuevo);
    localStorage.setItem('detalles', JSON.stringify(all));

    this.mensaje = 'Detalle agregado';
    this.cargar();
    this.limpiar();
  }


  eliminar(id: number) {
    if (!id || id <= 0) {
      this.mensaje = 'ID de detalle inválido';
      return;
    }

    if (confirm('¿Eliminar detalle?')) {
      const data = localStorage.getItem('detalles');
      let all: detallepedido[] = data ? JSON.parse(data) : [];

      const index = all.findIndex(d => d.id_detalle === id);
      if (index === -1) {
        this.mensaje = 'Detalle no encontrado';
        return;
      }

      all = all.filter(d => d.id_detalle !== id);

      localStorage.setItem('detalles', JSON.stringify(all));

      this.mensaje = 'Eliminado';
      this.cargar();
    }
  }


  editar(detalle: detallepedido) {
    if (!detalle || !detalle.id_detalle) {
      this.mensaje = 'Detalle inválido';
      return;
    }
    this.editando = true;
    this.editandoId = detalle.id_detalle;

    this.form = { ...detalle };
  }


  actualizar() {
    // Validaciones
    if (!this.form.producto || !this.form.producto.trim()) {
      this.mensaje = 'El producto es requerido';
      return;
    }
    if (this.form.producto.trim().length < 2) {
      this.mensaje = 'El producto debe tener al menos 2 caracteres';
      return;
    }
    if (this.form.cantidad === null || this.form.cantidad === undefined || this.form.cantidad <= 0) {
      this.mensaje = 'La cantidad debe ser un número positivo';
      return;
    }
    if (!Number.isInteger(this.form.cantidad)) {
      this.mensaje = 'La cantidad debe ser un número entero';
      return;
    }
    if (this.form.precio === null || this.form.precio === undefined || this.form.precio <= 0) {
      this.mensaje = 'El precio debe ser un número positivo';
      return;
    }

    const data = localStorage.getItem('detalles');
    let all: detallepedido[] = data ? JSON.parse(data) : [];

    all = all.map(d => {
      if (d.id_detalle === this.editandoId) {
        return {
          ...this.form,
          id_pedido: this.idPedido,
          id_detalle: this.editandoId
        };
      }
      return d;
    });

    localStorage.setItem('detalles', JSON.stringify(all));

    this.mensaje = 'Detalle actualizado';

    this.cargar();
    this.limpiar();

    this.editando = false;
    this.editandoId = null;

  }

  verPedidos() {
    this.mensaje = '';
    this.router.navigate(['/pedidos'], {
      queryParams: { cliente: this.idCliente }
    });
  }

  get totalGeneral(): number {
    return this.detalles.reduce((sum, d) => sum + (d.cantidad * d.precio), 0);
  }

  limpiar() {
    this.form = {
      id_detalle: 0,
      id_pedido: this.idPedido,
      producto: '',
      cantidad: 0,
      precio: 0
    };

    this.editando = false;
    this.editandoId = null;
  }
}