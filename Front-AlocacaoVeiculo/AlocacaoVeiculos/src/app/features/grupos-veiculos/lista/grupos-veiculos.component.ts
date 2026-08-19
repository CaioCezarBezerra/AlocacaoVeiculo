import { Component } from '@angular/core';
import { GridGrupoVeiculosComponent } from '../form/grid-grupo-veiculos.component';

@Component({
  selector: 'app-lista',
  imports: [GridGrupoVeiculosComponent],
  templateUrl: './grupos-veiculos.component.html',
  styleUrl: './grupos-veiculos.component.scss',
})
export class GruposVeiculos {}
