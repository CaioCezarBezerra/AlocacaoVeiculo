import { Component, inject } from '@angular/core';
import { GridComponent } from '../../veiculos/form/grid.component';
import { GridPlanos } from '../form/grid-planos.component';
import { PlanoAssistencia } from '../../../services/plano-assistencia.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-planos',
  imports: [GridPlanos],
  templateUrl: './planos.component.html',
  styleUrl: './lista.scss',
})
export class Lista {


   private readonly planosService = inject(PlanoAssistencia);

  private readonly dialog = inject(MatDialog);

  planos: PlanoAssistencia[] = [];

  ngOnInit(): void {
  console.log('1 - COMPONENTE PLANOS INICIOU');

  this.carregarPlanos();
  }

  carregarPlanos(): void {

  console.log('2 - CHAMANDO API');

  this.planosService.listar().subscribe({


    next: (dados) => {
      console.log("passei/")
      console.log('3 - API RESPONDEU');
      console.log('DADOS:', dados);
      console.log('TOTAL:', dados.length);

      this.planos = dados;

      console.log(
        'PLANOS NO COMPONENTE:',
        this.planos
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
