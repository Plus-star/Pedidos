// src/app/componentes/clientes/clientes.component.ts
import { Component, OnInit, AfterViewInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; // <-- OBLIGATORIO para los [(ngModel)] de tu HTML
import { ClientesService } from '../services/services_clientes';
import { cliente } from '../models/models_clientes';

// Si ya tienes el componente de pedidos creado, descomenta la siguiente línea e impórtalo:
// import { PedidosComponent } from '../pedidos/pedidos.component';

@Component({
  selector: 'app-clientes',
  standalone: true,
  // Aquí es donde le dices a Angular que tu HTML está en un archivo separado:
  templateUrl: './clientes.html', 
  styleUrls: ['./clientes.scss'],
  // Registramos CommonModule y FormsModule para que el HTML reconozca el *ngFor y los inputs
  imports: [CommonModule, FormsModule/*, PedidosComponent*/] 
})
export class ClientesComponent implements OnInit, AfterViewInit {


  constructor(private clientesService: ClientesService) { }
  // Variables que renderiza tu HTML separado
  clientes: cliente[] = [];
  clienteSeleccionado: cliente | null = null;
  mensaje: string = '';
  editandoId: number | null = null;

    form: cliente = {
    id_cliente: 0,
    nombre: '',
    email: ''
    };


  ngOnInit(): void {
    //this.listarClientes();
  }

    ngAfterViewInit(): void {
    this.listarClientes();
  }

listarClientes(): void {
  debugger;
  this.clientesService.getClientes().subscribe({
    next: (data) => { 
      debugger;
      this.clientes = data; },
    error: (error) => { console.log('Error al conectar con el servidor.'); }
  });
}

  seleccionarCliente(cliente: cliente): void {
    this.clienteSeleccionado = cliente;
  }

  editar(cliente: cliente): void {
    /*this.editandoId = cliente.id_cliente;
    this.form = {
      id_cliente: cliente.id_cliente,
      nombre: cliente.nombre || '',
      email: cliente.email || ''
    */};
  

  guardar(): void {
    /*if (!this.form.nombre || !this.form.email) {
      this.mostrarMensaje('Por favor diligencie todos los campos obligatorios (*)');
      return;
    
    }

    if (this.editandoId) {
      this.clientesService.updateCliente(this.form).subscribe({
        next: (exito) => {
          if (exito) {
            this.mostrarMensaje('Cliente actualizado con éxito.');
            this.finalizarAccion();
          }
        },
        error: () => this.mostrarMensaje('Error al actualizar cliente.')
      });
    } else {
      this.clientesService.saveCliente(this.form).subscribe({
        next: (nuevoId) => {
          this.mostrarMensaje(`Cliente creado con éxito. ID: ${nuevoId}`);
          this.finalizarAccion();
        },
        error: () => this.mostrarMensaje('Error al crear cliente.')
      });
    }
    */
  }

  eliminar(id: number): void {
    /*
    if (confirm('¿Está seguro de eliminar este cliente?')) {
      this.clientesService.deleteCliente(id).subscribe({
        next: (exito) => {
          if (exito) {
            this.mostrarMensaje('Cliente eliminado de la base de datos.');
            if (this.clienteSeleccionado?.id_cliente === id) {
              this.clienteSeleccionado = null;
            }
            this.listarClientes();
          }
        },
        error: () => this.mostrarMensaje('Error al eliminar el cliente.')
      });
    }
    */
  }

  limpiar(): void {
    
    this.editandoId = null;
    this.form = { id_cliente: 0, nombre: '', email: '' };
    
  }

  private finalizarAccion(): void {
    this.limpiar();
    this.listarClientes();
  }

  private mostrarMensaje(text: string): void {
    this.mensaje = text;
    setTimeout(() => this.mensaje = '', 4000);
  }

  verPedidosGenerales(): void {
    console.log('Abriendo panel general de pedidos...');
  }

  verPedidos(idCliente: number): void {
    console.log('Mostrando pedidos del cliente ID:', idCliente);
  }
}