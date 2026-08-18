import { Routes } from '@angular/router';

export const routes: Routes = [
    {
    path: '',
    redirectTo: 'veiculos',
    pathMatch: 'full'
  },
  {
    
    path: '',
    loadComponent: () => import('./core/layout/layout.component').then(m => m.LayoutComponent),
    children: [
      { path: '', redirectTo: 'veiculos', pathMatch: 'full' },
      {
        path: 'grupos',
        loadComponent: () => import('./features/grupos-veiculos/lista/lista').then(m => m.Lista)
      },
      {
        path: 'veiculos',
        loadComponent: () => import('./features/veiculos/lista/veiculos.component').then(m => m.VeiculosComponent)
      },
      {
        path: 'empresas',
        loadComponent: () => import('./features/empresas-assistencia/lista/empresa.component').then(m => m.EmpresaComponent)
      },
      {
        path: 'planos',
        loadComponent: () => import('./features/planos-assistencia/lista/lista').then(m => m.Lista)
      },
      {
        path: 'vinculos',
        loadComponent: () => import('./features/vinculos/lista/lista').then(m => m.Lista)
      },
      
    ]
  }
];