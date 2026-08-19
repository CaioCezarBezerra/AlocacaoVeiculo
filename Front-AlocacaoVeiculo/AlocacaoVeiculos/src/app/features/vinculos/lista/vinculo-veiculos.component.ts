import { Component } from '@angular/core';
import { GridVinculoVeiculos } from '../form/grid-vinculo-veiculos.component';


@Component({
  selector: 'app-lista',
  imports: [GridVinculoVeiculos],
  templateUrl: './vinculo-veiculos.component.html',
  styleUrl: './vinculo-veiculos.component.scss',
})
export class VinculoVeiculosComponent {}
