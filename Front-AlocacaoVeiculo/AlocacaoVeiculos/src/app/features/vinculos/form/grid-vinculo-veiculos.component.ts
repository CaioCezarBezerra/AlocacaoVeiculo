import { Component, inject, ViewChild } from '@angular/core';

import {
  FormBuilder,
  FormsModule,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { MatExpansionModule } from '@angular/material/expansion';
import { MatInputModule } from '@angular/material/input';
import {
  MatTableDataSource,
  MatTableModule
} from '@angular/material/table';

import { MatButtonModule } from '@angular/material/button';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CriaPlano, PlanoAssistencia } from '../../../services/plano-assistencia.service';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { EmpresaAssistencia, EmpresaAssistenciaService } from '../../../services/empresa-assistencia.service';
import { Veiculos, VeiculoService } from '../../../services/veiculos.service';
import { CriarVinculo, VinculoVeiculo } from '../../../services/vinculo-veiculos.service';


export interface CriarVinculos {
  veiculo: string;
  plano: string;
}





@Component({
  selector: 'app-viculo-veiculos',

  standalone: true,

  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatTooltipModule,
    MatIconModule,
    MatExpansionModule,
    MatTableModule,
    MatInputModule,
    MatButtonModule,
    MatStepperModule,
    MatFormFieldModule,
    MatSelectModule
  ],

  templateUrl: './grid-vinculo-veiculos.component.html',
  styleUrl: './grid-vinculo-veiculos.component.scss',
})
export class GridVinculoVeiculos {

  private readonly formBuilder = inject(FormBuilder);
  private readonly planoService = inject(PlanoAssistencia);
  private readonly veiculoService = inject(VeiculoService);
  private readonly vinculoVeiculosService = inject(VinculoVeiculo);

  vinculoVeiculos: VinculoVeiculo[] = [];
  planos: PlanoAssistencia[] = [];
  veiculos: Veiculos[] = [];



  dataSource = new MatTableDataSource<VinculoVeiculo>([]);

  editando: VinculoVeiculo | null = null;

  @ViewChild('stepper')
  stepper!: MatStepper;
  ngOnInit() {
    this.listarPlanos();
    this.listarVeiculo();
    this.listarVinculo();
  }
  displayedColumns: string[] = [
    'plano',
    'veiculo',
    'acoes'
  ];

  get veiculoSelecionado(): Veiculos | undefined {

    const veiculoId =
      this.modeloFormGroup.controls.veiculoId.value!;

    if (veiculoId === null) {
      return undefined;
    }

    return this.veiculos.find(
      veiculo => veiculo.id === Number(veiculoId)
    );
  }

  get planoSelecionado(): PlanoAssistencia | undefined {

    const planoId =
      this.planoFormGroup.controls.planoId.value!;

    if (planoId === null) {
      return undefined;
    }

    return this.planos.find(
      plano => plano.id === Number(planoId)
    );
  }
  planoFormGroup = this.formBuilder.group({
    planoId: [
      null as number | null,
      Validators.required
    ]
  });


  modeloFormGroup = this.formBuilder.group({
    veiculoId: [
      null as number | null,
      Validators.required
    ]
  });

  /*----------------------------------CRIAR----------------------------------------------------------*/
  cadastrarVinculo(): void {

    if (
      this.planoFormGroup.invalid ||
      this.modeloFormGroup.invalid
    ) {
      return;
    }

    const planoId =
      Number(this.planoFormGroup.controls.planoId.value);

    const veiculoId =
      Number(this.modeloFormGroup.controls.veiculoId.value);

    const dados: CriarVinculo = {
      planoId,
      veiculoId
    };

    console.log('ENVIANDO:', dados);

    this.vinculoVeiculosService
      .criarVinculo(dados)
      .subscribe({

        next: (resultado) => {

          console.log('Vínculo criado:', resultado);

          this.listarVinculo();

          this.planoFormGroup.reset();
          this.modeloFormGroup.reset();
          this.stepper.reset();
        },

        error: (erro) => {

          if (erro.status === 409) {

            console.error(
              'Este veículo já está vinculado a este plano.'
            );

            return;
          }

          console.error(
            'Erro ao criar vínculo:',
            erro
          );
        }

      });
  }
  /*----------------------------------LISTAR----------------------------------------------------------*/
  listarVinculo() {
    this.vinculoVeiculosService.listarVinculos().subscribe({
      next: (vinculo) => {
        console.log("Vinculo")
        this.dataSource.data = vinculo;
      }
    })
  }

  listarPlanos() {
    this.planoService.listar().subscribe({
      next: (dados) => {
        console.log('plano DA API:', dados);
        this.planos = dados;
      },
      error: (erro) => {
        console.error('Erro ao buscar veículos:', erro);
      }
    });
  }

  listarVeiculo(): void {

    this.veiculoService.listar().subscribe({

      next: (dados) => {

        console.log('Veiculos DA API:', dados);

        this.veiculos = dados;

        console.log('ARRAY EMPRESAS:', this.veiculos);
      },

      error: (erro) => {

        console.error(
          'Erro ao buscar Veiculos:',
          erro
        );
      }

    });
  }

  editar(vinculo: VinculoVeiculo) {
    this.editando = vinculo;
  }

  /*----------------------------------SALVAR----------------------------------------------------------*/
  salvar(vinculo: VinculoVeiculo): void {

    const dados = {
      plano: vinculo.plano,
      veiculo: vinculo.veiculo,
      id: vinculo.id
    };

    console.log('Dados alterados:', dados);

    this.vinculoVeiculosService
      .atualizar(vinculo.id, dados).subscribe({
        next: (resultado) => {

          console.log(
            'Vinculo atualizado:',
            resultado
          );

          this.listarVinculo();
        },

        error: (erro) => {

          console.error(
            'Erro ao atualizar vinculo:',
            erro
          );

        }

      });
  }
  /*----------------------------------EXCLUIR----------------------------------------------------------*/
  excluir(vinculo: VinculoVeiculo) {
    this.vinculoVeiculosService.deletar(vinculo.id).subscribe({
      next: () => {
        console.log('Veículo excluído');
        this.listarVinculo();
      },
      error: (erro) => {
        console.error('Erro ao excluir:', erro);
      }
    });
  }

}