import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatExpansionModule } from '@angular/material/expansion';

import { Veiculos, VeiculoService } from '../../../services/veiculos.service';

@Component({
  selector: 'app-grid',
  imports: [
    FormsModule,
    MatPaginatorModule,
    MatTableModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatExpansionModule,
  ],
  templateUrl: './grid.component.html',
  styleUrl: './grid.component.scss',
})
export class GridComponent implements AfterViewInit {

  private readonly veiculoService = inject(VeiculoService);

  displayedColumns: string[] = [
    'modelo',
    'placa',
    'grupoNome',
    'acoes'
  ];

  dataSource = new MatTableDataSource<Veiculos>([]);

  @ViewChild(MatPaginator)
  paginador!: MatPaginator;

editando: Veiculos | null = null;


  ngOnInit() {
    this.listarVeiculos();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginador;
  }

  listarVeiculos() {
    this.veiculoService.listar().subscribe({
      next: (veiculos) => {
        console.log("Ola")
        this.dataSource.data = veiculos;
      },
      error: (erro) => {
        console.error('Erro ao buscar veículos:', erro);
      }
    });
  }

  editar(veiculo: Veiculos) {
    this.editando = veiculo;
  }

  salvar(veiculo: Veiculos) {
    console.log('Dados alterados:', veiculo);

    this.veiculoService.atualizar(veiculo.id, {
      modelo: veiculo.modelo,
      placa: veiculo.placa,
      grupoId: veiculo.grupoId
    }).subscribe({
      next: () => {
        console.log('Veículo atualizado');
        this.editando = null;
        this.listarVeiculos();
      },
      error: (erro) => {
        console.error('Erro ao atualizar:', erro);
      }
    });
  }

  excluir(veiculo: Veiculos) {
    this.veiculoService.deletar(veiculo.id).subscribe({
      next: () => {
        console.log('Veículo excluído');
        this.listarVeiculos();
      },
      error: (erro) => {
        console.error('Erro ao excluir:', erro);
      }
    });
  }
}