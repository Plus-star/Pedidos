import { Routes } from '@angular/router';
import { ClientesComponent } from './pages/clientes/container/clientes';
import { Pedidos } from './pages/pedidos/container/pedidos';
import { Detalles } from './pages/detalles-pedidos/container/detalles-pedidos';

export const routes: Routes = [
    {
        path: '', redirectTo: '/clientes', pathMatch: 'full'
    },
    {
        path: 'clientes',
        component: ClientesComponent
    },
    {
        path: 'pedidos',
        component: Pedidos
    },
    {
        path: 'detalles',
        component: Detalles
    }
];
