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


export interface Plano {
  id: number;
  plano: string;
  descricao: string;
  valorCobertura: number;
  empresaId: number;
}



@Component({
  selector: 'app-grid-planos',

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

  templateUrl: './grid-planos.component.html',
  styleUrl: './form.scss',
})
export class GridPlanos {

  private readonly formBuilder = inject(FormBuilder);
  private readonly planoService = inject(PlanoAssistencia);
  private readonly empresaService = inject(EmpresaAssistenciaService)

  empresas: EmpresaAssistencia[] = [];


  dataSource = new MatTableDataSource<PlanoAssistencia>([]);

  editando: PlanoAssistencia | null = null;

@ViewChild('stepper')
stepper!: MatStepper;
  ngOnInit() {
    this.listarPlanos();
    this.listarEmpresas();
  }
  displayedColumns: string[] = [
    'plano',
    'valorCobertura',
    'descricao',
    'empresaId',
    'acoes'
  ];

  get empresaSelecionada(): EmpresaAssistencia | undefined {

    const id = this.empresaFormGroup.controls.id.value;

    if (id === null) {
      return undefined;
    }

    return this.empresas.find(
      empresa => empresa.id === Number(id)
    );
  }



  planoFormGroup =
    this.formBuilder.group({

      planos: [
        '',
        Validators.required
      ]

    });


  empresaFormGroup = this.formBuilder.group({
    id: [null as number | null, Validators.required]
  });
  valorCoberturaFormGroup =
    this.formBuilder.group({

      valorCobertura: [
        null as number | null,
        [
          Validators.required,
          Validators.min(1)
        ]
      ]

    });

  descricaoFormGroup =
    this.formBuilder.group({

      descricao: [
        '',
        Validators.required
      ]


    });


  /*----------------------------------CRIAR----------------------------------------------------------*/
  cadastrarPlano(): void {

    if (
      this.planoFormGroup.invalid ||
      this.empresaFormGroup.invalid ||
      this.descricaoFormGroup.invalid ||
      this.valorCoberturaFormGroup.invalid) {
      return;
    }

    const empresaSelecionada =
      this.empresas.find(
        empresa => empresa.id === empresa.id
      );


    if (!empresaSelecionada) {
      console.error('Empresa não encontrada');
      return;
    }

    const empresa = this.empresaSelecionada;
    console.log("empresa: " + empresa)

    if (!empresa) {
      console.error('Empresa não selecionada');
      return;
    }

    const dados: CriaPlano = {
      plano: this.planoFormGroup.controls.planos.value!,
      descricao: this.descricaoFormGroup.controls.descricao.value!,
      empresaId:
        empresa.id,
      valorCobertura: Number(
        this.valorCoberturaFormGroup.controls.valorCobertura.value
      )
    };

    this.planoService.criarPlano(dados).subscribe({
      next: (resultado) => {
        console.log('Plano criado:', resultado);
        this.listarPlanos();
        
       this.stepper.reset();
      },

      error: (erro) => {
        console.error('Erro ao criar plano:', erro);
      }
    });



    const novoPlano = {

      plano:
        this.planoFormGroup.controls.planos.value,

      empresaId:
        empresaSelecionada.id,

      empresaNome:
        empresaSelecionada.nome,

      valorCobertura:
        this.valorCoberturaFormGroup.controls.valorCobertura.value

    };


    console.log(
      'NOVO PLANO:',
      novoPlano
    );

  }

  /*----------------------------------LISTAR----------------------------------------------------------*/
  listarPlanos() {
    this.planoService.listar().subscribe({
      next: (plano) => {
        console.log("Ola")
        this.dataSource.data = plano;
      },
      error: (erro) => {
        console.error('Erro ao buscar veículos:', erro);
      }
    });
  }

  listarEmpresas(): void {

    this.empresaService.listarEmpresa().subscribe({

      next: (dados) => {

        console.log('EMPRESAS DA API:', dados);

        this.empresas = dados;

        console.log('ARRAY EMPRESAS:', this.empresas);
      },

      error: (erro) => {

        console.error(
          'Erro ao buscar empresas:',
          erro
        );
      }

    });
  }

  editar(plano: PlanoAssistencia) {
    this.editando = plano;
  }

  /*----------------------------------SALVAR----------------------------------------------------------*/
  salvar(plano: Plano): void {

    const dados = {
      plano: plano.plano,
      descricao: plano.descricao,
      valorCobertura: Number(plano.valorCobertura),
      empresaId: plano.empresaId
    };

    console.log('Dados alterados:', dados);

    this.planoService
      .atualizar(plano.id, dados)
      .subscribe({

        next: (resultado) => {

          console.log(
            'Plano atualizado:',
            resultado
          );

          this.listarPlanos();
        },

        error: (erro) => {

          console.error(
            'Erro ao atualizar plano:',
            erro
          );

        }

      });
  }
  /*----------------------------------EXCLUIR----------------------------------------------------------*/
  excluir(plano: PlanoAssistencia) {
    this.planoService.deletar(plano.id).subscribe({
      next: () => {
        console.log('Veículo excluído');
        this.listarPlanos();
      },
      error: (erro) => {
        console.error('Erro ao excluir:', erro);
      }
    });
  }
  

}