import { Component, inject, Input, OnChanges, OnInit, PLATFORM_ID, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';
import { pedido, pedidoForm } from '../models/models_pedidos';

@Component({
  selector: 'app-pedidos',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pedidos.html',
  styleUrls: ['./pedidos.scss'],
})
export class Pedidos implements OnChanges, OnInit {
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly platformId = inject(PLATFORM_ID);

  @Input() clienteId: number | null = null;
  @Input() isPreview: boolean = false;

  pedidos: pedido[] = [];
  clientes: any[] = []; // Agregar lista de clientes
  clienteActualId: number | null = null;

  form: pedidoForm = {
    fecha: "",
    id_cliente: 0
  };

  editandoId: number | null = null;
  mensaje = "";

  private isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      if (params['cliente']) {
        this.clienteActualId = +params['cliente'];
      }
    });
    this.cargar();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['clienteId']) {
      this.clienteActualId = this.clienteId;
      this.cargar();
    }
  }

  cargar() {
    if (!this.isBrowser()) return;

    const data = localStorage.getItem('pedidos');
    const todos: pedido[] = data ? JSON.parse(data) : [];

    const clientesData = localStorage.getItem('clientes');
    this.clientes = clientesData ? JSON.parse(clientesData) : [];

    if (this.clienteActualId !== null) {
      this.pedidos = todos.filter(
        p => p.id_cliente === this.clienteActualId
      );
    } else {
      this.pedidos = todos;
    }

    // Ordenar por fecha descendente para mostrar los más recientes primero
    this.pedidos.sort((a, b) => new Date(b.fecha).getTime() - new Date(a.fecha).getTime());

    // En preview, limitar a los primeros 5 pedidos
    if (this.isPreview) {
      this.pedidos = this.pedidos.slice(0, 5);
    }
  }

  guardar() {
    if (!this.isBrowser()) return;

    // Validaciones
    if (!this.form.fecha || !this.form.fecha.trim()) {
      this.mensaje = 'La fecha es requerida';
      return;
    }
    const fecha = new Date(this.form.fecha);
    if (isNaN(fecha.getTime())) {
      this.mensaje = 'La fecha no tiene un formato válido';
      return;
    }
    const hoy = new Date();
    hoy.setHours(0, 0, 0, 0);
    if (fecha > hoy) {
      this.mensaje = 'La fecha no puede ser futura';
      return;
    }

    // Verificar que el cliente exista si se especifica
    if (this.clienteActualId !== null) {
      const clienteExiste = this.clientes.some(c => c.id_cliente === this.clienteActualId);
      if (!clienteExiste) {
        this.mensaje = 'El cliente seleccionado no existe';
        return;
      }
    } else {
      this.mensaje = 'Debe seleccionar un cliente para crear un pedido';
      return;
    }

    const data = localStorage.getItem('pedidos');
    const todos: pedido[] = data ? JSON.parse(data) : [];

    const formConCliente = {
      ...this.form,
      id_cliente: this.clienteActualId ?? this.form.id_cliente
    };

    if (this.editandoId !== null) {
      const index = todos.findIndex(p => p.id_pedido === this.editandoId);

      if (index !== -1) {
        todos[index] = {
          ...formConCliente,
          id_pedido: this.editandoId
        };
        this.mensaje = 'Actualizado';
      }
    } else {
      const ids = todos.map(p => p.id_pedido).filter(id => typeof id === 'number' && !isNaN(id));
      const nuevoId = ids.length ? Math.max(...ids) + 1 : 1;

      todos.push({
        ...formConCliente,
        id_pedido: nuevoId
      });

      this.mensaje = 'Creado';
    }

    localStorage.setItem('pedidos', JSON.stringify(todos));

    this.cargar();
    this.limpiar();
  }

  editar(p: pedido) {
    if (!p || !p.id_pedido) {
      this.mensaje = 'Pedido inválido';
      return;
    }
    this.editandoId = p.id_pedido;
    this.form = {
      fecha: p.fecha,
      id_cliente: p.id_cliente
    };
  }

  eliminar(id: number) {
    if (!this.isBrowser()) return;

    if (!id || id <= 0) {
      this.mensaje = 'ID de pedido inválido';
      return;
    }

    if (confirm('¿Eliminar pedido?')) {
      const data = localStorage.getItem('pedidos');
      let todos: pedido[] = data ? JSON.parse(data) : [];

      const index = todos.findIndex(p => p.id_pedido === id);
      if (index === -1) {
        this.mensaje = 'Pedido no encontrado';
        return;
      }

      // Verificar si tiene detalles asociados
      const detallesData = localStorage.getItem('detalles');
      const detalles = detallesData ? JSON.parse(detallesData) : [];
      const tieneDetalles = detalles.some((d: any) => d.id_pedido === id);
      if (tieneDetalles) {
        this.mensaje = 'No se puede eliminar un pedido con detalles asociados';
        return;
      }

      todos = todos.filter(p => p.id_pedido !== id);

      localStorage.setItem('pedidos', JSON.stringify(todos));

      this.mensaje = 'Eliminado';
      this.cargar();
    }
  }

  verDetallesPedidos(id: number) {
  this.router.navigate(['/detalles'], {
    queryParams: { pedido: id }
  });
}

  verDetallesClientes(id: number) {
    this.router.navigate(['/clientes'], {
      queryParams: { cliente: id }
    });
  }

  volverAClientes() {
    this.router.navigate(['/clientes']);
  }

  limpiar() {
    this.form = {
      fecha: "",
      id_cliente: this.clienteActualId ?? 0
    };

    this.editandoId = null;
  }

  getNombreCliente(id: number): string {
    const cliente = this.clientes.find(c => c.id_cliente === id);
    return cliente ? cliente.nombre : 'Desconocido';
  }
}