import { Component, inject } from '@angular/core';
import { GridComponent } from '../form/grid.component';

import {
  VeiculoService,
  Veiculos
} from '../../../services/veiculos.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-lista',
  imports: [GridComponent],
  templateUrl: './veiculos.component.html',
  styleUrl: './veiculos.component.scss',
})
export class VeiculosComponent {
  private readonly veiculoService = inject(VeiculoService);

  private readonly dialog = inject(MatDialog);

  veiculos: Veiculos[] = [];

  ngOnInit(): void {
  console.log('1 - COMPONENTE VEICULO INICIOU');

  this.carregarVeiculos();
  }
/*----------------------------------LISTAR----------------------------------------------------------*/
  carregarVeiculos(): void {

  console.log('2 - CHAMANDO API');

  this.veiculoService.listar().subscribe({


    next: (dados) => {
      console.log("passei/")
      console.log('3 - API RESPONDEU');
      console.log('DADOS:', dados);
      console.log('TOTAL:', dados.length);

      this.veiculos = dados;

      console.log(
        'VEICULOS NO COMPONENTE:',
        this.veiculos
      );
    },

    error: (erro) => {

      console.error(
        '3 - API DEU ERRO:',
        erro
      );

    }

  });

  }
}
