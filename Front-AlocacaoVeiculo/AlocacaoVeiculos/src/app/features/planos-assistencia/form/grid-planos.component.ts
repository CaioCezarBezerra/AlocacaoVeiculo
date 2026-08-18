import { Component, inject } from '@angular/core';

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
import { MatStepperModule } from '@angular/material/stepper';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CriaPlano, PlanoAssistencia } from '../../../services/plano-assistencia.service';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';


export interface Plano {
  id: number;
  plano: string;
  descricao: string;
  valorCobertura: number;
  empresaId: number;
}

export interface EmpresaAssistencia {
  id: number;
  nome: string;
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

   empresas: EmpresaAssistencia[] = []


    dataSource = new MatTableDataSource<PlanoAssistencia>([]);

    editando: PlanoAssistencia | null = null;


     ngOnInit() {
    this.listarPlanos();
  }
  displayedColumns: string[] = [
    'plano',
    'valorCobertura',
    'descricao',
    'empresaId',
    'acoes'
  ];



  planoFormGroup =
    this.formBuilder.group({

      planos: [
        '',
        Validators.required
      ]

    });


  empresaFormGroup =
    this.formBuilder.group({

      empresaNome: [
        '',
        Validators.required
      ],
       empresaId: [
        null as number | null,
        [
          Validators.required,
          Validators.min(1)
        ]
      ]

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



  cadastrarPlano(): void {

    if (
      this.planoFormGroup.invalid ||
      this.empresaFormGroup.invalid ||
      this.descricaoFormGroup.invalid ||
      this.valorCoberturaFormGroup.invalid )
     {
      return;
    }

    const dados: CriaPlano = {
  plano: this.planoFormGroup.controls.planos.value!,
  descricao: this.descricaoFormGroup.controls.descricao.value!,
  empresaNome: this.empresaFormGroup.controls.empresaNome.value!,
  empresaId: Number(
    this.empresaFormGroup.controls.empresaId.value
  ),
  valorCobertura: Number(
    this.valorCoberturaFormGroup.controls.valorCobertura.value
  )
};

this.planoService.criarPlano(dados).subscribe({
  next: (resultado) => {
    console.log('Plano criado:', resultado);
    this.listarPlanos();
  },

  error: (erro) => {
    console.error('Erro ao criar plano:', erro);
  }
});



    const novoPlano = {

      plano:
        this.planoFormGroup.controls.planos.value,

      empresa:
        this.empresaFormGroup.controls.empresaNome.value,
      empresaId:
        this.empresaFormGroup.controls.empresaId.value,

      valorCobertura:
        this.valorCoberturaFormGroup.controls.valorCobertura.value

    };


    console.log(
      'NOVO PLANO:',
      novoPlano
    );

  }


  listarPlanos(){
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

  editar(plano: PlanoAssistencia) {
      this.editando = plano;
    }
  
  salvar(plano: Plano): void {

  console.log('ID DO PLANO:', plano.id);
  console.log('EMPRESA ID:', plano.empresaId);

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

  excluir(plano: PlanoAssistencia ) {
      this.planoService.deletar(plano.empresaId).subscribe({
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