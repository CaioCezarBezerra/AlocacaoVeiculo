import { AfterViewInit, Component, ViewChild, inject } from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatExpansionModule } from '@angular/material/expansion';

import { CriarVeiculos, Veiculos, VeiculoService } from '../../../services/veiculos.service';
import { CriarVinculo } from '../../../services/vinculo-veiculos.service';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { GrupoVeiculoService } from '../../../services/grupo-veiculo.service';
import { MatSelectModule } from '@angular/material/select';




export interface GrupoVeiculo {
  id: number;
  nome: string;
  descricao: string;
}


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
    MatStepperModule,
    ReactiveFormsModule,
    MatSelectModule
  ],
  templateUrl: './grid.component.html',
  styleUrl: './grid.component.scss',
})
export class GridComponent implements AfterViewInit {

  private readonly veiculoService = inject(VeiculoService);
  private readonly formBuilder = inject(FormBuilder);
  private readonly grupoService = inject(GrupoVeiculoService);

  grupos: GrupoVeiculo[] = [];

  displayedColumns: string[] = [
    'modelo',
    'placa',
    'grupoNome',
    'acoes'
  ];

  dataSource = new MatTableDataSource<Veiculos>([]);

  @ViewChild(MatPaginator)
  paginador!: MatPaginator;

  @ViewChild('stepper')
  stepper!: MatStepper;


  editando: Veiculos | null = null;


  ngOnInit() {
    this.listarVeiculos();
    this.listarGrupos();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginador;
  }


  modeloFormGroup =
    this.formBuilder.group({

      modelo: [
        '',
        Validators.required
      ]

    });

  placaFormGroup =
    this.formBuilder.group({

      placa: [
        '',
        Validators.required
      ]


    });

     grupoFormGroup = this.formBuilder.group({

  grupoId: [
    null as number | null,
    Validators.required
  ]

});


  cadastrarVeiculo(): void {

    if (
      this.modeloFormGroup.invalid ||
      this.placaFormGroup.invalid ||
      this.grupoFormGroup.invalid) {
      return;
    }

    const dados: CriarVeiculos = {
      modelo: this.modeloFormGroup.controls.modelo.value!,
      placa: this.placaFormGroup.controls.placa.value!,
 grupoId:
      this.grupoFormGroup.controls.grupoId.value!
    };

    this.veiculoService.criarVeiculos(dados).subscribe({
      next: (resultado) => {
        console.log('Plano criado:', resultado);
        this.listarVeiculos();

        this.stepper.reset();
      },

      error: (erro) => {
        console.error('Erro ao criar plano:', erro);
      }
    });



    const novoVeiculo = {

      modelo:
        this.modeloFormGroup.controls.modelo.value,

      placa:
        this.placaFormGroup.controls.placa.value,

      grupo:
        this.grupoFormGroup.controls.grupoId.value

    };


    console.log(
      'NOVO VEICULO:',
      novoVeiculo
    );

  }

  /*----------------------------------LISTAR----------------------------------------------------------*/
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

  listarGrupos(): void {

  this.grupoService.ListarGrupoVeiculos().subscribe({

    next: (dados) => {
      console.log('GRUPOS:', dados);

      this.grupos = dados;
    },

    error: (erro) => {
      console.error(
        'Erro ao carregar grupos:',
        erro
      );
    }

  });
}

  editar(veiculo: Veiculos) {
    this.editando = veiculo;
  }
  /*----------------------------------SALVAR----------------------------------------------------------*/
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
  /*----------------------------------EXCLUIR----------------------------------------------------------*/

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